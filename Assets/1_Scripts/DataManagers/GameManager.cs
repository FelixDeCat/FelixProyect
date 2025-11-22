using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameManager instance;

    ItemDropperPositions dropperPositions;
    [SerializeField] Transform[] dropDirections; 

    public override void SingletonAwake()
    {
        dropperPositions = new ItemDropperPositions(randomizations: 5, dropDirections);
    }
}
