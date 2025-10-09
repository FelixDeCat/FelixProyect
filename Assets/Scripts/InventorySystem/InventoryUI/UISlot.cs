using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public TextMeshProUGUI txt_quant;
    public Image image;


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
    }

}
