using System;
using TMPro;
using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public int indexID = 0;
    
    [SerializeField] Interactable interactable;

    int maxStack = 1;
    [SerializeField] int currQ = 0;

    [SerializeField] bool debugQuantity;
    [SerializeField] TextMeshPro debText;

    [SerializeField] bool placeHolder = false;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        interactable.SetInteractable(OnRecolect, OnGetInfo);
    }

    private void Start()
    {
        if (placeHolder)
        {
            spriteRenderer.sprite = InventoryDataCenter.DB[indexID].Image;
        }
    }

    public int ActivateItemRecolectable(int _indexElement, int newQ = 1)
    {
        debText.gameObject.SetActive(true);

        int remain = newQ;
        indexID = _indexElement;
        maxStack = InventoryDataCenter.DB[indexID].MaxStack;

        if (newQ + currQ >= maxStack)
        {
            remain = (newQ + currQ) - maxStack;
            currQ = maxStack;

            debText.text = currQ.ToString() + " R: " + remain;
            return remain;
        }

        currQ = currQ + newQ;
        remain = 0;

        debText.text = currQ.ToString() + " R: " + remain;
        return remain;
    }
    public void ResetItemRecolectable()
    {
        maxStack = 0;
        currQ = 0;
        indexID = -1;
    }

    void OnRecolect()
    {

        int remain = InventoryAgent.InstanceAgent.TryAddElement(indexID, currQ);
        if (remain == currQ) return; //sonido no se puede agregar

        if (remain > 0)
        {
            CustomConsole.Log($"Se intentó agregar {currQ.ToString().Paint(Color.yellow)}, pero sobró {remain.ToString().Paint(Color.red)}");

            ItemSpawner.SpawnItem(indexID,
                Tools.Random_XZ_PosInBound(
                    center: this.transform.position,
                    radius: 2f),
                remain);
        }

        ItemSpawner.ReturnItem(indexID, this);
    }
    string OnGetInfo()
    {
        return InventoryDataCenter.DB[indexID].Name;
    }


}
