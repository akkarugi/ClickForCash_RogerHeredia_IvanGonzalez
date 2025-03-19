using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinClick : MonoBehaviour
{
    public Button button;
    public Button transferButton;
    public TMP_Text counterText;
    public TMP_Text cryptoText;

    public Button multiplierButton;
    public Image multiplierIcon;
    public TMP_Text timerText;
    public Color redColor = Color.red;
    public Color greenColor = Color.green;

    [SerializeField] private double initialPoints = 0;
    private double counter;
    private float lastClickTime = 0f;
    private const float DOUBLE_CLICK_THRESHOLD = 0.5f;

    private int crypto = 0;
    private int pointsNeeded = 10000;
    private const int MAX_POINTS_NEEDED = 100000;
    private const int POINTS_INCREMENT = 10000;

    private bool isMultiplierActive = false;
    [SerializeField] public int multiplierFactor = 2;
    private float multiplierDuration = 240f;
    private float multiplierTimer = 0f;
    public int multiplierLevel = 1;

    [SerializeField] private float resetTimerDuration = 150f;
    private float timer;
    private bool isTimerRunning = true;
    private bool isWaitingForClick = false;

    void Start()
    {
        counter = initialPoints;
        timer = resetTimerDuration;

        button.onClick.AddListener(OnButtonClick);
        transferButton.onClick.AddListener(OnTransferButtonClick);
        multiplierButton.onClick.AddListener(OnMultiplierButtonClick);

        UpdateCounterText();
        UpdateCryptoText();
        UpdateTimerText();

        multiplierButton.image.color = redColor;
        multiplierIcon.enabled = true;
    }

    void Update()
    {
        if (isTimerRunning && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else if (isTimerRunning && timer <= 0)
        {
            isTimerRunning = false;
            isWaitingForClick = true;
            multiplierButton.image.color = greenColor;
            multiplierIcon.enabled = false;
            timerText.text = "0:00";
        }

        if (isMultiplierActive)
        {
            multiplierTimer -= Time.deltaTime;
            if (multiplierTimer <= 0)
            {
                isMultiplierActive = false;
                multiplierLevel = 1;
            }
        }
    }

    private void OnButtonClick()
    {
        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        double pointsToAdd = timeSinceLastClick < DOUBLE_CLICK_THRESHOLD ? 1.5 : 1;
        counter += pointsToAdd * multiplierLevel;

        lastClickTime = currentTime;
        UpdateCounterText();
    }

    private void OnTransferButtonClick()
    {
        if (counter >= pointsNeeded)
        {
            counter -= pointsNeeded;
            crypto++;
            if (pointsNeeded < MAX_POINTS_NEEDED)
                pointsNeeded += POINTS_INCREMENT;
            UpdateCounterText();
            UpdateCryptoText();
        }
    }

    private void OnMultiplierButtonClick()
    {
        if (isWaitingForClick)
        {
            timer = resetTimerDuration;
            isTimerRunning = true;
            isWaitingForClick = false;
            multiplierButton.image.color = redColor;
            multiplierIcon.enabled = true;

            isMultiplierActive = true;
            multiplierLevel *= multiplierFactor;
            multiplierTimer = multiplierDuration;
        }
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
            counterText.text = $"{counter.ToString("F1")}/{pointsNeeded}";
    }

    private void UpdateCryptoText()
    {
        if (cryptoText != null)
            cryptoText.text = crypto.ToString();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = $"{minutes}:{seconds:D2}";
        }
    }

    public void AddPoints(double pointsToAdd)
    {
        counter += pointsToAdd;
        UpdateCounterText();
    }
}