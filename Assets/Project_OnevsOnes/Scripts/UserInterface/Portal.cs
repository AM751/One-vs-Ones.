using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public void RunPhase()
    {
        SceneManager.LoadScene("");
        Time.timeScale = 1;
    }
}
