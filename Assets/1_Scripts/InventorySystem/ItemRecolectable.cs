using System;
using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public int indexElement = 0;
    
    [SerializeField] Interactable interactable;

    // TO DO: luego esto cambiar por el max stack de cada elemento, esto es para optimizar
    // para que se generen grupos y no tener x ejemplo 40 elementos individuales dispersos
    // cuando se puede tener 2 grupos de 20
    public int stacked = 1;

    private void Awake()
    {
        interactable.SetInteractable(OnRecolect, OnGetInfo);
    }

    private void Start()
    {
        stacked = InventoryDataCenter.DB[indexElement].MaxStack;
    }

    void OnRecolect()
    {
        InventoryAgent.InstanceAgent.AddElement(indexElement, 1);
        ItemSpawner.ReturnItem(indexElement, this);
    }
    string OnGetInfo()
    {
        return InventoryDataCenter.DB[indexElement].Name;
    }


}
