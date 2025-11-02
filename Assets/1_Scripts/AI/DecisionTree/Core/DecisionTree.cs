namespace IA.DecisionTree
{
    using UnityEngine;

    public class DecisionTree : MonoBehaviour
    {
        [SerializeField] Node firstNode;

        public void Think()
        {
            firstNode.Execute();
        }
    }
}
