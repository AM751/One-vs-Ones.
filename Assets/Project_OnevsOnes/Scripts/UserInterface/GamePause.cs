using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    
    [SerializeField] public Canvas gamePauseCanvas;
    public bool isPaused = false;
    
    void Start()
    {
        gamePauseCanvas.enabled = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            
        }
    }

    public void PauseGame()
    {
        gamePauseCanvas.enabled = true;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        gamePauseCanvas.enabled = false;
        Time.timeScale = 1;
        isPaused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("UI_MainMenu");
    }
    
}
