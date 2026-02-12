using UnityEngine;
using AI.Tools;
[System.Serializable]
public class Inventory_State : StateBase
{
    MoveControl moveControl;
    public void SetMoveControl(MoveControl _moveControl) => moveControl = _moveControl;

    [SerializeField] float speedMultiplier = 1f;

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
