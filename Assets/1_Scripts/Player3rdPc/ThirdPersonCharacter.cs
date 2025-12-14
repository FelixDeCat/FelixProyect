
using AI.Tools;
using UnityEngine;

public class ThirdPersonCharacter : MonoBehaviour, IPausable
{
    [SerializeField] ModuleHandler          moduleHandler;
    [SerializeField] MousePointModule       mousePoint;
    [SerializeField] GroundModule           groundModule;
    [SerializeField] InteractModule         interactModule;
    [SerializeField] EquipDataManager       equipDataManager;
    [SerializeField] InventoryAgent         inventoryAgent;
    [SerializeField] PlayerView             view;
    [SerializeField] DamageSensor           dmgSensor;

    // SSM
    [SerializeField] BuildMode_State        stateBuildMode;
    [SerializeField] CharControl_State      stateCharControl;
    [SerializeField] Inventory_State        stateMenues;

    [Header("Referencias Sueltas")]

    bool isPaused = false;
    bool isActive = false;

    /// brain
    SSM ssm;

    private void Awake()
    {
        moduleHandler.AddModule(groundModule);
        moduleHandler.AddModule(interactModule);
        moduleHandler.AddModule(equipDataManager);
        moduleHandler.AddModule(inventoryAgent);

        // SSM States
        moduleHandler.AddModule(stateCharControl);
        moduleHandler.AddModule(stateBuildMode);
        moduleHandler.AddModule(stateMenues);

        stateCharControl.IsGroundedCallback(groundModule.IsGrounded);
        stateCharControl.SetView(view);
        dmgSensor.SubscribeToDmgElement(OnElementDMG);
        stateCharControl.SubscribeToDoHit(dmgSensor.ExecuteQuery);

        interactModule.SetMousePointModule(mousePoint);
    }


    [SerializeField] DamageData damageDataExample;
    void OnElementDMG(IDamageable damaged)
    {
        if (damaged != null)
        {
            damaged.Damage(damageDataExample);
        }
    }

    private void Start()
    {
        moduleHandler.Start();
        SetBrain();
    }

    public void SetBrain()
    {
        ssm = new SSM(stateCharControl);
        ssm.StartFSM();
    }
    public void Activate()
    {
        isActive = true;
        moduleHandler.Activate();
    }
    public void Deactivate()
    {
        isActive = false;
        moduleHandler.Deactivate();
    }

    void ResetModules()
    {
        moduleHandler.Reset();
    }
    void Update()
    {
        if (!isActive) return;
        if (isPaused) return;
        moduleHandler.Tick(Time.deltaTime);
        ssm.UpdateFSM();
    }
    private void FixedUpdate()
    {
        if(!isActive) return;
        if (isPaused) return;
        moduleHandler.FixedTick(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (!isActive) return;
        //mousePoint.OnDrawGizmos();
        dmgSensor.DrawGizmosManually();
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
