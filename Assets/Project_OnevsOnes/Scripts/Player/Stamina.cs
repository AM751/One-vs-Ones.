using UnityEngine;
using UnityEngine.UI;
public class Stamina : MonoBehaviour
{
    [Header("Configuration")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float baseRegen = 10f;
    public float baseDrain = 30f; 

    [Header("Visual Feedback")]
    public Slider staminaSlider; 
    public Image fillImage;      
    public Color normalColor = Color.cyan;
    public Color exhaustedColor = Color.gray;

    [Header("State")]
    public bool isExhausted = false;

    void Start()
    {
        currentStamina = maxStamina;
        if(fillImage != null) fillImage.color = normalColor;
    }

    void Update()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += baseRegen * Time.deltaTime;
        }

        
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

       
        if (staminaSlider != null) 
        {
            staminaSlider.value = currentStamina / maxStamina;
        }

        
        if (currentStamina <= 0 && !isExhausted)
        {
            isExhausted = true; 
            if(fillImage != null) fillImage.color = exhaustedColor; 
        }
        else if (isExhausted && currentStamina > 20f)
        {
            isExhausted = false; 
            if(fillImage != null) fillImage.color = normalColor;
        }
    }

    
    public void DrainStamina(float deltaTime)
    {
        
        float multiplier = 1f;
        if (GameManager.Instance != null) 
        {
            multiplier = GameManager.Instance.staminaDrainMultiplier;
        }
        
        
        currentStamina -= (baseDrain * multiplier * deltaTime) + (baseRegen * deltaTime);
    }
    
    public void InstantDrain(float amount)
    {
        currentStamina -= amount;
    }
}
