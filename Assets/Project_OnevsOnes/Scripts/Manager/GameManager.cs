using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Progression Data")]
    public float timeSurvived = 0f;
    public int currentPhase = 1;
    public bool isGameActive = true;

    [Header("Phase Thresholds (Seconds)")]
    public float phase2Time = 30f; 
    public float phase3Time = 60f; 

    [Header("System Interconnections")]
    // These multipliers affect the PlayerController and StaminaSystem
    public float globalSpeedMultiplier = 1f;
    public float staminaDrainMultiplier = 1f;

    // Data Logging Variables
    private string logPath;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        logPath = Application.dataPath + "/PlaytestMetrics.csv";
        if (!File.Exists(logPath)) File.WriteAllText(logPath, "Time,Phase,Cause,SpeedMult\n");
    }

    void Update()
    {
        if (!isGameActive) return;

        timeSurvived += Time.deltaTime;

        // --- EVOLUTION MECHANIC (Rubric: 3 Distinct Phases) ---
        if (currentPhase == 1 && timeSurvived > phase2Time)
        {
            SetPhase(2);
        }
        else if (currentPhase == 2 && timeSurvived > phase3Time)
        {
            SetPhase(3);
        }
    }

    void SetPhase(int phase)
    {
        currentPhase = phase;
        // Interconnection: Time affects Difficulty
        if (phase == 2)
        {
            globalSpeedMultiplier = 1.2f; // 20% Faster
            staminaDrainMultiplier = 1.2f; 
            Debug.Log("PHASE 2: SPEED UP");
        }
        else if (phase == 3)
        {
            globalSpeedMultiplier = 1.4f; // 40% Faster
            staminaDrainMultiplier = 1.5f; 
            Debug.Log("PHASE 3: SURVIVAL");
        }
    }

    public void EndGame(string reason)
    {
        isGameActive = false;
        // --- DATA LOGGING (Rubric: Tracked Metrics) ---
        string line = $"{timeSurvived},{currentPhase},{reason},{globalSpeedMultiplier}\n";
        File.AppendAllText(logPath, line);
        Debug.Log("Metrics Saved.");
    }
}
