using System.Collections;
using UnityEngine;
using UnityEngine.UI;   

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float soulHealth;
    [SerializeField] private float decreaseTime;

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
