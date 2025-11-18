using UnityEngine;
using AI.Tools;

public enum ToolType
{
    none,
    weapon,
    buildTool
}

[System.Serializable]
public class ToolSSM : IPausable, IUpdateable, IStarteable
{
    bool isPause;
    SSM ssm;

    [SerializeField] BuildToolState buildToolState;
    [SerializeField] WeaponState weaponState;
    [SerializeField] InventoryState inventoryState;

    private static ToolSSM playerInstance;

    IPausable[] statesPausables;

    void IStarteable.Start()
    {
        ssm = new SSM(weaponState);

        ssm.StartFSM();
        playerInstance = this;

        statesPausables = new IPausable[3];
        statesPausables[0] = buildToolState;
        statesPausables[1] = weaponState;
        statesPausables[2] = inventoryState;
    }

    void IUpdateable.Tick(float delta)
    {
        if (isPause) return;
        ssm.UpdateFSM();
    }
    public static void ChangeToInventoryState()
    {
        playerInstance.ssm.ChangeTo(playerInstance.inventoryState);
    }
    public static void ChangeToWeaponState()
    {
        playerInstance.ssm.ChangeTo(playerInstance.weaponState);
    }
    public static void ChangeToToolState()
    {
        playerInstance.ssm.ChangeTo(playerInstance.buildToolState);
    }


    void IPausable.Pause()
    {
        foreach (var p in statesPausables) 
            p.Pause();
    }

    void IPausable.Resume()
    {
        foreach (var p in statesPausables)
            p.Resume();
    }
}
