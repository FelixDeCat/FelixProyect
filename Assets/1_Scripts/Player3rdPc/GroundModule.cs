using UnityEngine;

[System.Serializable]
public class GroundModule : IStarteable
{
    RaycastHit[] hits;
    [SerializeField] LayerMask mask;
    [SerializeField] float maxDistance = 0.5f;
    [SerializeField] Transform root;

    [SerializeField] int count = 0;

    [SerializeField] bool debug;

    public bool IsGrounded()
    {
        count = Physics.RaycastNonAlloc(root.position + Vector3.up * 0.5f, Vector3.down, hits, maxDistance, mask);
        return count > 0;
    }
    void IStarteable.Start()
    {
        hits = new RaycastHit[3];
    }
}
