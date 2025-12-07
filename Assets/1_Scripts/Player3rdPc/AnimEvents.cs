using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    Dictionary<string, Action> events = new Dictionary<string, Action>();

    public void SubscribeToEvent(string event_key, Action action)
    {
        if (events.ContainsKey(event_key)) throw new System.Exception("No puedo recibir dos eventos con el mismo parametro");
        events.Add(event_key, action);
    }

    public void ANIM_EVENT(string key)
    {
        if (!events.ContainsKey(key)) throw new System.Exception($"No tengo esa Key {key}");
        events[key].Invoke();
    }
}
