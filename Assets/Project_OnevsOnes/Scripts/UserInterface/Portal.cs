using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public void RunPhase()
    {
        SceneManager.LoadScene("GPS_ActualLvl");
        Time.timeScale = 1;
    }
}
