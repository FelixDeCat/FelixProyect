using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class UsableManager
{
    [SerializeField] Transform parent;
    Dictionary<int, UsableBehaviourBase> usables = new Dictionary<int, UsableBehaviourBase>();

    public UseResult TryUse(UsableBehaviourBase usable, Slot slot)
    {
        int KEY = usable.GetUniqueBehaviourID();
        if (KEY < 0)
        {
            KEY = 1000 + usable.GetInstanceID();
        }

        if (!usables.ContainsKey(KEY))
        {
            usables.Add(KEY, GameObject.Instantiate(usable, parent));
        }

        return usable.Use(slot.IndexID);
    }
}
