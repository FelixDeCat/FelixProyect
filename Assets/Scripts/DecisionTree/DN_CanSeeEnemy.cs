using UnityEngine;
using IA.DecisionTree;

public class DN_CanSeeEnemy : DecisionNode
{
    [SerializeField] Transform owner;
    [SerializeField] Transform target;
    [SerializeField] float distance = 2f;

    private void Start()
    {
        ConfigureDecision(CanSeeEnemy);
    }

    bool CanSeeEnemy()
    {
        return Vector3.Distance(owner.position, target.position) < distance;
    }
}
