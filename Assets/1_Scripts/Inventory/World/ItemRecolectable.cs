using System;
using TMPro;
using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public int indexID = 0;
    public string GUID = "";
    
    [SerializeField] Interactable interactable;

    int maxStack = 1;
    [SerializeField] int currQ = 0;

    [SerializeField] string parameters = string.Empty;

    [SerializeField] bool debugQuantity;
    [SerializeField] TextMeshPro debText;

    [SerializeField] bool placeHolder = false;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Rigidbody Rigidbody;

    private void Awake()
    {
        interactable.SetInteractable(OnRecolect, OnGetInfo);
    }

    private void Start()
    {
        if (placeHolder)
        {
            spriteRenderer.sprite = InventoryDataCenter.Get_Data_ByID(indexID).Image;
        }
    }

    public int ActivateItemRecolectable(int _indexElement, int newQ = 1, string customGUID = "")
    {
        if(debText) debText.gameObject.SetActive(true);

        int remain = newQ;
        indexID = _indexElement;
        maxStack = InventoryDataCenter.Get_Data_ByID(indexID).MaxStack;

        GUID = customGUID;

        if (newQ + currQ >= maxStack)
        {
            remain = (newQ + currQ) - maxStack;
            currQ = maxStack;

            if (debText) debText.text = currQ.ToString() + " R: " + remain;
            return remain;
        }

        currQ = currQ + newQ;
        remain = 0;

        if (debText) debText.text = currQ.ToString() + " R: " + remain;
        return remain;
    }
    public void ApplyForce(Vector3 origin)
    {

    }
    public void ResetItemRecolectable()
    {
        maxStack = 0;
        currQ = 0;
        indexID = -1;
    }

    void OnRecolect()
    {

        int remain = InventoryAgent.InstanceAgent.TryAddElement(indexID, currQ, parameters, GUID);
        if (remain == currQ) return; //sonido no se puede agregar

        if (remain > 0)
        {
            CustomConsole.Log($"Se intentó agregar {currQ.ToString().Paint(Color.yellow)}, pero sobró {remain.ToString().Paint(Color.red)}");

            ItemSpawner.SpawnItem
            (
                indexID: indexID,
                position: Tools.Random_XZ_PosInBound
                (
                    center: this.transform.position,
                    radius: 2f
                ),
                quantity: remain,
                group: true,
                customGUID: GUID
            );
        }

        ItemSpawner.ReturnItem(indexID, this);
    }
    string OnGetInfo()
    {
        return InventoryDataCenter.Get_Data_ByID(indexID).Name;
    }


}
