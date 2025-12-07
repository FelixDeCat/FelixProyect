using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    int indexInContainer = -1;
    [SerializeField] TextMeshProUGUI txt_quant;
    [SerializeField] Image image;
    [SerializeField] Action<int> onPointerEnter;
    [SerializeField] Action<int> onPointerExit;
    [SerializeField] Action<int, int> onPointerDown;
    [SerializeField] Action<int, int> onPointerUp;

    [SerializeField] TextMeshProUGUI debug;

    Color color_transp = new Color(0,0,0,0);

    public void Set_ContainerIndex(int index)
    {
        indexInContainer = index;
    }
    public void RemoveContainerID()
    {
        indexInContainer = -1;
    }
    public void Set_Image(Sprite sp)
    {
        image.sprite = sp;
        image.color = Color.white;
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
        image.color = color_transp;
        txt_quant.text = string.Empty;
        debug.text = string.Empty;
    }

    public void SetDebug(string deb)
    {
        debug.text = deb;
    }

    public void SetCallback_PointerEnterExit(Action<int> onEnter, Action<int> onExit)
    {
        onPointerEnter = onEnter;
        onPointerExit = onExit;
    }
    public void SetCallback_PointerDownUp(Action<int,int> _onPointerDown, Action<int,int> _onPointerUp)
    {
        onPointerDown = _onPointerDown;
        onPointerUp = _onPointerUp;
    }

    #region Enter & Exit
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter.Invoke(indexInContainer);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        onPointerExit.Invoke(indexInContainer);
    }
    #endregion

    #region Down & Up
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // pointerId -1 Click Izquierdo
        // pointerId -2 Click Derecho
        // pointerId -3 Click Central
        // pointerId 0 dedo 0
        // pointerId 1 dedo 1
        onPointerDown.Invoke(indexInContainer, eventData.pointerId);
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        onPointerUp.Invoke(indexInContainer, eventData.pointerId);
    }
    #endregion
}
