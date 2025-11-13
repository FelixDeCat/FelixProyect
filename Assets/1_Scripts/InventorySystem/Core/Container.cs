using System.Collections.Generic;
using UnityEngine;

// inventario de cada entidad o cofre
public class Container
{
    [SerializeField] int capacity;
    [SerializeField] List<Slot> items;

    public Container(int capacity)
    {
        this.capacity = capacity;

        Slot[] emptyInventory = new Slot[capacity];
        for (int i = 0; i < emptyInventory.Length; i++)
            emptyInventory[i] = new Slot(-1, 0, null);

        items = new List<Slot>(emptyInventory);
    }
    public Slot this[int index]
    {
        get => items[index];
        private set => items[index] = value;
    }
    public int MaxCapacity { get { return capacity; } }
    public int Add(int index, int quantity, params int[] states)
    {
        if (index < 0) throw new System.IndexOutOfRangeException("El indice a agrega no puede ser menor a cero");

        int resultant = quantity;
        Item itemSelected = InventoryDataCenter.DB[index];

        for (int i = 0; i < items.Count; i++)
        {
            // si ya no me queda para agregar me salgo
            if (resultant <= 0) return 0;

            if (items[i].IndexID != -1 && items[i].IndexID != index) continue;

            //si era casillero vacio
            if (items[i].IndexID == -1)
            {
                items[i].Set(index, 0, states);
            }

            resultant = items[i].AddQuantity(resultant);
        }

        return resultant;
    }
    public int RemoveQuantityFromPosition(int index_container, int quant_to_remove)
    {
        if (index_container < 0 || index_container >= items.Count)
            throw new System.IndexOutOfRangeException("Índice fuera de rango");

        if (quant_to_remove < 0)
            throw new System.ArgumentOutOfRangeException(nameof(quant_to_remove), "La cantidad a eliminar no puede ser negativa.");

        if (quant_to_remove == 0) return 0;

        var slot = items[index_container];
        return slot.TakeFromStack(quant_to_remove);
    }
}