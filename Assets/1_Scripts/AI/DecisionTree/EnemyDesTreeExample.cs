using UnityEngine;
using IA.DecisionTree;

public class EnemyDesTreeExample : MonoBehaviour
{
    [SerializeField] DecisionTree myTree;

    void Update()
    {
        myTree.Think();
    }
}
