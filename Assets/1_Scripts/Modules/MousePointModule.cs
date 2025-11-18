using UnityEngine;

[System.Serializable]
public class MousePointModule: IUpdateable, IPausable, IActivable
{
    [SerializeField] LayerMask mask;
    Ray ray;
    RaycastHit hit;
    [SerializeField] GameObject feedbackPointer;

    bool active = false;
    void IActivable.Active() { active = true; }

    void IActivable.Deactivate() { active = false; }

    void IPausable.Pause() { }

    void IPausable.Resume() { }

    void IUpdateable.Tick(float delta)
    {
        if (!active) return;
        if (Input.GetMouseButtonDown(0) && feedbackPointer != null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue, mask, QueryTriggerInteraction.Ignore))
            {
                feedbackPointer.transform.position = hit.point;
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (!active) return;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 10);
    }
}
