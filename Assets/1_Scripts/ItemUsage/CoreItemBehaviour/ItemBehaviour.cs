using UnityEngine;

public abstract class ItemBehaviour : MonoBehaviour
{
    [SerializeField] private int unique_behaviour_ID = -1;
    public int GetUniqueBehaviourID()
    {
        return unique_behaviour_ID;
    }
}
