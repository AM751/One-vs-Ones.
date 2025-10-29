using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    
    [SerializeField] public Canvas gamePauseCanvas;
    public bool isPaused = false;
    private InputSystem_Actions _inputActions;
    
    void Start()
    {
        gamePauseCanvas.enabled = false;
    }

    void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Pause.performed += Pause;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Pause.performed -= Pause;
        _inputActions.Disable();
    }

    void Pause(InputAction.CallbackContext context)
    {
        if (_inputActions.Player.Pause.enabled)
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
