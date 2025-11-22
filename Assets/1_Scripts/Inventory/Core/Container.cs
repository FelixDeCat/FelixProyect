using System;
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
    public int Add(int ID, int quant, params int[] states)
    {
        if (ID < 0) throw new System.IndexOutOfRangeException("El indice a agregar no puede ser menor a cero");

        int resultant = quant;
        Item itemSelected = InventoryDataCenter.Get_Valid_Item_ByID(ID);
        if (itemSelected == null) 
        {
            return quant;
        }

        for (int i = 0; i < items.Count; i++)
        {
            // si ya no me queda para agregar me salgo
            if (resultant <= 0) return 0;

            if (items[i].IndexID != -1 && items[i].IndexID != ID) continue;

            //si era casillero vacio
            if (items[i].IndexID == -1)
            {
                items[i].Set(ID, 0, states);
            }

            resultant = items[i].AddQuantity(resultant);
        }

        return resultant;
    }

    public bool HaveReceip(/*Receip*/)
    {
        return false;
    }
    public int CanAdd(int ID, int quant)
    {
        if (quant <= 0) throw new System.ArgumentException("La cantidad no puede ser cero");

        Item itm = InventoryDataCenter.Get_Valid_Item_ByID(ID);
        if (itm == null)
        {
            return quant;
        }

        int resultant = quant;

        for (int i = 0; i < items.Count; i++)
        {
            if (resultant <= 0) return 0;

            resultant = items[i].Query_CanAdd(resultant, ID);
        }
        return resultant; 
    }

    public bool TryAdd(int ID, int quant, out int remainder, params int[] states)
    {
        if (ID < 0) throw new System.IndexOutOfRangeException("El indice a agregar no puede ser menor a cero");
        if (quant < 0) throw new System.ArgumentOutOfRangeException(nameof(quant), "La cantidad no puede ser negativa.");
        if (quant == 0)
        {
            CustomConsole.LogError("Cantidad a agregar CERO");
            remainder = 0;
            return true;
        }

        Item itemSelected = InventoryDataCenter.Get_Valid_Item_ByID(ID);
        if (itemSelected == null)
        {
            CustomConsole.LogError("Item Invalido");
            remainder = quant;
            return false;
        }

        int resultant = quant;

        for (int i = 0; i < items.Count; i++)
        {
            if (resultant <= 0) break;

            if (items[i].CanNOTAddInThisSlot(ID)) continue;

            if (items[i].IndexID == -1) 
            {
                CustomConsole.Log("agregando en casillero vacio");
                items[i].Set(ID, 0, states); 
            }
            
        

            resultant = items[i].AddQuantity(resultant);
        }

        remainder = resultant;

        return remainder == 0;
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