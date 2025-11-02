using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ModuleHandler
{

    List<IStarteable> starteables = new List<IStarteable>();
    List<IPausable> pausables = new List<IPausable>();
    List<IActivable> activables = new List<IActivable>();
    List<IUpdateable> updateables = new List<IUpdateable>();
    List<IReseteable> reseteables = new List<IReseteable>();
    List<IFixedUpdateable> fixedupdateables = new List<IFixedUpdateable>();

    public ModuleHandler AddModule(IModule mod)
    {
        if (mod is IStarteable starteable) starteables.Add(starteable);
        if (mod is IPausable pausable) pausables.Add(pausable);
        if (mod is IActivable activable) activables.Add(activable);
        if (mod is IUpdateable updateable) updateables.Add(updateable);
        if (mod is IReseteable reseteable) reseteables.Add(reseteable);
        if (mod is IFixedUpdateable fixedUp) fixedupdateables.Add(fixedUp);
        return this;
    }

    public void Start() => starteables.ForEach(x => x.Start());
    public void Activate() => activables.ForEach(x => x.Active());
    public void Deactivate() => activables.ForEach(x => x.Active());
    public void Pause() => pausables.ForEach(x => x.Pause());
    public void Resume() => pausables.ForEach(x => x.Resume());
    public void Reset() => reseteables.ForEach(x => x.ResetModule());
    public void Tick(float delta) => updateables.ForEach(x => x.Tick(delta));
    public void FixedTick(float delta) => fixedupdateables.ForEach(x => x.FixedTick(delta));
}
