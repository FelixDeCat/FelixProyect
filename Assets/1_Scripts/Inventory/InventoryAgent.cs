using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryAgent: IStarteable, IUpdateable
{
    public static InventoryAgent InstanceAgent;
    [SerializeField] bool isPrincipalAgent = false;
    // ejemplo de inventario
    public Container container;
    
    [SerializeField] ItemUseManager useManager;

    [Header("Front End")]
    [SerializeField] UIContainer uiContainer;

    [Header("Drop Position")]
    [SerializeField] Transform dropRoot;

    Vector3 spawnPos
    {
        get
        {
            return dropRoot.position + dropRoot.forward * 5 + dropRoot.up;
        }
    }

    void IStarteable.Start()
    {
        if (isPrincipalAgent)
        {
            InstanceAgent = this;
            uiContainer = UIGlobalData.UIContainer_PlayerInventory;
            container = new Container(15);
            uiContainer.Intialize(container, OnPointerEnter, OnPointerExit, OnPointerDown, OnPointerUp);
            uiContainer.AddCallback_GetGUIDs(useManager.GuidEquiped);
        }
    }

    void IUpdateable.Tick(float delta)
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            var item = InventoryDataCenter.RandomItem.ItemID;
            Debug.Log(item);

            ItemSpawner.SpawnItem(item,
                Tools.Random_XZ_PosInBound(
                    center: spawnPos,
                    radius: 2f));
        }
    }

    public int TryAddElement(int index_ID, int quantity, string parameters, string GUID = "")
    {
        container.TryAdd(index_ID, quantity, out int remain, parameters, GUID);
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

    public void Refresh()
    {
        uiContainer.Refresh(container);
    }


    public void OnPointerEnter(int _indexInContainer)
    {
       // Debug.Log(_indexInContainer);
        var slot = container[_indexInContainer];
        if (slot.IndexID == -1)
        {
            CustomConsole.LogStaticText(1, "Item: empty");
            CustomConsole.LogStaticText(2, "MaxStack: empty");
            return;
        }
        CustomConsole.LogStaticText(1, $"Item: {InventoryDataCenter.Get_Data_ByID(slot.IndexID).Name.FiveChar()}", color: Color.yellow);
        CustomConsole.LogStaticText(2, $"MaxStack: {InventoryDataCenter.Get_Data_ByID(slot.IndexID).MaxStack.ToString()}", color: Color.yellow);
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

        var item = InventoryDataCenter.Get_Item_ByID(slot.IndexID, debug: false);
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
            if (slot == null) throw new System.Exception("El slot es nulo");

            var result = useManager.UseBehaviour(slot);

            switch (result)
            {
                case UseResult.Success:
                    break;
                case UseResult.Fail:
                    CustomConsole.Log("Fallo al usar ID: " + slot.IndexID);
                    break;
                case UseResult.Consume:
                    container.RemoveQuantityFromPosition(_indexInContainer, 1);
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
                var slotEquip = container[_indexInContainer].GetEquipSlot();

                ItemSpawner.SpawnItem(dropID, spawnPos, quant, slotEquip.GUID);
            }
        }

        uiContainer.Refresh(container);

    }
    public void OnPointerUp(int _indexInContainer, int _pointerID)
    {
        var slot = container[_indexInContainer];

        if (currentIndex == _indexInContainer)// press en el mismo lugar
        {
            if (_pointerID == -1) { }
            else if (_pointerID == -2) { }
            else if (_pointerID == -3) { }
        }
        else
        {
            
        }

        if (_pointerID == -3)
        {
            var item = InventoryDataCenter.Get_Item_ByID(slot.IndexID, debug: false);
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