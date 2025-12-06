using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItemInfo
{

    Dictionary<int, EquipableBehaviour> pool;

    Transform parent;

    int currentActiveID = -1;
    public int CurrentID { get { return currentActiveID; } }

    EquipableBehaviour current;

    public EquipedItemInfo(Transform _parent)
    {
        pool = new Dictionary<int, EquipableBehaviour>();
        currentActiveID = -1;
        current = null;
        this.parent = _parent;
    }

    public UseResult Equip(int _itemID, EquipableBehaviour _behaviour)
    {
        if (_behaviour == null) return UseResult.Fail;

        if (!pool.TryGetValue(_itemID, out EquipableBehaviour instance_to_equip))
        {
            instance_to_equip = GameObject.Instantiate(_behaviour, parent);
            pool.Add(_itemID, instance_to_equip);
        }

        // Si ya es la misma instancia y mismo ID, no hacer nada (toggle lo gestiona el manager)
        if (current != null && current == instance_to_equip && currentActiveID == _itemID)
        {
            return UseResult.Success;
        }

        if (current != null && current != instance_to_equip)
        {
            current.UnEquip();
            if (current.gameObject != null) 
                current.gameObject.SetActive(false);
        }


        LogicEquip(_itemID, instance_to_equip);

        return UseResult.Success;
    }

    public UseResult UnEquipCurrent()
    {
        if (current == null || currentActiveID == -1)
            return UseResult.Fail;

        current.UnEquip();

        if (current.gameObject != null) 
            current.gameObject.SetActive(false);

        current = null; 
        currentActiveID = -1;

        return UseResult.Success;
    }

    public bool SameID(int ID)
    {
        return currentActiveID == ID;
    }
    public bool Ocuppied()
    {
        return currentActiveID != -1;
    }

    bool Exist(int ID)
    {
        return pool.ContainsKey(ID);
    }

    void LogicEquip(int _itemID, EquipableBehaviour _behaviour)
    {
        currentActiveID = _itemID;
        current = _behaviour;
        current.gameObject.SetActive(true);
        current.Equip(_itemID);
    }
}

public enum EquipableType
{
    oneHand = 0,
    twoHand = 1,
    head = 2,
    chest = 3,
    pants = 4,
    back = 5,
    foots = 6,
    space1 = 7,
    space2 = 8,
    ring1 = 9,
    ring2 = 10,
    neck = 11
}
