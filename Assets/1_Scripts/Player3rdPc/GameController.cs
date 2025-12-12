using AI.Tools;
using UnityEngine;

public class GameController : MonoBehaviour
{
    FSMLite fsm;

    [SerializeField] ThirdPersonCharacter character;

    EState playing;
    EState paused;
    EState menues;
    EState inventories;
    EState animations;
    EState deb45;

    private void Start()
    {
        CameraFollow.Instance.Activate(true);

        //////////////////////////////////////
        /// P L A Y I N G
        //////////////////////////////////////
        playing = new EState().SetCallbacks
        (
            _enter: () =>
            {
                
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.thirdPersonCam);
                
                CameraFollow.Instance.ActiveCursor(true);
                character.Activate();
            },
            _exit: () =>
            {
                character.Deactivate();
            },
            _tick: () =>
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    ChangeState(inventories);
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeState(deb45);
                }
            }
        );


        //////////////////////////////////////
        /// P A U S E D
        //////////////////////////////////////
        paused = new EState().SetCallbacks
        (
            _enter: () => { Time.timeScale = 0; },
            _exit: () => { Time.timeScale = 1; },
            _tick: () => { }
        );


        //////////////////////////////////////
        /// M E N U E S
        //////////////////////////////////////
        menues = new EState().SetCallbacks
        (
            _enter: () => { },
            _exit: () => { },
            _tick: () => { }
        );

        //////////////////////////////////////
        /// D E B U G  4 5
        //////////////////////////////////////
        deb45 = new EState().SetCallbacks
        (
            _enter: () =>
            {
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.debug45);
                character.Activate();
            },
            _exit: () =>
            {
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.thirdPersonCam);
            },
            _tick: () =>
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    ChangeState(playing);
                }
            }
        );


        //////////////////////////////////////
        /// I N V E N T O R I E S
        //////////////////////////////////////
        inventories = new EState().SetCallbacks
        (
            _enter: () => 
            {
                PlayerPanel.Open();
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.lookAtPlayer);
            },
            _exit: () => 
            { 
                PlayerPanel.Close(); 
            },
            _tick: () =>
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    ChangeState(playing);
                }
            }
        );

        //////////////////////////////////////
        /// A N I M S   &   C I N E M A T I C S
        //////////////////////////////////////
        animations = new EState().SetCallbacks
        (
            _enter: () => { },
            _exit: () => { },
            _tick: () => { }
        );

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
