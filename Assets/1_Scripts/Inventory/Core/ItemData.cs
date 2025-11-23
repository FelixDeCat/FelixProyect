using System;
using System.Collections.Generic;
using UnityEngine;

public enum KeyToAffect
{
    life,
    energy,
    speed,
    strenght,
    mana,
    command
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] string item_name;
    [SerializeField] string description;
    [SerializeField] int maxStack;
    [SerializeField] Sprite image;
    [SerializeField] ItemRecolectable model;
    [SerializeField] UsabeBehaviour behaviour;
    [SerializeField] List<Command> commands;

    public string Name { get { return item_name; } }
    public string Description { get { return description; } }
    public int MaxStack { get { return maxStack; } }
    public Sprite Image { get => image; }
    public ItemRecolectable Model { get { return model; } }
    public UsabeBehaviour Behaviour { get { return behaviour; } }
    public List<Command> Commands { get { return commands; } }

}

[Serializable]
public class Command
{
    public KeyToAffect KeyToAffect;
    public int value;
}

