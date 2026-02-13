using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IndexableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button myButton;
    [SerializeField] TextMeshProUGUI myText;

    Action<int> callback;
    int index = -1;

    string tooltip = "";

    public void Set(Action<int> callback,int index, string text, string tooltip)
    {
        this.callback = callback;
        this.index = index;
        myText.text = text;
        this.tooltip = tooltip;
        myButton.onClick.AddListener(() => callback.Invoke(index));
    }

    public void Clear()
    {
        myButton.onClick.RemoveAllListeners();
        callback = null;
        index = -1;
        myText.text = string.Empty;
        tooltip = string.Empty;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TooltipInGame.Instance.ShowTooltip(tooltip, eventData.position);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TooltipInGame.Instance.HideTooltip();
    }
}
