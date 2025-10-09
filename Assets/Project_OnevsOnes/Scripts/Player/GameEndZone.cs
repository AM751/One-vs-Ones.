using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndZone : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject())
        {
            SceneManager.LoadScene("UI_GameEnd");
        }
    }
    
}
