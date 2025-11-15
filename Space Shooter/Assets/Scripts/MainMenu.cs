using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string jogoSceneName = "Main";
    public string creditosSceneName = "Creditos";

    public void PlayGame()
    {
        SceneManager.LoadScene(jogoSceneName);
    }

    public void AbrirCreditos()
    {
        SceneManager.LoadScene(creditosSceneName);
    }

    public void SairDoJogo()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
}
