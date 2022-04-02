using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float decreaseRate;
    public float batteryLevel;

    public Text batteryLevelText;
    
    public void UpdateBattery(int delta)
    {
        batteryLevel = Mathf.Max(0, Mathf.Min(100, batteryLevel + delta));
    }

    void Start()
    {
        batteryLevel = 100;
    }

    void Update()
    {
        batteryLevelText.text = batteryLevel.ToString();
    }
}
