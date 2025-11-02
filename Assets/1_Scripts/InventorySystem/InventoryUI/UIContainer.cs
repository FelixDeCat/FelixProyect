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

    public void Intialize(Container container)
    {
        slots = new List<UISlot>(container.MaxCapacity);
        for (int i = 0; i < container.MaxCapacity; i++)
        {
            UISlot slot = GameObject.Instantiate(UIGlobalData.model_ui_slots, parent);
            slot.txt_quant.text = " ";
            slot.image.sprite = null;
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
        }
    }
}
