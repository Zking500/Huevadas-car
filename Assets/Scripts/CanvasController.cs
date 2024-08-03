using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Referencia al Canvas de perder
    public GameObject WinCanvas;
    public GameObject loseCanvas;
    // Referencia al Text donde se mostrará el nombre del ganador
    public Text winnerText;

    private void Start() {
        loseCanvas.SetActive(false);
        WinCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Gana el jugador, puedes mostrar un mensaje o realizar alguna acción
            WinCanvas.SetActive(true);
        }
        // Si el objeto que colisiona tiene el tag "Bot"
        else if (other.CompareTag("Bot"))
        {
            // Mostrar el Canvas de perder
            loseCanvas.SetActive(true);
            // Actualizar el Text con el nombre del objeto que ganó
            winnerText.text = "El ganador es: " + other.gameObject.name;
        }
    }

    // Método para reiniciar el escenario
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
