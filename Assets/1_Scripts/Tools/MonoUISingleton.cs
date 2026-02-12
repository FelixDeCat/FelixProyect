using UnityEngine;

public abstract class MonoUISingleton<T> : UIComponent where T : MonoBehaviour
{
    protected static MonoUISingleton<T> MyInstance;
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
            CustomAwake();
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
