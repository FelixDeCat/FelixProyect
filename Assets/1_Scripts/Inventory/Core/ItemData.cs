using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] string item_name;
    [SerializeField] string description;
    [SerializeField] int maxStack;
    [SerializeField] Sprite image;
    [SerializeField] ItemRecolectable model;
    [SerializeField] UsableBehaviour usable;
    [SerializeField] EquipableBehaviour equipable;
    [SerializeField] string[] parameters;

    public string Name { get { return item_name; } }
    public string Description { get { return description; } }
    public int MaxStack { get { return maxStack; } }
    public Sprite Image { get => image; }
    public ItemRecolectable Model { get { return model; } }
    public UsableBehaviour Usable { get { return usable; } }
    public EquipableBehaviour Equipable { get { return equipable; } }
    public string[] Parameters { get { return parameters; } }

}

