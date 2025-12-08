using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] string item_name;
    [SerializeField] int item_ID;
    [SerializeField] string description;
    [SerializeField] int maxStack;
    [SerializeField] Sprite image;
    [SerializeField] ItemRecolectable model;
    [SerializeField] UsableBehaviourBase usable;
    [SerializeField] EquipableBehaviourBase equipable;
    [SerializeField] string[] parameters;

    public string Name { get { return item_name; } }
    public int ItemID { get { return item_ID; } }
    public string Description { get { return description; } }
    public int MaxStack { get { return maxStack; } }
    public Sprite Image { get => image; }
    public ItemRecolectable Model { get { return model; } }
    public UsableBehaviourBase Usable { get { return usable; } }
    public EquipableBehaviourBase Equipable { get { return equipable; } }
    public string Parameters { get 
        {
            string res = String.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                res += "["+parameters[i]+"]";
            }
            return res;
        } 
    }

}

