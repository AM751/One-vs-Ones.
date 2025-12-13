using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField] private Canvas _instructionCanvas;
    public void StartGame()
    {
        SceneManager.LoadScene("GPS_Lvl1");
        Time.timeScale = 1;
    }

    public void goToInstructions()
    {
        _instructionCanvas.enabled = true;
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("UI_MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
