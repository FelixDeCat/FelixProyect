using TMPro;
using UnityEngine;

public class TooltipInGame : MonoSingleton<TooltipInGame>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI tooltipText;

    public override void SingletonAwake()
    {
        
    }

    public void ShowTooltip(string info, Vector2 screenPos)
    {
        tooltipText.text = info;
        canvasGroup.alpha = 1;
        transform.position = screenPos;
    }
    public void HideTooltip()
    {
        tooltipText.text = string.Empty;
        canvasGroup.alpha = 0;
        transform.position = Vector2.zero; 
    }
}
