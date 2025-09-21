using UnityEngine;

[System.Serializable]
public class MousePointModule: IUpdateable, IPausable, IActivable
{
    [SerializeField] LayerMask mask;
    Ray ray;
    RaycastHit hit;
    [SerializeField] GameObject feedbackPointer;

    void IActivable.Active() { }

    void IActivable.Deactivate() { }

    void IPausable.Pause() { }

    void IPausable.Resume() { }

    void IUpdateable.Tick(float delta)
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue, mask, QueryTriggerInteraction.Ignore))
            {
                feedbackPointer.transform.position = hit.point;
            }
        }
    }
}
