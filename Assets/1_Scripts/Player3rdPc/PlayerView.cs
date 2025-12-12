using System;
using UnityEngine;

[System.Serializable]
public class PlayerView : IPausable
{
    [SerializeField] Animator mainAnimator;
    [SerializeField] AnimEvents events;

    public const int BASE_LAYER = 0;
    public const int ARMS_LAYER = 1;

    public void SubscribeToEvent(string event_key, Action action)
    {
        events.SubscribeToEvent(event_key, action);
    }

    public void Animate_Fire()
    {
        mainAnimator.Play("PH_MoveArm_Right", ARMS_LAYER);
    }

    void IPausable.Pause()
    {
        
    }

    void IPausable.Resume()
    {
        
    }
}
