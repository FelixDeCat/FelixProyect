using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] string item_name;
    [SerializeField] string description;
    [SerializeField] int maxStack;
    [SerializeField] Sprite image;

    public string Name { get { return item_name; } }
    public string Description { get { return description; } }
    public int MaxStack { get { return maxStack; } }
    public Sprite Image { get => image; }

}

