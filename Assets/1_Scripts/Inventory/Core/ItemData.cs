using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] string item_name;
    [SerializeField] string description;
    [SerializeField] int maxStack;
    [SerializeField] Sprite image;
    [SerializeField] ItemRecolectable model;
    [SerializeField] UsabeBehaviour behaviour;

    public string Name { get { return item_name; } }
    public string Description { get { return description; } }
    public int MaxStack { get { return maxStack; } }
    public Sprite Image { get => image; }
    public ItemRecolectable Model { get { return model; } }
    public UsabeBehaviour Behaviour { get { return behaviour; } }

}

