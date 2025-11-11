
using UnityEngine;

public class ThirdPersonCharacter : MonoBehaviour
{
    [SerializeField] ModuleHandler moduleHandler;
    [SerializeField] MousePointModule mousePoint;
    [SerializeField] CharacterController characterController;
    [SerializeField] GroundModule groundModule;
    [SerializeField] InteractModule interactModule;

    private void Awake()
    {
        moduleHandler.AddModule(mousePoint);
        moduleHandler.AddModule(characterController);
        moduleHandler.AddModule(groundModule);
        moduleHandler.AddModule(interactModule);

        characterController.IsGroundedCallback(groundModule.IsGrounded);
    }
    private void Start()
    {
        moduleHandler.Start();
    }
    void Activate()
    {
        moduleHandler.Activate();
    }
    void Deactivate()
    {
        moduleHandler.Deactivate();
    }
    void Pause()
    {
        moduleHandler.Pause();
    }
    void Resume()
    {
        moduleHandler.Resume();
    }
    void ResetModules()
    {
        moduleHandler.Reset();
    }
    void Update()
    {
        moduleHandler.Tick(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        moduleHandler.FixedTick(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        mousePoint.OnDrawGizmos();
    }
}
