using System.Collections.Generic;
using UnityEngine;

public class InventoryAgent : MonoBehaviour
{
    public static InventoryAgent InstanceAgent;

    [SerializeField] bool isAgent;

    // ejemplo de inventario
    public Container bag; 
    [SerializeField] UIContainer uiContainer;

    [SerializeField] Transform root;


    private void Awake()
    {
        if (isAgent)
        {
            InstanceAgent = this;
        }
    }
    void Start()
    {
        bag = new Container(32);
        uiContainer.Intialize(bag);

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemSpawner.SpawnItem(4, root.position + root.forward * 2 + transform.up);
        }
    }

    public void AddElement(int index, int quantity, params int[] states)
    {
        bag.Add(index, quantity, states);
        uiContainer.Refresh(bag);
    }

}





