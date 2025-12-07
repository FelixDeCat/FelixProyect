namespace AI.Tools
{
    using System;
    using System.Collections.Generic;

    public class EventFSM<T>
    {
        Dictionary<IState, Dictionary<T, IState>> transitions;
        bool started = false;
        IState current;

        public event Action<IState, IState> OnStateChanged;

        public EventFSM(IState first)
        {
            transitions = new Dictionary<IState, Dictionary<T, IState>>();
            this.current = first;
        }

        Action<string> debug;
        public void SetDebug(Action<string> debug)
        {
            this.debug = debug;
        }

        public void StartFSM()
        {
            if (current == null) throw new System.Exception("No hay un Estado marcado como FIRST");
            if (started) return;
            started = true;
            current.Enter();
            debug?.Invoke($"FIRST current_behaviour:{current.ToString()}");
        }

        public void AddTransition(IState from, T input, IState to)
        {
            if (from == null) throw new System.ArgumentNullException(nameof(from));
            if (to == null) throw new System.ArgumentNullException(nameof(to));

            if (!transitions.TryGetValue(from, out var map))
            {
                //si no estaba el estado, lo agrego y ademas le creo el diccionario interno
                map = new Dictionary<T, IState>();
                transitions[from] = map;
            }

            if (map.ContainsKey(input))
            {
                throw new Exception("Ya existía la llave relacionada a este estado");
            }

            map.Add(input, to);
        }
        

        // Envía un input y cambia de estado si existe la transición.
        public bool SendInput(T input)
        {
            return TrySendInput(input, out _);
        }
        public bool TrySendInput(T input, out IState nextState)
        {
            nextState = null;
            if (current == null) return false;

            if (!transitions.TryGetValue(current, out var map))
            {
                debug?.Invoke($"c:<color=red>{current}</color> in: <color=yellow>{input}</color> (no transitions defined)");
                return false;
            }

            if (!map.TryGetValue(input, out nextState))
            {
                debug?.Invoke($"c:<color=red>{current}</color> in: <color=yellow>{input}</color> (no match)");
                return false;
            }

            current.Exit();
            var prev = current;
            current = nextState;

            debug?.Invoke($"c:<color=green>{current}</color> in: <color=yellow>{input}</color>");
            current.Enter();

            OnStateChanged?.Invoke(prev, current);
            return true;
        }

        public void UpdateFSM()
        {
            if (current != null)
            {
                current.Update();
            }
        }
        public void ChangeState(IState newState, bool callExitEnter = true)
        {
            if (newState == null) throw new ArgumentNullException(nameof(newState));
            if (ReferenceEquals(current, newState)) return;
            var prev = current;
            if (callExitEnter && current != null)
            {
                current.Exit();
            }
            current = newState;
            if (callExitEnter)
            {
                current.Enter();
            }
            OnStateChanged?.Invoke(prev, current);
        }

        public void ClearTransitions()
        {
            transitions.Clear();
        }
        public bool RemoveTransition(IState from, T input)
        {
            if (from == null) return false;
            if (!transitions.TryGetValue(from, out var map)) return false;
            return map.Remove(input);
        }
        public bool HasTransition(IState from, T input)
        {
            if (from == null) return false;
            return transitions.TryGetValue(from, out var map) && map.ContainsKey(input);
        }
    }
}
