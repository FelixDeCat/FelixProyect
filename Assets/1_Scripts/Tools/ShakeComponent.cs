using UnityEngine;

[System.Serializable]
public class ShakeComponent
{
    [SerializeField] Transform root;
    [SerializeField] float intensity = 0.1f;
    [SerializeField] float duration = 0.2f;

    Vector3 originalLocalPos;
    float timer;
    bool shaking;

    public void Initialize()
    {
        originalLocalPos = root.localPosition;
    }

    public void Shake()
    {
        timer = duration;
        shaking = true;
    }

    public void Tick()
    {
        if (!shaking) return;

        if (Time.timeScale == 0f)
        {
            root.localPosition = originalLocalPos;
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            shaking = false;
            root.localPosition = originalLocalPos;
            return;
        }

        Vector3 randomOffset = Random.insideUnitSphere * intensity;
        root.localPosition = originalLocalPos + randomOffset;
    }
}
