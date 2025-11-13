using UnityEngine;

public class Item
{
    [SerializeField] string item_name;
    [SerializeField] string description;
    [SerializeField] int maxStack;
    [SerializeField] Sprite image;

    public Item(string name, string description, int maxStack, Sprite image)
    {
        this.item_name = name;
        this.description = description;
        this.maxStack = maxStack;
        this.image = image;
    }

    public string Name { get { return item_name; } }
    public string Description { get { return description; } }
    public int MaxStack { get { return maxStack; } }
    public Sprite Image { get => image; }

    static Item itemNull = null;
    public static Item ItemNull
    {
        get
        {
            if (itemNull == null)
            {
                itemNull = new Item("NULL","NULL",0,null);
            }
            return itemNull;
        }
    }

}
