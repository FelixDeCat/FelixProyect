using System.Collections.Generic;
using UnityEngine;

public class InventoryAgent : MonoBehaviour
{
    public static InventoryAgent InstanceAgent;

    [SerializeField] bool isAgent;

    // ejemplo de inventario
    public Container container; 
    [SerializeField] UIContainer uiContainer;

    [SerializeField] Transform root;

    Vector3 spawnPos
    {
        get
        {
            return root.position + root.forward * 5 + root.up;
        }
    }

    private void Awake()
    {
        if (isAgent)
        {
            InstanceAgent = this;
        }
    }
    void Start()
    {
        container = new Container(18);
        uiContainer.Intialize(container, OnPointerEnter, OnPointerExit, OnPointerDown, OnPointerUp);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            ItemSpawner.SpawnItem(Random.Range(0,InventoryDataCenter.DB.Length),
                Tools.Random_XZ_PosInBound(
                    center: spawnPos,
                    radius: 2f));
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemSpawner.SpawnItem(4, 
                Tools.Random_XZ_PosInBound(
                    center: spawnPos,
                    radius: 2f));
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ItemSpawner.SpawnItem(6, 
                Tools.Random_XZ_PosInBound(
                    center: spawnPos,
                    radius: 2f));
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ItemSpawner.SpawnItem(7, 
                Tools.Random_XZ_PosInBound(
                    center: spawnPos,
                    radius: 2f));
        }
    }

    public int TryAddElement(int index_ID, int quantity, params int[] states)
    {
        container.TryAdd(index_ID, quantity, out int remain);
        uiContainer.Refresh(container);
        return remain;
    }

    public int RemoveElement(int index_container, int quantity)
    {
        int remain = container.RemoveQuantityFromPosition(index_container, quantity);
        int result = quantity - remain;
        uiContainer.Refresh(container);

        return result;
    }


    public void OnPointerEnter(int _indexInContainer)
    {
        Debug.Log(_indexInContainer);
        var slot = container[_indexInContainer];
        if (slot.IndexID == -1)
        {
            CustomConsole.LogStaticText(1, "Item: empty");
            CustomConsole.LogStaticText(2, "MaxStack: empty");
            return;
        }
        CustomConsole.LogStaticText(1, $"Item: {InventoryDataCenter.DB[slot.IndexID].Name.FiveChar()}", color: Color.yellow);
        CustomConsole.LogStaticText(2, $"MaxStack: {InventoryDataCenter.DB[slot.IndexID].MaxStack.ToString()}", color: Color.yellow);
    }
    public void OnPointerExit(int _indexInContainer)
    {
        CustomConsole.LogStaticText(1, "Item:", Color.gray);
        CustomConsole.LogStaticText(2, "MaxStack:", Color.gray);
    }
    int currentIndex = -1;
    public void OnPointerDown(int _indexInContainer, int _pointerID)
    {
        if (_indexInContainer < 0 || _indexInContainer >= container.MaxCapacity)
        {
            CustomConsole.LogError($"Me esta llegando mal el indice mi capacidad " +
                $"es de {container.MaxCapacity}, el indice recibido es: {_indexInContainer}" );
        }

        var slot = container[_indexInContainer];
        currentIndex = _indexInContainer;

        var item = InventoryDataCenter.Get_Valid_Item_ByID(slot.IndexID, debug: false);
        if (item == null)
        {
            return;
            
        }

        CustomConsole.LogStaticText(3, $"Down::{_pointerID}::{item.Name.FiveChar()}", Color.green);

        if (_pointerID == -1) // show por ahora TODO: agarrar
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //tomo uno con slider
            }
            else
            {
                //tomo uno normal
            }
        }
        else if (_pointerID == -2)
        {
            // usar item

            int ID = container[_indexInContainer].IndexID;
            ItemData idata = InventoryDataCenter.DataBase[ID];
            var result = ItemUseManager.Instance.UseBehaviour(ID, idata.Behaviour);

            switch (result)
            {
                case UseResult.Success:
                    break;
                case UseResult.Fail:
                    break;
                case UseResult.Consume:
                    container.RemoveQuantityFromPosition(_indexInContainer, 1);
                    break;
                case UseResult.Equip:
                    break;
                default:
                    break;
            }

        }
        else if (_pointerID == -3)
        {
            int quant = 0;
            int dropID = container[_indexInContainer].IndexID;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                quant = RemoveElement(_indexInContainer, 1);
            }
            else
            {
                quant = RemoveElement(_indexInContainer, container[_indexInContainer].Quantity);
            }
            if (quant > 0 && dropID != -1)
            {
                ItemSpawner.SpawnItem(dropID, spawnPos, quant);
            }

        }


        uiContainer.Refresh(container);

    }
    public void OnPointerUp(int _indexInContainer, int _pointerID)
    {
        var slot = container[_indexInContainer];

        if (currentIndex == _indexInContainer)// press en el mismo lugar
        {
            if (_pointerID == -1)
            {

            }
            else if (_pointerID == -2)
            {

            }
            else if (_pointerID == -3)
            {
            }
        }
        else
        {
            
        }

        if (_pointerID == -3)
        {
            var item = InventoryDataCenter.Get_Valid_Item_ByID(slot.IndexID, debug: false);
            if (item != null)
            {
                CustomConsole.LogStaticText(3, $"Down::{_pointerID}::{item.Name.FiveChar()}", Color.green);
            }
            else
            {
                CustomConsole.LogStaticText(3, $"Down::{_pointerID}::null", Color.green);
            }
        }
    }
}