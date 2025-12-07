
using UnityEngine;

public class ThirdPersonCharacter : MonoBehaviour, IPausable
{
    [SerializeField] ModuleHandler          moduleHandler;
    [SerializeField] MousePointModule       mousePoint;
    [SerializeField] CharacterController    characterController;
    [SerializeField] GroundModule           groundModule;
    [SerializeField] InteractModule         interactModule;
    [SerializeField] ToolSSM                toolFSM;
    [SerializeField] EquipDataManager       equipDataManager;
    [SerializeField] InventoryAgent         inventoryAgent;

    [Header("Referencias Sueltas")]

    bool isPaused = false;

    private void Awake()
    {
        moduleHandler.AddModule(characterController);
        moduleHandler.AddModule(groundModule);
        moduleHandler.AddModule(interactModule);
        moduleHandler.AddModule(toolFSM);
        moduleHandler.AddModule(equipDataManager);
        moduleHandler.AddModule(inventoryAgent);

        characterController.IsGroundedCallback(groundModule.IsGrounded);
        interactModule.SetMousePointModule(mousePoint);
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
        //*mousePoint.OnDrawGizmos();
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
