using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UIContainer:

// El tipo de Container deberia corresponder al mismo tipo que lo renderiza, por lo que las cantidades
// no deberian variar, por ejemplo, el Container del personaje siempre corresponde a la misma UI

// esta clase se encarga de Renderizar un container completo
// dentro de este cada uno de los items tiene su nombre, imagen y cantidad

public class UIContainer : MonoBehaviour
{
    [SerializeField] RectTransform parent;
    List<UISlot> slots;
    Func<int,Slot> peek = null;

    public void Intialize(Container container)
    {
        peek = ID =>
        {
            return container[ID];
        };
        slots = new List<UISlot>(container.MaxCapacity);
        for (int i = 0; i < container.MaxCapacity; i++)
        {
            UISlot slot = GameObject.Instantiate(UIGlobalData.model_ui_slots, parent);
            slot.Set_Empty();
            slots.Add(slot);
        }
        _refresh(container);
    }

    public void Refresh(Container container)
    {
        _refresh(container);
    }

    void _refresh(Container container)
    {
        if (container.MaxCapacity != slots.Count) throw new System.Exception("No tiene la misma cantidad de elementos");

        for (int i = 0; i < container.MaxCapacity; i++)
        {
            if (container[i].IndexID == -1)
            {
                slots[i].Set_Empty();
            }
            else
            {
                slots[i].Set_Image(InventoryDataCenter.DB[container[i].IndexID].Image);
                slots[i].Set_Quantity(container[i].Quantity);
                
            }
            slots[i].Set_ContainerIndex(i);
            slots[i].SetCallback_PointerEnterExit(OnPointerEnter, OnPointerExit);
        }
    }

    public void OnPointerEnter(int indexInContainer)
    {
        var slot = peek(indexInContainer);
        if (slot.IndexID == -1) 
        {
            CustomConsole.LogStaticText(1, "Empty");
            return;
        }
        CustomConsole.LogStaticText(1, InventoryDataCenter.DB[slot.IndexID].Name);
        CustomConsole.LogStaticText(2, InventoryDataCenter.DB[slot.IndexID].MaxStack.ToString());
    }
    public void OnPointerExit(int indexInContainer)
    {
        CustomConsole.LogStaticText(1, String.Empty);
        CustomConsole.LogStaticText(2, String.Empty);  
    }
}
