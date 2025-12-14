using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public void RunPhase()
    {
        SceneManager.LoadScene("GPS_Lvl2");
        Time.timeScale = 1;
    }
}
