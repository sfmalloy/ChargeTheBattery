using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float decreaseDelay;

    public Text batteryLevelText;

    private bool isDelayed;
    private float batteryLevel;

    void Start()
    {
        batteryLevel = 20;
    }

    void Update()
    {
        if (!isDelayed)
        {
            UpdateBattery(-1);
            StartCoroutine(DelayDischarge());
        }
        batteryLevelText.text = batteryLevel.ToString();
    }

    public void UpdateBattery(int delta)
    {
        batteryLevel = Mathf.Max(0, Mathf.Min(100, batteryLevel + delta));
    }

    IEnumerator DelayDischarge() 
    {
        isDelayed = true;
        yield return new WaitForSeconds(decreaseDelay);
        isDelayed = false;
    }
}
