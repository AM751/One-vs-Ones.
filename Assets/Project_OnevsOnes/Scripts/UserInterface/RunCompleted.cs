using UnityEngine;
using UnityEngine.SceneManagement;

public class RunCompleted : MonoBehaviour
{
    public void AgainMainMenu()
    {
        SceneManager.LoadScene("UI_MainMenu");
    }
}
