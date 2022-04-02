using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float decreaseDelay;

    public Text batteryLevelText;

    private bool isDelayed;
    private float batteryLevel;

    private bool currentPhase;

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

        if (!currentPhase)
        {
            StartCoroutine(DoPhase());
        }
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

    IEnumerator DoPhase()
    {
        currentPhase = true;
        yield return new WaitForSeconds(30);
        decreaseDelay = Mathf.Max(decreaseDelay - 0.05f, 0);
        currentPhase = false;
    }
}
