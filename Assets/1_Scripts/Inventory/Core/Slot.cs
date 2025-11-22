using UnityEngine;
using UnityEngine.WSA;

// Item individual por inventario, por entidad, serializable para guardar partida

public class Slot
{
    [SerializeField] int index_id;
    [SerializeField] int quantity;
    [SerializeField] int[] qualitystates; // puede tener varios estados a la vez


    public Slot(int index_id, int quantity, int[] qualitystates)
    {
        this.index_id = index_id;
        this.quantity = quantity;
        this.qualitystates = qualitystates;
    }

    public void EmptyStack()
    {
        index_id = -1;
        quantity = 0;
        qualitystates = null;
    }
    public int IndexID { get { return index_id; } }
    public int Quantity { 
        get 
        { 
            return quantity; 
        }
    }
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
        if (index_id == -1)
            throw new System.InvalidOperationException("Primero modificar el Indice antes de agregar al inventario.");

        Item item = InventoryDataCenter.Get_Valid_Item_ByID(index_id);
        if (item == null) throw new System.Exception("Item Invalido");

        int espacioDisponible = item.MaxStack - quantity;

        if (espacioDisponible <= 0)
            return toAdd; // No hay espacio, todo lo que entra se devuelve

        int quantToAdd = Mathf.Min(toAdd, espacioDisponible);
        quantity += quantToAdd;

        return toAdd - quantToAdd; // Devuelve lo que no pudo agregarse (0 si todo entró)
    }

    public int Query_CanAdd(int _quant, int ID)
    {
        if (_quant < 0)
            throw new System.ArgumentOutOfRangeException(nameof(_quant), "La cantidad a consultar no puede ser negativa.");
        if (ID < 0)
            throw new System.ArgumentOutOfRangeException(nameof(ID), "El ID no puede ser negativo.");

        // slot vacío: simular que se puede crear una nueva pila del item consultado
        if (index_id == -1)
        {
            Item item = InventoryDataCenter.Get_Valid_Item_ByID(ID);
            if (item == null) return _quant;

            int available = item.MaxStack; // espacio total en una pila nueva

            if (available <= 0)
                return _quant;

            int quantToAdd = Mathf.Min(_quant, available);
            return _quant - quantToAdd;
        }

        // slot con otro item: no aporta espacio para este ID
        if (index_id != ID)
            return _quant;

        // slot con el mismo ID: calcular espacio restante en la pila existente
        Item existingItem = InventoryDataCenter.Get_Valid_Item_ByID(index_id);
        if (existingItem == null)
            return _quant;

        int avaliableInStack = existingItem.MaxStack - quantity;

        if (avaliableInStack <= 0)
            return _quant; // no hay espacio

        int addable = Mathf.Min(_quant, avaliableInStack);
        return _quant - addable; // devuelve lo que no pudo agregarse (0 si todo entra)
    }

    public int TakeFromStack(int toRemove)
    {
        if (toRemove < 0)
            throw new System.ArgumentOutOfRangeException(nameof(toRemove), "La cantidad a eliminar no puede ser negativa.");

        if (toRemove == 0)
            return 0;

        if (index_id == -1 || quantity <= 0)
        {
            EmptyStack();
            return toRemove;
        }

        if (toRemove >= quantity)
        {
            int remain = toRemove - quantity;
            EmptyStack();
            return remain;
        }

        quantity -= toRemove;
        return 0;

    }

    public static Slot[] EmptyInventory(int capacity)
    {
        Slot[] emptyInventory = new Slot[capacity];
        for (int i = 0; i < emptyInventory.Length; i++)
            emptyInventory[i] = new Slot(-1, 0, null);
        return emptyInventory;
    }
}

public static class SlotExtensions
{
    public static bool CanNOTAddInThisSlot(this Slot slot, int IndexIDToAdd)
    {
        return slot.IndexID != -1 && slot.IndexID != IndexIDToAdd;
    }

    public static bool IsAEmptySlot(this Slot slot)
    {
        return slot.IndexID == -1;
    }
}