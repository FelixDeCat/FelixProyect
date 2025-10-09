using System.Collections.Generic;
using UnityEngine;

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
        Item itemSelected = InventoryAgentExample.DB[index];

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