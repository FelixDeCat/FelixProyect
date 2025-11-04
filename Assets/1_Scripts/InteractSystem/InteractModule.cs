using TMPro;
using UnityEngine;

public class InteractModule : MonoBehaviour, IPausable
{

    bool isPaused;
    [SerializeField] LayerMask interactables;
    Vector3 center;
    Ray ray;
    RaycastHit info;
    [SerializeField] float maxDistance = 3f;
    private void Start()
    {
        center = new Vector3(Screen.width/2,Screen.height/2,0f);
    }


    Interactable interactable = null;
    void Update()
    {
        
        ray = Camera.main.ScreenPointToRay(center);

        CustomConsole.LogStaticText(0, $"center: {center}");

        if (Physics.Raycast(ray, out info, maxDistance, interactables))
        {
            Debug.Log($"{info.collider.name}");

            interactable = info.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                UIGlobalData.Txt_InteractInfo.text = interactable.Info;
                if (Input.GetButtonDown("Interact"))
                {
                    interactable.Interact();
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
