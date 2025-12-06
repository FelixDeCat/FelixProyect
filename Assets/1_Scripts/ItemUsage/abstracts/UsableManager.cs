using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UsableManager : MonoBehaviour
{
    Dictionary<int, UsableBehaviour> usables = new Dictionary<int, UsableBehaviour>();

    public UseResult TryUse(UsableBehaviour usable, int ID)
    {
        int KEY = usable.GetUniqueBehaviourID();
        if (KEY < 0)
        {
            KEY = 1000 + usable.GetInstanceID();
        }

        if (!usables.ContainsKey(KEY))
        {
            usables.Add(KEY, Instantiate(usable, this.transform));
        }

        return usable.Use(ID);
    }
}
