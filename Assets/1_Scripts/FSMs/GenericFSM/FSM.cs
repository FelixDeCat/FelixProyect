using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    Dictionary<State, Dictionary<T, State>> transitions;

    State current;

    public FSM(State first)
    {
        transitions = new Dictionary<State, Dictionary<T, State>>();
        this.current = first;
    }

    public void StartFSM()
    {
        if (current == null) throw new System.Exception("No hay un Estado marcado como FIRST");
        current.OnEnter();
    }

    public void AddTransition(State from, T input, State to)
    {
        if (from == null) throw new System.ArgumentNullException(nameof(from));
        if (to == null) throw new System.ArgumentNullException(nameof(to));

        if (transitions.ContainsKey(from))
        {
            if (transitions[from].ContainsKey(input))
            {
                throw new System.Exception("Ya existia la llave relacionada a este estado");
            }
            transitions[from].Add(input, to);
        }
        else
        {
            Dictionary<T, State> inputto = new Dictionary<T, State>();
            inputto.Add(input, to);
            transitions.Add(from, inputto);
        }
    }

    public bool SendInput(T input)
    {
        if (current == null) return false;
        if (transitions[current].ContainsKey(input))
        {
            current.OnExit();
            current = transitions[current][input];
            current.OnEnter();
            return true;
        }
        return false;
    }

    public void UpdateFSM()
    {
        if (current != null)
        {
            current.OnUpdate();
        }
    }
}
