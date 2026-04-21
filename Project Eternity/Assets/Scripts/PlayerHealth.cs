using System.Collections;
using UnityEngine;
using UnityEngine.UI;   

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float decreaseTime;
    public float maxHealth;
    public float soulHealth;

    private void Start()
    {
        healthSlider.value = maxHealth;
    }

    private void Update()
    {
        if (soulHealth <= 0)
        {
            uiManager.DeathScreen();
            soulHealth = maxHealth;
        }
        healthSlider.value = soulHealth;
    }

    private void Awake()
    {
        healthSlider.maxValue = soulHealth;
        healthSlider.value = soulHealth;

    }
    public void TakeDamage(float amount)
    {
        soulHealth -= amount;
        healthSlider.value = soulHealth;


    }

}
