using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DamageSensor
{
    [SerializeField] Bounds dmgbounds;
    [SerializeField] Transform root;
    [SerializeField] LayerMask mask;
    public void SetTransform(Transform root)
    {
        this.root = root;
    }

    Action<IDamageable> OnDmg;

    public void SubscribeToDmgElement(Action<IDamageable> onDmg)
    {
        this.OnDmg += onDmg;
    }
    public void ExecuteQuery()
    {
        var colliders = Physics.OverlapBox(CalculateOffset(), dmgbounds.size, root.rotation, mask);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<IDamageable>(out var dmg))
               OnDmg?.Invoke(dmg);
        }
    }

    public void DrawGizmosManually()
    {
        Gizmos.color = Color.green;

        Matrix4x4 oldMatrix = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(
            CalculateOffset(),      // posición del centro
            root.rotation,          // rotación del sensor
            Vector3.one             // escala 1 (porque Bounds ya trae su size)
        );

        Gizmos.DrawWireCube(Vector3.zero, dmgbounds.size);
        Gizmos.matrix = oldMatrix;
    }

    Vector3 CalculateOffset()
    {
        return root.position + root.forward + dmgbounds.center;
    }
}
