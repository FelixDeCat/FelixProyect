using UnityEngine;
using AI.DecisionTree;

public class EnemyDesTreeExample : MonoBehaviour
{
    [SerializeField] DecisionTree myTree;

    void Update()
    {
        myTree.Think();
    }
}
