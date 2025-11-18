using UnityEngine;
using AI.DecisionTree;

public class Decision_CanSeeEnemy : DecisionNode
{
    [SerializeField] Transform owner;
    [SerializeField] Transform target;
    [SerializeField] float distance = 2f;

    protected override bool Predicate()
    {
        return Vector3.Distance(owner.position, target.position) < distance;
    }
}
