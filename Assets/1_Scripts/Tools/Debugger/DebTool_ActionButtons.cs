using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DebTool_ActionButtons : MonoBehaviour
{
    ObjectPool<ui_debugger_button> pool;
    [SerializeField] ui_debugger_button model;

    private void Awake()
    {
        pool = new ObjectPool<ui_debugger_button> (   
            createFunc: () => 
            {
                var b = Instantiate(model, transform);
                b.gameObject.SetActive(false);
                return b;
            },
            actionOnGet: b => b.gameObject.SetActive(true),
            actionOnRelease: b => 
            {
                b.PoolRelease();
                b.gameObject.SetActive(false);
            },
            actionOnDestroy: b =>  Destroy(b.gameObject)  
            );
    }

    public void AddActions(List<Tuple<string,Action>> actions)
    {
        foreach (var action in actions)
        {
            string n = action.Item1;
            Action a = action.Item2;
            pool.Get().Set(n,a);
        }
    }
    public void AddAction(string name, Action action)
    {
        pool.Get().Set(name, action);
    }
    public void ReleaseAll()
    {
        foreach (var btn in GetComponentsInChildren<ui_debugger_button>(true))
        {
            // Si el botón está activo en escena, devolverlo al pool.
            if (btn.gameObject.activeSelf)
            {
                pool.Release(btn);
            }
        }

        pool.Clear();
    }


}
