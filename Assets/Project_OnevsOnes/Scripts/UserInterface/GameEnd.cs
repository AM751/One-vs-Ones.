using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void backToMainMenu()
    {
        SceneManager.LoadScene("UI_MainMenu");
    }
}
