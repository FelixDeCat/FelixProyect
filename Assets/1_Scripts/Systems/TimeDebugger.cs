using UnityEngine;
using TMPro;

public class TimeDebugger : MonoBehaviour
{
    public TextMeshProUGUI txtDebug;

    private void Update()
    {
        long current = TimeService.NowUnixSeconds;
        txtDebug.text = $"Unix seconds: {current}";
    }
}
