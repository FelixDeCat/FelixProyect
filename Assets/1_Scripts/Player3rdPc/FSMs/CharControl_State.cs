using System;
using UnityEngine;
using AI.Tools;

[System.Serializable]
public class CharControl_State : StateBase, IStarteable, IFixedUpdateable
{
    MoveControl moveControl;
    public void SetMoveControl(MoveControl _moveControl) => moveControl = _moveControl;

    PlayerView view;
    Action onHit;

    [SerializeField] float speedMultiplier = 1f;

    public void SetView(PlayerView view)
    {
        this.view = view;
    }

    void IStarteable.Start()
    {
        
    }

    public override void OnEnter()
    {
        view.SubscribeToEvent("hit", ANIM_EV_Hit);
    }

    public override void OnExit()
    {
        
    }

    protected override void OnPause()
    {
        
    }
    protected override void OnResume()
    {
        
    }

    public void SubscribeToDoHit(Action onHit)
    {
        this.onHit = onHit;
    }
    void ANIM_EV_Hit()
    {
        Debug.Log("IsHitting");
        onHit?.Invoke();
    }

    public override void OnUpdate()
    {
        moveControl.Tick();

        if (Input.GetButtonDown("Fire1"))
        {
            view.Animate_Fire();
        }

        view.Animate_Motion_Basic(moveControl.InputMagnitude);
    }

    void IFixedUpdateable.FixedTick(float delta)
    {
        moveControl.FixedTick(speedMultiplier: 1);
    }
}
