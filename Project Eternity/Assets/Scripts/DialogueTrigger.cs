using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Character[] characterList;
    private int characterCount;

    private void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
            Debug.Log("Dialogue triggered");
            FindAnyObjectByType<DialogueManager>().BeginDialogue(characterList[characterCount]);
        
    }

    public void nextCharacter()
    {
        characterCount++;
        Debug.Log(characterCount);

        if (characterCount < characterList.Length)
        {
            FindAnyObjectByType<DialogueManager>().BeginDialogue(characterList[characterCount]);
        }
        else 
        {
            characterCount = 0;
        }

    }
}
