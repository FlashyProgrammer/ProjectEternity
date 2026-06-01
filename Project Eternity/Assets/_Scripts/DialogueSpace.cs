using UnityEngine;
using UnityEngine.UI;

public class DialogueSpace : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueToTrigger;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject markSprite;
    [SerializeField] private Button talkButtonSmall;
    [SerializeField] private Button talkButtonBig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talkButtonSmall.onClick.AddListener(dialogueToTrigger.EnableTrigger);
            talkButtonBig.onClick.AddListener(dialogueToTrigger.EnableTrigger);
            dialogueToTrigger.EnableTrigger();
            markSprite.SetActive(true);
        }

        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            markSprite.SetActive(true);
        }

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                dialogueManager.DisplayNextLine();
            } 


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (markSprite != null)
        {
            markSprite.SetActive(false);

        }
        talkButtonSmall.onClick.RemoveListener(dialogueToTrigger.EnableTrigger);
        talkButtonBig.onClick.RemoveListener(dialogueToTrigger.EnableTrigger);
    }
}
