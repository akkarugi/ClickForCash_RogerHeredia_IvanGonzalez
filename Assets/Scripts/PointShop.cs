using UnityEngine;
using UnityEngine.UI;

public class PointShop : MonoBehaviour
{
    public Button add250PointsButton;
    public Button add3500PointsButton;
    public Button add22000PointsButton;
    public Button add50000PointsButton;

    public Button earnPointsAutomaticallyButton;
    public Button doubleClickPointsButton;
    public Button increaseMultiplierTo3Button;
    public Button increaseMultiplierTo5Button;

    public CoinClick coinClickScript;

    private bool isEarningAutomatically = false;
    private float autoEarnTimer = 0f;
    private const float AUTO_EARN_INTERVAL = 3f;

    void Start()
    {
        if (add250PointsButton != null)
            add250PointsButton.onClick.AddListener(() => coinClickScript.AddPoints(250));

        if (add3500PointsButton != null)
            add3500PointsButton.onClick.AddListener(() => coinClickScript.AddPoints(3500));

        if (add22000PointsButton != null)
            add22000PointsButton.onClick.AddListener(() => coinClickScript.AddPoints(22000));

        if (add50000PointsButton != null)
            add50000PointsButton.onClick.AddListener(() => coinClickScript.AddPoints(50000));

        if (earnPointsAutomaticallyButton != null)
            earnPointsAutomaticallyButton.onClick.AddListener(ActivateAutoEarn);

        if (doubleClickPointsButton != null)
            doubleClickPointsButton.onClick.AddListener(DoubleClickPoints);

        if (increaseMultiplierTo3Button != null)
            increaseMultiplierTo3Button.onClick.AddListener(IncreaseMultiplierTo3);

        if (increaseMultiplierTo5Button != null)
            increaseMultiplierTo5Button.onClick.AddListener(IncreaseMultiplierTo5);
    }

    void Update()
    {
        if (isEarningAutomatically)
        {
            autoEarnTimer += Time.deltaTime;
            if (autoEarnTimer >= AUTO_EARN_INTERVAL)
            {
                coinClickScript.AddPoints(1);
                autoEarnTimer = 0f;
            }
        }
    }

    private void ActivateAutoEarn()
    {
        isEarningAutomatically = true;
        earnPointsAutomaticallyButton.interactable = false;
    }

    private void DoubleClickPoints()
    {
        coinClickScript.multiplierLevel *= 2;
        doubleClickPointsButton.interactable = false;
    }

    private void IncreaseMultiplierTo3()
    {
        coinClickScript.multiplierFactor = 3;
        increaseMultiplierTo3Button.interactable = false;
    }

    private void IncreaseMultiplierTo5()
    {
        coinClickScript.multiplierFactor = 5;
        increaseMultiplierTo5Button.interactable = false;
    }
}