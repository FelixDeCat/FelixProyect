using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : UIBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    int indexInContainer = -1;
    [SerializeField] TextMeshProUGUI txt_quant;
    [SerializeField] Image image;
    [SerializeField] Action<int> onPointerEnter;
    [SerializeField] Action<int> onPointerExit;

    public void Set_ContainerIndex(int index)
    {
        indexInContainer = index;
    }
    public void Set_Image(Sprite sp)
    {
        image.sprite = sp;
    }
    public void Set_Quantity(int quant)
    {
        if (quant > 1)
        {
            txt_quant.text = quant.ToString();
        }
        else
        {
            txt_quant.text = string.Empty;
        }
    }
     
    public void Set_Empty()
    {
        image.sprite = null;
        txt_quant.text = string.Empty;
        indexInContainer = -1;
    }

    public void SetCallback_PointerEnterExit(Action<int> onEnter, Action<int> onExit)
    {
        onPointerEnter = onEnter;
        onPointerExit = onExit;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        onPointerEnter.Invoke(indexInContainer);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        onPointerExit.Invoke(indexInContainer);
    }
}
