using AI.Tools;
using UnityEngine;
using TMPro;

public class GameController : MonoSingleton<GameController>
{
    FSMLite fsm;

    [SerializeField] ThirdPersonCharacter character;

    EState playing;
    EState paused;
    EState inventories;
    EState contextual;
    EState deb45;

    [SerializeField] TextMeshProUGUI state_debug;

    public override void SingletonAwake()
    {

    }

    private void Start()
    {
        CameraFollow.Instance.Activate(true);

        //////////////////////////////////////
        /// P L A Y I N G
        //////////////////////////////////////
        playing = new EState("playing").SetCallbacks
        (
            _enter: () =>
            {
                character.SetStateTo_CharControl();
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.thirdPersonCam);
                CameraFollow.Instance.ActiveCursor(false);
                character.Activate_Modules();
            },
            _exit: () =>
            {
                character.Deactivate_Modules();
                
            },
            _tick: () =>
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    _changeState(inventories);
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _changeState(deb45);
                }
            }
        );

        //////////////////////////////////////
        /// P A U S E D
        //////////////////////////////////////
        paused = new EState("paused").SetCallbacks
        (
            _enter: () => { Time.timeScale = 0; },
            _exit: () => { Time.timeScale = 1; },
            _tick: () => { }
        );

        //////////////////////////////////////
        /// D E B U G  4 5
        //////////////////////////////////////
        deb45 = new EState("Debug45").SetCallbacks
        (
            _enter: () =>
            {
                CameraFollow.Instance.ActiveCursor(true);
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.debug45);
                character.Activate_Modules();
            },
            _exit: () =>
            {
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.thirdPersonCam);
            },
            _tick: () =>
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    _changeState(playing);
                }
            }
        );

        //////////////////////////////////////
        /// I N V E N T O R I E S
        //////////////////////////////////////
        inventories = new EState("Inventories").SetCallbacks
        (
            _enter: () => 
            {
                character.SetStateTo_Menues();
                CameraFollow.Instance.ActiveCursor(true);
                PlayerPanel.Instance.Open();
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.lookAtPlayer);
            },
            _exit: () => 
            {
                UIGlobalData.CloseAllUIComponents();
            },
            _tick: () =>
            {
                if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.I))
                {
                    _changeState(playing);
                }
            }
        );

        //////////////////////////////////////
        /// C O N T E X T U A L
        //////////////////////////////////////
        contextual = new EState("contextual").SetCallbacks
        (
            _enter: () =>
            {
                character.SetStateTo_Menues();
                CameraFollow.Instance.ActiveCursor(true);
                PlayerPanel.Instance.Open();
                CameraFollow.Instance.ChangeMode(CameraFollow.CameraMode.none);
            },
            _exit: () =>
            {
                UIGlobalData.CloseAllUIComponents();
            },
            _tick: () =>
            {
                if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.I))
                {
                    _changeState(playing);
                }
            }
        );

        character.StartPlayer();

        fsm = new FSMLite(playing, DEBUG_OnChangeState);
        fsm.StartFSM();
    }

    public void ChangeToInventories()
    {
        _changeState(inventories);
    }

    public void ChangeToPlay()
    {
        _changeState(playing);
    }
    public void ChangeToContextual()
    {
        _changeState(contextual);
    }

    

    void _changeState(EState state)
    {
        fsm.ChangeTo(state);
    }

    void Update()
    {
        fsm.UpdateFSM();
    }

    #region DEBUGS
    void DEBUG_OnChangeState(string newstate)
    {
        if (state_debug != null) state_debug.text = newstate;
    }
    #endregion
}
