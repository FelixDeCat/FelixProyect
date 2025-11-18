using AI.Tools;
using UnityEngine;

public class InputController : MonoBehaviour
{
    FSMLite fsm;

    [SerializeField] ThirdPersonCharacter character;

    EState playing;
    EState paused;
    EState menues;
    EState inventories;
    EState animations;

    private void Start()
    {
        playing = new EState()
            .OnInitialize(() => { })
            .OnEnter(() => 
            {
                CameraFollow.Instance.Activate(true);
                character.Activate();
            })
            .OnExit(() => 
            { 
                CameraFollow.Instance.Activate(false);
                character.Deactivate();
            })
            .OnUpdate(() =>
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    ChangeState(inventories);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeState(paused);
                }
            }
            );

        paused = new EState()
            .OnInitialize(() => { })
            .OnEnter(() => { Time.timeScale = 0; })
            .OnExit(() => { Time.timeScale = 1; })
            .OnUpdate(() => { });

        menues = new EState()
            .OnInitialize(() => { })
            .OnEnter(() =>
            {
                
            })
            .OnExit(() => { })
            .OnUpdate(() => { });

        inventories = new EState()
            .OnInitialize(() => { })
            .OnEnter(() => 
            { 
                PlayerPanel.Open(); 
            })
            .OnExit(() => 
            { 
                PlayerPanel.Close(); 
            })
            .OnUpdate(() =>
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeState(playing);
                }
            }
            );

        animations = new EState()
            .OnInitialize(() => { })
            .OnEnter(() => { })
            .OnExit(() => { })
            .OnUpdate(() => { });

        fsm = new FSMLite(playing);

        fsm.StartFSM();
    }

    public void ChangeState(EState state)
    {
        fsm.ChangeTo(state);
    }

    void Update()
    {
        fsm.UpdateFSM();
    }
}
