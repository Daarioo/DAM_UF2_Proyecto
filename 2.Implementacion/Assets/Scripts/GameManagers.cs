using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private AudioSource stadiumAmbience;
    [SerializeField] private AudioSource whistleSound;

    private int player1Score = 0;
    private int player2Score = 0;
    private float timeRemaining = 90f;
    private bool gameActive = true;

    private GameObject ball;
    private GameObject player1;
    private GameObject player2;

    private void Start()
    {
        ball = GameObject.Find("Balon");
        player1 = GameObject.Find("Lamine Yamal");
        player2 = GameObject.Find("Iniesta");

        UpdateScoreUI();
        UpdateTimerUI();
        countdownText.gameObject.SetActive(false);

        stadiumAmbience.Play();
        whistleSound.Play();
    }

    private void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                gameActive = false;
                timerText.text = "¡Tiempo terminado!";
                GuardarResultadosYSalir();
            }
        }
    }

    public void AddScore(int player)
    {
        if (player == 1) player2Score++;
        else player1Score++;

        UpdateScoreUI();
        whistleSound.Play();
        StartCoroutine(ResetWithCountdown());
    }

    private IEnumerator ResetWithCountdown()
    {
        gameActive = false;
        FreezeAll(true);
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);
        ResetBallPosition();
        yield return new WaitForSeconds(0.5f);

        Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
        StartCoroutine(DropBall(rbBall));

        gameActive = true;
        FreezeAll(false);
    }

    public void ResetBallPosition()
    {
        ball.transform.position = new Vector2(0, 3);
        Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
        rbBall.simulated = false;
        rbBall.linearVelocity = Vector2.zero;

        player1.transform.position = new Vector2(-6, -2);
        player2.transform.position = new Vector2(6, -2);

        Rigidbody2D rbPlayer1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rbPlayer2 = player2.GetComponent<Rigidbody2D>();
        rbPlayer1.linearVelocity = Vector2.zero;
        rbPlayer2.linearVelocity = Vector2.zero;
    }

    private IEnumerator DropBall(Rigidbody2D rbBall)
    {
        yield return new WaitForSeconds(0.5f);
        ball.transform.position = Vector2.zero;
        rbBall.simulated = true;
    }

    private void FreezeAll(bool freeze)
    {
        Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
        Rigidbody2D rbPlayer1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rbPlayer2 = player2.GetComponent<Rigidbody2D>();

        rbBall.simulated = !freeze;
        rbPlayer1.simulated = !freeze;
        rbPlayer2.simulated = !freeze;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"{player1Score} - {player2Score}";
    }

    private void UpdateTimerUI()
    {
        int minutos = Mathf.FloorToInt(timeRemaining / 60);
        int segundos = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Tiempo: {minutos}:{segundos:00}";
    }

    private void GuardarResultadosYSalir()
    {
        PlayerPrefs.SetInt("GolesJugador", player1Score);
        PlayerPrefs.SetInt("GolesRival", player2Score);
        PlayerPrefs.Save();
        SceneManager.LoadScene(3);
    }
}
