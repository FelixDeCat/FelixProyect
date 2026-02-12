using UnityEngine;
using AI.Tools;

[System.Serializable]
public class BuildMode_State : StateBase
{

    MoveControl moveControl;
    public void SetMoveControl(MoveControl _moveControl) => moveControl = _moveControl;

    [SerializeField] float speedMultiplier = 0.5f;

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        // Using State
        // Build Menu State
    }
}
