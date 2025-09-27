using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // lo agregamos desde afuera
    [SerializeField] ItemData[] dataBase;
    public static Item[] DB;

    // ejemplo de inventario
    public Container bag;
    [SerializeField] UIContainer uiContainer;

    void Start()
    {
        DB = new Item[dataBase.Length];
        for (int i = 0; i < dataBase.Length; i++)
        {
            DB[i] = dataBase[i].data;
        }

        bag = new Container(32);

        uiContainer.Intialize(bag);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bag.Add(Random.Range(0, DB.Length), Random.Range(1,4), 0);
            uiContainer.Refresh(bag);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}

// inventario de cada entidad o cofre
[System.Serializable]
public class Container
{
    [SerializeField] int capacity;
    [SerializeField] List<InventoryStack> items;

    public Container(int capacity)
    {
        this.capacity = capacity;
        items = new List<InventoryStack>(InventoryStack.EmptyInventory(capacity));
    }
    public InventoryStack this[int index]
    {
        get => items[index];
        private set => items[index] = value;
    }
    public int MaxCapacity { get { return capacity; } }
    public int Add(int index, int quantity, params int[] states)
    {
        if (index < 0) throw new System.IndexOutOfRangeException("El indice a agrega no puede ser menor a cero");

        int resultant = quantity;
        Item itemSelected = InventorySystem.DB[index];

        for (int i = 0; i < items.Count; i++)
        {
            // si ya no me queda para agregar me salgo
            if (resultant <= 0) return 0;

            if (items[i].IndexID != Item.INDEX_EMPTY && items[i].IndexID != index) continue;

            //si era casillero vacio
            if (items[i].IndexID == Item.INDEX_EMPTY)
            {
                items[i].Set(index, 0, states);
            }

            resultant = items[i].AddQuantity(resultant);
        }

        return resultant;
    }
}

// Item individual por inventario, por entidad, serializable para guardar partida
[System.Serializable]
public class InventoryStack
{
    [SerializeField] int index_id;
    [SerializeField] int quantity;
    [SerializeField] int[] qualitystates; // puede tener varios estados a la vez

    public InventoryStack(int index_id, int quantity, int[] qualitystates)
    {
        this.index_id = index_id;
        this.quantity = quantity;
        this.qualitystates = qualitystates;
    }

    public void EmptyStack()
    {
        index_id = Item.INDEX_EMPTY;
        quantity = 0;
        qualitystates = null;
    }
    public int IndexID { get { return index_id; } }
    public int Quantity { get { return quantity; } }
    public int[] QualityStates { get { return qualitystates; } }

    public void Set(int index, int quantity, params int[] qualityStates)
    {
        this.index_id = index;
        this.quantity = quantity;
        this.qualitystates = qualityStates;
    }

    public int AddQuantity(int toAdd)
    {
        if (toAdd < 0)
            throw new System.ArgumentOutOfRangeException(nameof(toAdd), "La cantidad a agregar no puede ser negativa.");
        if (index_id == Item.INDEX_EMPTY)
            throw new System.InvalidOperationException("Primero modificar el Indice antes de agregar al inventario.");

        Item item = InventorySystem.DB[index_id];
        int espacioDisponible = item.MaxStack - quantity;

        if (espacioDisponible <= 0)
            return toAdd; // No hay espacio, todo lo que entra se devuelve

        int quantToAdd = Mathf.Min(toAdd, espacioDisponible);
        quantity += quantToAdd;

        return toAdd - quantToAdd; // Devuelve lo que no pudo agregarse (0 si todo entró)
    }

    public static InventoryStack[] EmptyInventory(int capacity)
    {
        InventoryStack[] emptyInventory = new InventoryStack[capacity];
        for (int i = 0; i < emptyInventory.Length; i++)
            emptyInventory[i] = new InventoryStack(Item.INDEX_EMPTY ,0, null);
        return emptyInventory;
    }
}

// estados del los items
public enum QualityStates
{
    rotten = 0,
    burned = 1,
    cooked = 2,
    dried = 3,
    excellent = 4
}


// Generacion de Contedido de Items en el editor
[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public Item data;
}

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
