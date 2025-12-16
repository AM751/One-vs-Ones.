using UnityEngine;
using System.IO;

public class MetricsLogger : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        // On Mac, this saves to my Unity Project's "Assets" folder
        filePath = Application.dataPath + "/PlaytestMetrics.csv";

        // Create the file headers if it doesn't exist yet
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Time Survived,Phase Reached,Cause of Death,Speed Multiplier\n");
        }
    }

    public void LogData(float time, string reason, int phase)
    {
        // Get the speed from the Manager
        float speed = 1.0f;
        if (GameManager.Instance != null) speed = GameManager.Instance.globalSpeedMultiplier;

        // Format the line: "45.2, 2, Hit Red Block, 1.2"
        string line = $"{time},{phase},{reason},{speed}\n";

        // Write to file
        File.AppendAllText(filePath, line);
        Debug.Log("Stats saved to: " + filePath);
    }
}
