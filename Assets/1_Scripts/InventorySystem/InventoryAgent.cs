using System.Collections.Generic;
using UnityEngine;

public class InventoryAgent : MonoBehaviour
{
    // ejemplo de inventario
    public Container bag; 
    [SerializeField] UIContainer uiContainer;

    void Start()
    {
        bag = new Container(32);
        uiContainer.Intialize(bag);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bag.Add(Random.Range(0, InventoryDataCenter.DB.Length), Random.Range(1,4), 0);
            uiContainer.Refresh(bag);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}





