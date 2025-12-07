using System;
using TMPro;
using UnityEngine;

[System.Serializable]
public class InteractModule: IStarteable, IPausable, IUpdateable, IActivable
{

    [SerializeField] LayerMask interactables;
    Vector3 center;
    Ray ray;
    RaycastHit info;
    [SerializeField] float maxDistance = 3f;

    MousePointModule mousePointModule;
    public void SetMousePointModule(MousePointModule mousePointModule)
    {
        this.mousePointModule = mousePointModule;
    }

    void IStarteable.Start()
    {
        //center = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
    }

    Interactable interactable = null;

    void IUpdateable.Tick(float delta)
    {
        if (!active) return;

        Debug.Log("active");

        Collider col = mousePointModule.QueryAtScreenCenter(interactables, maxDistance, QueryTriggerInteraction.Collide);

        if (col != null)
        {
            UIGlobalData.Txt_InteractInfo.text = col.name;

            interactable = col.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    interactable.Interact();
                }
                else
                {
                    UIGlobalData.Txt_InteractInfo.text = interactable.Info;
                }
            }
        }
        else
        {
            UIGlobalData.Txt_InteractInfo.text = string.Empty;
        }
    }

    void IPausable.Pause()
    {
        
    }

    void IPausable.Resume()
    {
        
    }

    bool active = false;
    void IActivable.Active() { active = true; }

    void IActivable.Deactivate() { active = false; }
}
