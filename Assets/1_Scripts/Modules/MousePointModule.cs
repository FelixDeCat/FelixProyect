using UnityEngine;

[System.Serializable]
public class MousePointModule
{
    Ray ray;
    RaycastHit hit;

    Vector3 center;
    public Collider QueryAtScreenCenter(LayerMask mask, float maxDistance, QueryTriggerInteraction interaction)
    {
        Vector3 center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out hit, maxDistance, mask, interaction))
        {
            UIGlobalData.Txt_InteractInfo.text = hit.collider.gameObject.name;
            return hit.collider;
        }
        
        return null;
    }

    public Collider QueryAtMouse(LayerMask mask, float maxDistance, QueryTriggerInteraction interaction)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance, mask, interaction))
        {
            UIGlobalData.Txt_InteractInfo.text = hit.collider.gameObject.name;
            return hit.collider;
        }
        return null;
    }
}
