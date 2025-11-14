using System;
using TMPro;
using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public int indexElement = 0;
    
    [SerializeField] Interactable interactable;

    int maxStack = 1;
    [SerializeField] int currQ = 0;

    [SerializeField] bool debugQuantity;
    [SerializeField] TextMeshPro debText;

    private void Awake()
    {
        interactable.SetInteractable(OnRecolect, OnGetInfo);
    }

    public int ActivateItemRecolectable(int _indexElement, int newQ = 1)
    {
        debText.gameObject.SetActive(true);

        int remain = newQ;
        indexElement = _indexElement;
        maxStack = InventoryDataCenter.DB[indexElement].MaxStack;

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
        indexElement = -1;
    }

    void OnRecolect()
    {
        InventoryAgent.InstanceAgent.AddElement(indexElement, currQ);
        ItemSpawner.ReturnItem(indexElement, this);
    }
    string OnGetInfo()
    {
        return InventoryDataCenter.DB[indexElement].Name;
    }


}
