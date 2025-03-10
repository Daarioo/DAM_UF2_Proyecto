using UnityEngine;
using TMPro; // Para TextMeshPro
using UnityEngine.SceneManagement; // Para cambio de escena

public class SceneLoader : MonoBehaviour
{
    public TMP_Text pressKeyText;    // Texto que parpadea
    public float blinkSpeed = 1f;    // Velocidad del parpadeo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // Primera escena
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // Enter
        {
            SceneManager.LoadScene(2); // Tercera escena
        }

        BlinkText();

        if (Input.anyKeyDown) // Cualquier tecla para avanzar de escena
        {
            LoadNextScene();
        }
    }

    void BlinkText()
    {
        if (pressKeyText != null)
        {
            pressKeyText.alpha = Mathf.PingPong(Time.time * blinkSpeed, 1);
        }
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Evita error si es la última escena
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
