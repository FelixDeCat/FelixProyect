using TMPro;
using UnityEngine;

public class PlayerPanel : MonoUISingleton<PlayerPanel>
{

    public override void SingletonAwake()
    {

    }

    public static void Wait() => Instance.fsm.Wait();
    public static void Select() => Instance.fsm.Select();
}
