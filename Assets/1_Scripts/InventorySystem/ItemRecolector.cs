using System;
using UnityEngine;

public class ItemRecolector : MonoBehaviour
{
    public int indexElement = 0;
    public int stacked = 1;// TO DO: luego esto cambiar por el max stack de cada elemento

    private void Start()
    {
        stacked = InventoryDataCenter.DB[indexElement].MaxStack;
    }

    public void Recolect()
    {
        InventoryAgent.InstanceAgent.AddElement(indexElement, 1);
    }
}
