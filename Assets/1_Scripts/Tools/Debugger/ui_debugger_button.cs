using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ui_debugger_button : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI txt;
    Action action;

    public void Set(string txt, Action action)
    {
        this.txt.text = txt;
        this.action = action;
        button.onClick.AddListener(OnClick);
    }

    public void PoolRelease()
    {
        button.onClick.RemoveListener(OnClick);
        action = null;
        txt.text = string.Empty;
    }

    void OnClick()
    {
        this.action.Invoke();
    }
}
