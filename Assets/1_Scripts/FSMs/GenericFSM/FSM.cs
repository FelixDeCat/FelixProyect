using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    Dictionary<State, Dictionary<T, State>> transitions;

    public FSM()
    {
        transitions = new Dictionary<State, Dictionary<T, State>>();
    }

    public void AddTransition(State from, T input, State to)
    {
        // Forma vieja
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
}
