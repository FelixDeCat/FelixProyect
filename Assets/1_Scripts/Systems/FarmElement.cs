using UnityEngine;
using System;

public class FarmElement : MonoBehaviour
{

    [SerializeField] FarmElementData elementData;

    long beginTime;
    long endTime;
    long remainTime;

    [SerializeField] GameObject[] stages;

    bool readyToHarvest = false;

    TimedData farmdata;

    [SerializeField] string ID;

    [SerializeField] Interactable interactable;

    private void Awake()
    {
        interactable.SetInteractable(Interact,Info);

    }

    string Info()
    {
        return "cosecha";
    }

    public void Interact()
    {
        ContextualMenu.Open(
            "Planta", 
                new System.Tuple<Action, string, bool>(
                    DEBUG_Guardar_Persistencia, 
                    "guardar", 
                    true),
                new System.Tuple<Action, string, bool>(
                    FuncionVacia,
                    "ejemplo2",
                    false));
    }

    public void FuncionVacia()
    {

    }
    public void DEBUG_Guardar_Persistencia()
    {

    }

    public void OnCreate()
    {
        beginTime = TimeService.NowUnixSeconds;
        endTime = TimeService.NowUnixSeconds +
            elementData.secondsToGrow +
            elementData.minutesToGrow * 60 +
            elementData.hoursToGrow * 3600;

        remainTime = endTime - beginTime;

        farmdata = new TimedData
        {
            id = System.Guid.NewGuid().ToString(),
            beginTime = this.beginTime,
            endTime = this.endTime
        };

        TimeManager.Instance.AddTimedData(farmdata);
        Debug.Log($"Elemento de granja {farmdata.id} creado y añadido al gestor de tiempo.");
    }

    public void OnHarvest()
    {
        readyToHarvest = false;
        TimeManager.Instance.RemoveTimedData(farmdata);
        // Aquí puedes añadir lógica adicional para cuando se cosecha el elemento
        Debug.Log("Elemento de granja cosechado.");
    }
}
