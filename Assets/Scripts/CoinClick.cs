using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinClick : MonoBehaviour
{
    public Button button;
    public Button transferButton;
    public TMP_Text counterText;
    public TMP_Text cryptoText;

    [SerializeField] private double initialPoints = 0;
    private double counter;
    private float lastClickTime = 0f;
    private const float DOUBLE_CLICK_THRESHOLD = 0.5f;

    private int crypto = 0;
    private int pointsNeeded = 10000;
    private const int MAX_POINTS_NEEDED = 100000;
    private const int POINTS_INCREMENT = 10000;

    void Start()
    {
        counter = initialPoints;
        button.onClick.AddListener(OnButtonClick);
        transferButton.onClick.AddListener(OnTransferButtonClick);
        UpdateCounterText();
        UpdateCryptoText();
    }

    private void OnButtonClick()
    {
        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        if (timeSinceLastClick < DOUBLE_CLICK_THRESHOLD)
            counter += 1.5;
        else
            counter += 1;

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

    private void UpdateCounterText()
    {
        if (counterText != null)
            counterText.text = $"{counter.ToString("F1")}/{pointsNeeded}";
    }

    private void UpdateCryptoText()
    {
        if (cryptoText != null)
            cryptoText.text = $"Crypto: {crypto}";
    }
}