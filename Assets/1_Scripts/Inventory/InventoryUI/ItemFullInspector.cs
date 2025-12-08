using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemFullInspector : MonoSingleton<ItemFullInspector>
{
    [SerializeField] CanvasGroup group;

    //Slot
    [SerializeField] TextMeshProUGUI txt_index_id;
    [SerializeField] TextMeshProUGUI txt_quantity;
    [SerializeField] TextMeshProUGUI txt_parameters;
    [SerializeField] TextMeshProUGUI txt_guid;

    //Item Data
    [SerializeField] TextMeshProUGUI item_name;
    [SerializeField] TextMeshProUGUI item_ID;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI maxStack;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI model;
    [SerializeField] TextMeshProUGUI usable;
    [SerializeField] TextMeshProUGUI equipable;
    [SerializeField] TextMeshProUGUI type;
    [SerializeField] TextMeshProUGUI parameters;

    public override void SingletonAwake()
    {
        Instance.group.alpha = 0;
    }

    public static void Inspect(Slot slot)
    {
        Instance._Inspect(slot);
    }
    void _Inspect(Slot slot)
    {
        group.alpha = 1;

        var data = InventoryDataCenter.Get_Data_ByID(slot.IndexID);

        //Slot
        txt_index_id.text = slot.IndexID.ToString();
        txt_quantity.text = slot.Quantity.ToString();
        txt_parameters.text = slot.Parameters.ToString();
        txt_guid.text = slot.GUID.ToString();

        //Item Data
        item_name.text = data.Name;
        item_ID.text = data.ItemID.ToString();
        description.text = data.Description;
        maxStack.text = data.MaxStack.ToString();
        image.sprite = data.Image ;
        model.text = data.Model != null ? data.Model.name : "NULL";
        usable.text = data.Usable != null ? data.Usable.name : "NULL";
        equipable.text = data.Equipable != null ? data.Equipable.name : "NULL";
        type.text = data.Equipable != null ? data.Equipable.Type.ToString() : "NULL";
        parameters.text = data.Parameters.ToString();
    }

    public static void StopInspect()
    {
        Instance.group.alpha = 0;
    }
}
