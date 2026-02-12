using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;

public class ContextualMenu : MonoUISingleton<ContextualMenu>
{


    [Header("Contextual Menu")]
    [SerializeField] TextMeshProUGUI txt_name;
    Tuple<Action, string, bool>[] callbacks = new Tuple<Action, string, bool>[0];

    [SerializeField] Transform parent;
    [SerializeField] IndexableButton buttonPrefab;
    ObjectPool<IndexableButton> btn_pool;
    HashSet<IndexableButton> actives = new HashSet<IndexableButton>();
    [SerializeField] Button btn_close;
    private void Start()
    {
        btn_pool = new ObjectPool<IndexableButton>(
            createFunc: () => Instantiate(buttonPrefab, parent),
            actionOnGet: (btn) => btn.gameObject.SetActive(true),
            actionOnRelease: (btn) => btn.gameObject.SetActive(false),
            actionOnDestroy: (btn) => Destroy(btn.gameObject),
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 10
        );

        btn_close.onClick.AddListener(() => Close(removeFromManager: true));
    }

    public override void SingletonAwake()
    {

    }

    //// O P E N 
    
    /// <summary>
    /// Opens a menu with the specified name and attaches callback actions to menu events.
    /// </summary>
    /// <remarks>Each tuple in the <paramref name="_cbk"/> array represents a menu event handler, where the
    /// <see cref="Action"/> is invoked for the event, the string provides an identifier or context, and the boolean
    /// flag may control handler behavior. The method is static and can be called without creating an
    /// instance.</remarks>
    /// <param name="_name">The name of the menu to open. Cannot be null or empty.</param>
    /// <param name="_cbk">An array of tuples, each containing a callback action, a string identifier, and a boolean flag. Used to
    /// configure menu event handlers. Can be empty if no callbacks are required.</param>
    public static void Open(string _name, params Tuple<Action, string, bool>[] _cbk)
    {
        Instance.OpenMenu(_name, _cbk);
    }
    void OpenMenu(string _name, params Tuple<Action, string, bool>[] _cbk)
    {
        txt_name.text = _name;
        callbacks = _cbk;
        for (int i = 0; i < callbacks.Length; i++)
        {
            var btn = btn_pool.Get();

            btn.Set(x => OnClick(x), i, callbacks[i].Item2, "test"); // El string vacío es un placeholder para la descripción

            Debug.Log("Asignando callback al botón " + i + " con acción: " + callbacks[i].Item2);
            actives.Add(btn);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = callbacks[i].Item2;
        }
        Open();
        GameController.Instance.ChangeToInventories();
        UIGlobalData.AddUiComponent(this);
    }

    void OnClick(int index)
    {
        callbacks[index].Item1.Invoke();
        if (callbacks[index].Item3)
        {
            GameController.Instance.ChangeToPlay();
        }
    }

    protected override void EVENT_Open_Sucess()
    {
        
    }
    protected override void EVENT_Close_Sucess()
    {
        txt_name.text = string.Empty;
        callbacks = new Tuple<Action, string, bool>[0];
        foreach (var btn in actives)
        {
            btn.Clear();
            btn_pool.Release(btn);
        }

    }
}
