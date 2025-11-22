using System;
using System.Collections.Generic;
using UnityEngine;


public enum ActionResultOnUse
{
    nothing,
    consume,
    equip
}

public class ItemUseManager : MonoSingleton<ItemUseManager>
{

    Dictionary<EquipableType, IEquipable> slots = new Dictionary<EquipableType, IEquipable>();
    Dictionary<int, UsabeBehaviour> behaviours = new Dictionary<int, UsabeBehaviour>();

    public override void SingletonAwake()
    {
        #region Initialize Dictionary
        slots.Add(EquipableType.oneHand, null);
        slots.Add(EquipableType.twoHand, null);
        slots.Add(EquipableType.head, null);
        slots.Add(EquipableType.chest, null);
        slots.Add(EquipableType.pants, null);
        slots.Add(EquipableType.back, null);
        slots.Add(EquipableType.foots, null);
        slots.Add(EquipableType.space1, null);
        slots.Add(EquipableType.space2, null);
        slots.Add(EquipableType.ring1, null);
        slots.Add(EquipableType.ring2, null);
        slots.Add(EquipableType.neck, null);
        #endregion
    }

    public UseResult UseItem(IUsable usable)
    {
        if (usable is IEquipable equipNew)
        {
            var type = equipNew.Type;

            if (!slots.ContainsKey(type))
            {
                Debug.LogWarning($"ItemUseManager: ranura no registrada: {type}");
                return UseResult.Fail;
            }

            if (slots[type] != null)
            {
                if (slots[type] == equipNew)
                {
                    // Toogle mismo item
                    slots[type].UnEquip();
                    slots[type] = null;
                    
                }
                else
                {
                    // swap con otro item
                    slots[type].UnEquip();
                }
            }

            slots[type] = equipNew;
            equipNew.Equip();

            return UseResult.Success;
        }
        else 
        {
            try
            {
                return usable.Use();
            }
            catch (System.Exception ex)
            {
                return UseResult.Fail;
            }
            
        }
    }

    public UseResult UseBehaviour(int index, UsabeBehaviour behaviour)
    {
        if (!behaviours.ContainsKey(index))
        {
            behaviours.Add(index, Instantiate(behaviour, this.transform));
        }

        return UseItem(behaviours[index]);
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