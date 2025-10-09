using UnityEngine;

// datos del items en si
[System.Serializable]
public class Item
{
    [SerializeField] string item_name;
    [SerializeField] string description;
    [SerializeField] int maxStack;
    [SerializeField] Sprite image;

    public Item(string name, string description, int maxStack)
    {
        this.item_name = name;
        this.description = description;
        this.maxStack = maxStack;
    }

    public string Name { get { return item_name; } }
    public string Description { get { return description; } }
    public int MaxStack { get { return maxStack; } }
    public Sprite Image { get => image; }

    public static int INDEX_EMPTY = -1;
    public static Item Empty
    {
        get
        {
            return new Item("empty", "empty", -1);
        }
    }
}
