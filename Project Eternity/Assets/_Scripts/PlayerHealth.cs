using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;
    [SerializeField] private Slider healthSlider;
    public float maxHealth;
    public float soulHealth;
    float healthPercent => soulHealth / maxHealth;

    public Image healthIcon;

    public Sprite greenIcon;
    public Sprite yellowIcon;
    public Sprite redIcon;

    public Volume volume;
    private Vignette vignette;

    [Header("Low Health Overlay")]
    public Image damageOverlay;

    public float maxOverlayAlpha = 0.25f;
    public float minOverlayAlpha = 0f;

    public float fadeSpeed = 3f;
    float currentOverlayAlpha;
    public float overlayStartThreshold = 0.4f;


    private void Start()
    {
        healthSlider.value = maxHealth;
        UpdateHealthIcon();
        volume.profile.TryGet(out vignette);
    }
    void UpdateHealthIcon()
    {
        float healthPercent = soulHealth / maxHealth;

        if (healthPercent > 0.66f)
        {
            healthIcon.sprite = greenIcon;
        }
        else if (healthPercent > 0.33f)
        {
            healthIcon.sprite = yellowIcon;
        }
        else
        {
            healthIcon.sprite = redIcon;
        }
    }
    
    private void Update()
    {
        if (soulHealth <= 0)
        {
            uiManager.DeathScreen();
            soulHealth = maxHealth;
        }
        healthSlider.value = soulHealth;
        UpdateHealthIcon();
        UpdateOverlay();
        UpdateVignette();
    }

    float GetTargetOverlayAlpha()
    {
        if (healthPercent > overlayStartThreshold)
            return 0f;

        // start VERY soft at first
        if (healthPercent > 0.25f)
            return 0.05f;

        // danger zone
        return maxOverlayAlpha;
    }

    void UpdateOverlay()
    {
        float target = GetTargetOverlayAlpha();

        currentOverlayAlpha = Mathf.Lerp(
            currentOverlayAlpha,
            target,
            Time.deltaTime * 3f
        );

        float pulse = 0f;

        // ONLY pulse under 40% HP
        if (healthPercent <= overlayStartThreshold)
        {
            float speed = Mathf.Lerp(2f, 8f, 1f - healthPercent);
            pulse = Mathf.Sin(Time.time * speed) * 0.03f;
        }

        float finalAlpha = Mathf.Clamp01(currentOverlayAlpha + pulse);

        damageOverlay.color = new Color(0.6f, 0f, 0f, finalAlpha);
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

    public void GainHealth(float amount)
    {
        soulHealth += amount;
    }
    void UpdateVignette()
    {
        float targetIntensity = 0f;

        // start vignette at 40% HP
        if (healthPercent <= 0.4f)
        {
            targetIntensity = (1f - healthPercent) * 0.5f;
        }

        vignette.intensity.value = Mathf.Lerp(
            vignette.intensity.value,
            targetIntensity,
            Time.deltaTime * 3f
        );
    }

}
