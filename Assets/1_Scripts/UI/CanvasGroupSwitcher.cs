using System;
using System.Collections;
using UnityEngine;

public class CanvasGroupSwitcher
{
    CanvasGroup group;
    float duration = 0.5f;
    float timer = 0;

    Coroutine routine;
    bool opened = false;

    Action finish = null;
    MonoBehaviour mono;

    public CanvasGroupSwitcher(CanvasGroup group, MonoBehaviour owner, bool openOnStart = false, float time = 0.2f)
    {
        this.group = group ?? throw new ArgumentNullException(nameof(owner));
        this.mono = owner ?? throw new ArgumentNullException(nameof(owner));
        this.duration = Mathf.Max(0f, time);
        opened = openOnStart;

        ApplyInstant(openOnStart);
    }
    public void SetOpenOnStart(bool openOnStart = false)
    {
        opened = openOnStart;
        ApplyInstant(openOnStart);
    }
    public void ApplyInstant(bool state)
    {
        group.alpha = state ? 1f : 0f;
        group.interactable = state;
        group.blocksRaycasts = state;
    }

    public void Open(Action OnFinish = null)
    {
        if (opened == true)
        {
            OnFinish?.Invoke();
            return;
        }
        finish = OnFinish;
        if (routine != null) mono.StopCoroutine(routine);
        routine = mono.StartCoroutine(TurnSwitch(true));
    }

    public void Close(Action OnFinish = null)
    {
        if (opened == false)
        {
            OnFinish?.Invoke();
            return;
        }
        finish = OnFinish;
        if (routine != null) mono.StopCoroutine(routine);
        routine = mono.StartCoroutine(TurnSwitch(false));
    }

    public void Switch(Action OnFinish = null)
    {
        opened = !opened;
        finish = OnFinish;
        if (routine != null) mono.StopCoroutine(routine);
        routine = mono.StartCoroutine(TurnSwitch(opened));
    }

    IEnumerator TurnSwitch(bool newState)
    {
        while (timer < duration)
        {
            timer += Time.deltaTime;
            group.alpha = newState ? timer / duration : 1 - timer / duration;
            yield return new WaitForEndOfFrame();
        }
        group.blocksRaycasts = newState;
        group.interactable = newState;
        timer = 0;

        routine = null;
        opened = newState;

        finish?.Invoke();
        finish = null;

        yield return null;
    }
}
