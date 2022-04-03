using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float decreaseDelay;
    public int currentPhaseNum;
    public float score;
    public Text scoreText;

    public GameObject mainCanvas;
    public GameObject tutorialCanvas;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public Text batteryLevelText;
    public Image batteryLevelSlider;
    public Text debug;

    private bool isDelayed;
    private float batteryLevel;
    private float originalBatteryWidth;

    private bool currentPhase;
    private bool tutorialMode;

    readonly Color RED = new Color(201, 46, 46, 255) * (1.0f / 255.0f);
    readonly Color ORANGE = new Color(201, 132, 46, 255) * (1.0f / 255.0f);
    readonly Color GREEN = new Color(77, 201, 46, 255) * (1.0f / 255.0f);

    void Start()
    {
        tutorialMode = true;

        batteryLevel = 20;
        originalBatteryWidth = batteryLevelSlider.transform.localScale.x;
        currentPhaseNum = 1;
        Time.timeScale = 0;
        score = 0;
    }

    void Update()
    {
        if (!tutorialMode && Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }

            if (!isDelayed)
            {
                UpdateBattery(-1);
                StartCoroutine(DelayDischarge());
            }

            if (!currentPhase)
            {
                StartCoroutine(DoPhase());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                tutorialMode = false;
                tutorialCanvas.SetActive(false);
                mainCanvas.SetActive(true);
            }
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

        debug.text = "Phase: " + currentPhaseNum.ToString();

        score += Time.deltaTime;

        if (batteryLevel == 0)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
            scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        }
    }

    public void UpdateBattery(int delta)
    {
        batteryLevel = Mathf.Max(0, Mathf.Min(100, batteryLevel + delta));
    }

    IEnumerator DelayDischarge() 
    {
        isDelayed = true;
        yield return new WaitForSecondsRealtime(decreaseDelay);
        isDelayed = false;
    }

    IEnumerator DoPhase()
    {
        currentPhase = true;
        yield return new WaitForSecondsRealtime(30);
        if (decreaseDelay > 0.05f)
            currentPhaseNum += 1;
        decreaseDelay = Mathf.Max(decreaseDelay - 0.05f, 0.05f);
        currentPhase = false;
    }

    
    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
