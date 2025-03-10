using UnityEngine;

public class GoalTrigger : MonoBehaviour
{

    [SerializeField] private int playerScored; // 1 para jugador 1, 2 para jugador 2
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balon")) // Asegúrate de que el balón tiene el tag "Ball"
        {
            // Llamamos al método de GameManager para sumar el gol
            gameManager.AddScore(playerScored);

            // Reiniciamos la posición del balón
            gameManager.ResetBallPosition();
        }
    }

}
