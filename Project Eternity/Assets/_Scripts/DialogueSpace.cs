using UnityEngine;
using UnityEngine.UI;

public class DialogueSpace : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueToTrigger;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject markSprite;
    [SerializeField] private Button talkButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talkButton.onClick.AddListener(dialogueToTrigger.EnableTrigger);
            markSprite.SetActive(true);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            markSprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (markSprite != null)
        {
            markSprite.SetActive(false);

        }
        talkButton.onClick.RemoveListener(dialogueToTrigger.EnableTrigger);
    }
}
