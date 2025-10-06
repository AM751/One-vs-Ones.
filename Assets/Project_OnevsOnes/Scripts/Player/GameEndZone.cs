using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndZone : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] public Canvas gameEndCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameEndCanvas.enabled = false;
        StartCoroutine(GameEndDelay());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject())
        {
            gameEndCanvas.enabled = true;
            Time.timeScale = 0;
            StartCoroutine(GameEndDelay());
        }
    }

    IEnumerator GameEndDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("UI_MainMenu");
    }
}
