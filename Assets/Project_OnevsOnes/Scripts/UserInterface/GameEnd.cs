using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{ 
    public void backToMainMenu()
    {
        SceneManager.LoadScene("UI_MainMenu");
    }
}
