using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCaught : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject == _player)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("UI_GameEnd");
        }
    }
}
