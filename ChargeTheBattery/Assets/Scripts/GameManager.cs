using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float decreaseDelay;
    public int currentPhaseNum;

    public Text batteryLevelText;
    public Image batteryLevelSlider;
    public Text debug;

    private bool isDelayed;
    private float batteryLevel;
    private float originalBatteryWidth;

    private bool currentPhase;

    readonly Color RED = new Color(201, 46, 46, 255) * (1.0f/255.0f);
    readonly Color ORANGE = new Color(201, 132, 46, 255) * (1.0f/255.0f);
    readonly Color GREEN = new Color(77, 201, 46, 255) * (1.0f/255.0f);

    void Start()
    {
        batteryLevel = 20;
        originalBatteryWidth = batteryLevelSlider.transform.localScale.x;
        currentPhaseNum = 1;
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

        if (batteryLevel <= 20)
            batteryLevelSlider.color = RED;
        else if (batteryLevel <= 50)
            batteryLevelSlider.color = ORANGE;
        else
            batteryLevelSlider.color = GREEN;

        debug.text = currentPhaseNum.ToString();

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
        if (decreaseDelay > 0.05f)
            currentPhaseNum += 1;
        decreaseDelay = Mathf.Max(decreaseDelay - 0.05f, 0.05f);
        currentPhase = false;
    }
}
