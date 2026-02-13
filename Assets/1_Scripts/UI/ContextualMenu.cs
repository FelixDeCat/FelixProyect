using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ContextualMenu : MonoUISingleton<ContextualMenu>
{
    [Header("Contextual Menu")]
    [SerializeField] TextMeshProUGUI txt_name;
    List<ContextualMenuOption> options = new List<ContextualMenuOption>();

    [SerializeField] Button btn_close;

    [Header("Instanciado de Botones")]
    [SerializeField] Transform parent;
    [SerializeField] IndexableButton buttonPrefab;

    ObjectPool<IndexableButton> btn_pool;
    HashSet<IndexableButton> actives = new HashSet<IndexableButton>();
    
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

        btn_close.onClick.AddListener(() => GameController.Instance.ChangeToPlay());
    }

    public override void SingletonAwake()
    {

    }

    // BUILDER
    public static ContextualMenu Begin(string _name)
    {
        Instance._openMenu(_name);
        return Instance;
    }
    public ContextualMenu AddOption(string _name, Action _action, string tooltip = "", bool closeOnClick = true)
    {
        ContextualMenuOption option = new ContextualMenuOption(_action, _name, tooltip, closeOnClick);
        _addOption(option);
        return this;
    }
    void _openMenu(string _name)
    {
        txt_name.text = _name;
        Open();
        GameController.Instance.ChangeToContextual();
        UIGlobalData.AddUiComponent(this);
    }
    void _addOption(ContextualMenuOption option)
    {
        if (!options.Contains(option)) 
        { 
            options.Add(option);
            var btn = btn_pool.Get();
            btn.Set(x => OnClickList(x), options.Count - 1, option.name, option.tooltip);
            actives.Add(btn);
        }
    }
    
    void OnClickList(int index)
    {
        options[index].action.Invoke();
        if (options[index].closeOnClick)
        {
            GameController.Instance.ChangeToPlay();
        }
    }

    protected override void EVENT_Open_Sucess()
    {
        
    }
    protected override void EVENT_Close_Sucess()
    {
        options.Clear();

        txt_name.text = string.Empty;
        foreach (var btn in actives)
        {
            btn.Clear();
            btn_pool.Release(btn);
        }

    }

    public struct ContextualMenuOption
    {
        public Action action;
        public string name;
        public string tooltip;
        public bool closeOnClick;
        public ContextualMenuOption(Action action, string name, string tooltip = "", bool closeOnClick = true)
        {
            this.action = action;
            this.name = name;
            this.tooltip = tooltip;
            this.closeOnClick = closeOnClick;
        }
    }
}
