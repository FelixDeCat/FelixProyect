
using UnityEngine;

public class ThirdPersonCharacter : MonoBehaviour, IPausable
{
    [SerializeField] ModuleHandler          moduleHandler;
    [SerializeField] MousePointModule       mousePoint;
    [SerializeField] CharacterController    characterController;
    [SerializeField] GroundModule           groundModule;
    [SerializeField] InteractModule         interactModule;
    [SerializeField] ToolSSM                toolFSM;

    bool isPaused = false;

    private void Awake()
    {
        moduleHandler.AddModule(mousePoint);
        moduleHandler.AddModule(characterController);
        moduleHandler.AddModule(groundModule);
        moduleHandler.AddModule(interactModule);
        moduleHandler.AddModule(toolFSM);

        characterController.IsGroundedCallback(groundModule.IsGrounded);
    }
    private void Start()
    {
        moduleHandler.Start();
    }
    public void Activate()
    {
        moduleHandler.Activate();
    }
    public void Deactivate()
    {
        moduleHandler.Deactivate();
    }
    void ResetModules()
    {
        moduleHandler.Reset();
    }
    void Update()
    {
        if (isPaused) return;
        moduleHandler.Tick(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (isPaused) return;
        moduleHandler.FixedTick(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        mousePoint.OnDrawGizmos();
    }

    void IPausable.Pause()
    {
        moduleHandler.Pause();
        isPaused = true;
    }

    void IPausable.Resume()
    {
        moduleHandler.Resume();
        isPaused = false;
    }
}
