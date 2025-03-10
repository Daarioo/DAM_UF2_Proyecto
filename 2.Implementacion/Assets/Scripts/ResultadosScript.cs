using TMPro;
using UnityEngine;

public class ResultadosScript : MonoBehaviour
{
    public TextMeshProUGUI resultadoTexto;
    public TextMeshProUGUI resultadoTexto2; // Referencia al Text en la UI
    public GameObject imagenVictoria; // Imagen que aparece cuando ganas
    public GameObject imagenDerrota;  // Imagen que aparece cuando pierdes

    private int golesJugador;
    private int golesRival;

    void Start()
    {
        // Recuperar los valores de la escena anterior
        golesJugador = PlayerPrefs.GetInt("GolesJugador", 0);
        golesRival = PlayerPrefs.GetInt("GolesRival", 0);

        MostrarResultado();
    }

    void MostrarResultado()
    {
        if (golesJugador > golesRival)
        {
            resultadoTexto.text = $"Resultado: {golesJugador}-{golesRival}"; 
            resultadoTexto2.text = "Has Ganado :)";
            imagenVictoria.SetActive(true);
            imagenDerrota.SetActive(false);
        }
        else if (golesJugador < golesRival)
        {
            resultadoTexto.text = $"Resultado: {golesJugador}-{golesRival}"; 
            resultadoTexto2.text = "Has Perdido :(";
            resultadoTexto2.color = Color.red;
            imagenVictoria.SetActive(false);
            imagenDerrota.SetActive(true);
        }
        else
        {
            resultadoTexto.text = $"Resultado: {golesJugador}-{golesRival}"; 
            resultadoTexto2.text = "Empate :/";
            resultadoTexto2.color = Color.yellow;
            imagenVictoria.SetActive(false);
            imagenDerrota.SetActive(false);
        }
    }
}
