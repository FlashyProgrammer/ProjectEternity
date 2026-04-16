using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    [SerializeField] private Button changeButton;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private ParticleSystem particleVfxPlus;
    [SerializeField] private ParticleSystem particleVfxMinus;

    private float number = 0f;


    private void Start()
    {
        numberText.text = number.ToString();
    }

    public void DisplayText()
    {
        messageText.text = "Welcome!!!";
    }
    public void increaseNumber()
    {
        particleVfxPlus.Play();
        healthSlider.value += 1f;
        number++;
        numberText.text = number.ToString();
    }
        

    public void decreseIncrease()
    {
        particleVfxMinus.Play();
        healthSlider.value -= 1f;
        number--;
        numberText.text = number.ToString();
    }
        
}
