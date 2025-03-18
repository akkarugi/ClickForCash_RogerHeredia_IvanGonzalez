using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinClick : MonoBehaviour
{
    public Button button;
    public TMP_Text counterText;

    private float counter = 0f;
    private float lastClickTime = 0f;
    private const float DOUBLE_CLICK_THRESHOLD = 0.5f;

    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        UpdateCounterText();
    }

    private void OnButtonClick()
    {
        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        if (timeSinceLastClick < DOUBLE_CLICK_THRESHOLD)
        {
            counter += 1.5f;
        }
        else
        {
            counter += 1f;
        }

        lastClickTime = currentTime;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = counter.ToString("F1");
        }
    }
}