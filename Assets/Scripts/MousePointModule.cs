using UnityEngine;

public class MousePointModule : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    Ray ray;
    RaycastHit hit;
    [SerializeField] GameObject feedbackPointer;
    void Update()
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
