using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float decreaseDelay;

    public Text batteryLevelText;
    public Image batteryLevelSlider;
    public Text debug;

    private bool isDelayed;
    private float batteryLevel;
    private float originalBatteryWidth;

    private bool currentPhase;

    void Start()
    {
        batteryLevel = 20;
        originalBatteryWidth = batteryLevelSlider.transform.localScale.x;
    }

    void Update()
    {
        if (!isDelayed)
        {
            UpdateBattery(-1);
            StartCoroutine(DelayDischarge());
        }

        batteryLevelText.text = batteryLevel.ToString();
        batteryLevelSlider.transform.localScale = new Vector3(
            originalBatteryWidth * (batteryLevel / 100),
            batteryLevelSlider.transform.localScale.y,
            batteryLevelSlider.transform.localScale.z
        );

        debug.text = decreaseDelay.ToString();

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
        decreaseDelay = Mathf.Max(decreaseDelay - 0.05f, 0.05f);
        currentPhase = false;
    }
}
