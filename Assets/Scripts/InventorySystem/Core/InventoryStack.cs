using UnityEngine;

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

        Item item = InventoryAgentExample.DB[index_id];
        int espacioDisponible = item.MaxStack - quantity;

        if (espacioDisponible <= 0)
            return toAdd; // No hay espacio, todo lo que entra se devuelve

        int quantToAdd = Mathf.Min(toAdd, espacioDisponible);
        quantity += quantToAdd;

        return toAdd - quantToAdd; // Devuelve lo que no pudo agregarse (0 si todo entr�)
    }

    public static InventoryStack[] EmptyInventory(int capacity)
    {
        InventoryStack[] emptyInventory = new InventoryStack[capacity];
        for (int i = 0; i < emptyInventory.Length; i++)
            emptyInventory[i] = new InventoryStack(Item.INDEX_EMPTY, 0, null);
        return emptyInventory;
    }
}