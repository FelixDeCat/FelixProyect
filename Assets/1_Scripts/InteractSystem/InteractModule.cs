using TMPro;
using UnityEngine;

[System.Serializable]
public class InteractModule: IStarteable, IPausable, IUpdateable
{

    bool isPaused;
    [SerializeField] LayerMask interactables;
    Vector3 center;
    Ray ray;
    RaycastHit info;
    [SerializeField] float maxDistance = 3f;

    void IStarteable.Start()
    {
        center = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
    }

    Interactable interactable = null;

    void IUpdateable.Tick(float delta)
    {
        ray = Camera.main.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out info, maxDistance, interactables))
        {
            interactable = info.transform.GetComponent<Interactable>();
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
        isPaused = true;
    }

    void IPausable.Resume()
    {
        isPaused = false;
    }

   
}
