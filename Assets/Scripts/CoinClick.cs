using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinClick : MonoBehaviour
{
    // Referencias principales
    public Button button; // Botón principal para sumar puntos
    public Button transferButton; // Botón de transferir puntos por Crypto
    public TMP_Text counterText; // Texto del contador de puntos
    public TMP_Text cryptoText; // Texto del contador de Crypto

    // Nueva funcionalidad del botón con temporizador
    public Button multiplierButton; // Botón del multiplicador
    public Image multiplierIcon; // Imagen asociada al botón (desaparecerá cuando el contador llegue a 0)
    public TMP_Text timerText; // Texto del temporizador en el botón
    public Color redColor = Color.red; // Color rojo del botón
    public Color greenColor = Color.green; // Color verde del botón

    [SerializeField] private double initialPoints = 0;
    private double counter;
    private float lastClickTime = 0f;
    private const float DOUBLE_CLICK_THRESHOLD = 0.5f;

    private int crypto = 0;
    private int pointsNeeded = 10000;
    private const int MAX_POINTS_NEEDED = 100000;
    private const int POINTS_INCREMENT = 10000;

    // Variables del multiplicador
    private bool isMultiplierActive = false; // Indica si el multiplicador está activo
    [SerializeField] private float multiplierDuration = 240f; // Duración del multiplicador en segundos (editable en el Inspector)
    [SerializeField] private int multiplierFactor = 2; // Factor de multiplicación (editable en el Inspector)
    private float multiplierTimer = 0f; // Tiempo restante del multiplicador
    private int multiplierLevel = 1; // Nivel del multiplicador (1 = x1, 2 = x2, 4 = x4)

    // Variables del temporizador del botón
    [SerializeField] private float resetTimerDuration = 150f; // Duración del temporizador de reseteo en segundos (2:30 minutos, editable en el Inspector)
    private float timer; // Tiempo restante del temporizador
    private bool isTimerRunning = true; // Indica si el temporizador está activo
    private bool isWaitingForClick = false; // Indica si el botón está esperando un clic después de llegar a 0

    void Start()
    {
        counter = initialPoints;
        timer = resetTimerDuration; // Inicializar el temporizador con el valor configurado

        // Asignar listeners a los botones
        button.onClick.AddListener(OnButtonClick);
        transferButton.onClick.AddListener(OnTransferButtonClick);
        multiplierButton.onClick.AddListener(OnMultiplierButtonClick);

        // Inicializar textos
        UpdateCounterText();
        UpdateCryptoText();
        UpdateTimerText();

        // Configurar el botón del multiplicador
        multiplierButton.image.color = redColor;
        multiplierIcon.enabled = true;
    }

    void Update()
    {
        // Actualizar el temporizador del botón
        if (isTimerRunning && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else if (isTimerRunning && timer <= 0)
        {
            // Cuando el temporizador llega a 0, cambiar el botón a verde
            isTimerRunning = false;
            isWaitingForClick = true;
            multiplierButton.image.color = greenColor;
            multiplierIcon.enabled = false;
            timerText.text = "0:00"; // Mostrar 0:00 en el temporizador
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
        if (isWaitingForClick)
        {
            // Reiniciar el temporizador del botón
            timer = resetTimerDuration;
            isTimerRunning = true;
            isWaitingForClick = false;
            multiplierButton.image.color = redColor;
            multiplierIcon.enabled = true;

            // Activar el multiplicador
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
            cryptoText.text = crypto.ToString(); // Solo muestra el número de Crypto
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