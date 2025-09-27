using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static MonoSingleton<T> MyInstance;
    public static T Instance
    {
        get
        {
            return MyInstance as T;
        }
    }

    private void Awake()
    {
        if (MyInstance == null)
        {
            MyInstance = this;
            SingletonAwake();
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("Hay dos instancias de " + nameof(T) + " corregir este error, no esta pensado para esto");
        }
    }

    public abstract void SingletonAwake();
}
