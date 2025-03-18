using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinClick : MonoBehaviour
{
    // Referencias principales
    public Button button; // Bot�n principal para sumar puntos
    public Button transferButton; // Bot�n de transferir puntos por Crypto
    public TMP_Text counterText; // Texto del contador de puntos
    public TMP_Text cryptoText; // Texto del contador de Crypto

    // Nueva funcionalidad del bot�n con temporizador
    public Button multiplierButton; // Bot�n del multiplicador
    public Image multiplierIcon; // Imagen asociada al bot�n (desaparecer� cuando el contador llegue a 0)
    public TMP_Text timerText; // Texto del temporizador en el bot�n
    public Color redColor = Color.red; // Color rojo del bot�n
    public Color greenColor = Color.green; // Color verde del bot�n

    [SerializeField] private double initialPoints = 0;
    private double counter;
    private float lastClickTime = 0f;
    private const float DOUBLE_CLICK_THRESHOLD = 0.5f;

    private int crypto = 0;
    private int pointsNeeded = 10000;
    private const int MAX_POINTS_NEEDED = 100000;
    private const int POINTS_INCREMENT = 10000;

    // Variables del multiplicador
    private bool isMultiplierActive = false; // Indica si el multiplicador est� activo
    private float multiplierDuration = 240f; // Duraci�n del multiplicador en segundos (4 minutos)
    private float multiplierTimer = 0f; // Tiempo restante del multiplicador
    private int multiplierLevel = 1; // Nivel del multiplicador (1 = x1, 2 = x2, 4 = x4)

    // Variables del temporizador del bot�n
    private float timer = 150f; // Tiempo inicial del contador en segundos (2:30 minutos)
    private bool isTimerRunning = true; // Indica si el temporizador est� activo

    void Start()
    {
        counter = initialPoints;

        // Asignar listeners a los botones
        button.onClick.AddListener(OnButtonClick);
        transferButton.onClick.AddListener(OnTransferButtonClick);
        multiplierButton.onClick.AddListener(OnMultiplierButtonClick);

        // Inicializar textos
        UpdateCounterText();
        UpdateCryptoText();
        UpdateTimerText();

        // Configurar el bot�n del multiplicador
        multiplierButton.image.color = redColor;
        multiplierIcon.enabled = true;
    }

    void Update()
    {
        // Actualizar el temporizador del bot�n
        if (isTimerRunning && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else if (isTimerRunning && timer <= 0)
        {
            // Cuando el temporizador llega a 0, cambiar el bot�n a verde
            isTimerRunning = false;
            multiplierButton.image.color = greenColor;
            multiplierIcon.enabled = false;
        }

        // Actualizar el temporizador del multiplicador
        if (isMultiplierActive)
        {
            multiplierTimer -= Time.deltaTime;
            if (multiplierTimer <= 0)
            {
                // Cuando el multiplicador termina, resetear al nivel base
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
        if (isTimerRunning)
        {
            // Si el temporizador est� activo, no hacer nada
            return;
        }

        // Activar el multiplicador
        isMultiplierActive = true;
        multiplierLevel *= 2;
        multiplierTimer = multiplierDuration;

        // Reiniciar el temporizador del bot�n
        timer = 150f;
        isTimerRunning = true;
        multiplierButton.image.color = redColor;
        multiplierIcon.enabled = true;
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

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = $"{minutes}:{seconds:D2}";
        }
    }
}