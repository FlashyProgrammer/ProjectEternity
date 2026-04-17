using UnityEngine;
using UnityEngine.UI;   

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float soulHealth;
    [SerializeField] private float soulDamage;

    private void Awake()
    {
        healthSlider.maxValue = soulHealth;
        healthSlider.value = soulHealth;

    }
    public void TakeDamage()
    {
        soulHealth -= soulDamage;
        healthSlider.value = soulHealth;


    }

}
