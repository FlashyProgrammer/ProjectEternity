using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI flashingText;
    [SerializeField] private float flashTime;
    [SerializeField] private float textSpeed;
    [SerializeField] private float textTime;
    [SerializeField] private Image portraitPos; 
    private Queue<string> lineList;
    void Awake()
    {
        lineList = new Queue<string>();
    }

    public void BeginDialogue(Character character)
    {
        lineList.Clear();
        portraitPos.sprite = character.characterSprite;
        foreach (string line in character.lines) 
        {
            lineList.Enqueue(line);
        }
        StartCoroutine(Flash());
        DisplayNextLine();
        
    }

    public void DisplayNextLine()
    {
        if (lineList.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = lineList.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Flash());
        StartCoroutine(TypeLine(line));
        StartCoroutine(DisplayTimer());
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            yield return new WaitForSeconds(textSpeed);
            dialogueText.text += letter;
           
        }
    }
    IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(textTime);
        DisplayNextLine(); 
    }
    
    private void EndDialogue()
    {
        dialogueText.text = "";
    }

    IEnumerator Flash()
    {
        while (lineList.Count != 0)
        {
            flashingText.enabled = true;
            yield return new WaitForSeconds(flashTime);
            flashingText.enabled = false;
            yield return new WaitForSeconds(flashTime);

        }
    }

    
    
}
