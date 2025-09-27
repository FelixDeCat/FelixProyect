using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if (container[i].IndexID == Item.INDEX_EMPTY)
            {
                slots[i].Set_Empty();
            }
            else
            {
                slots[i].Set_Image(InventorySystem.DB[container[i].IndexID].Image);
                slots[i].Set_Quantity(container[i].Quantity);
            }     
        }
    }
}
