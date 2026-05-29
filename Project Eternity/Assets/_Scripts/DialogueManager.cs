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

    [Header("Animations")]
    [SerializeField] private Animator portraitAnim;
    [SerializeField] private Animator gameViewAnim;
    private Queue<string> lineList;

    public bool dialogueStarted;
    private string currentLine;


    
    void Awake()
    {
        lineList = new Queue<string>();
    }

    public void BeginDialogue(Character character)
    {
        dialogueStarted = true;
        lineList.Clear();
        portraitAnim.SetTrigger("Grow");
        gameViewAnim.SetTrigger("Shrink");
        portraitPos.sprite = character.characterSprite;

        foreach (string line in character.lines)
        {
            lineList.Enqueue(line);
        }

        StartCoroutine(Flash());
        currentLine = lineList.Dequeue();
        StartCoroutine(TypeLine(currentLine));
        DisplayNextLine();

    }

    public void DisplayNextLine()
    {
        if (dialogueText.text == currentLine)
        {
            if (lineList.Count == 0)
            {
                EndDialogue();
                return;
            }

            currentLine = lineList.Dequeue();


            StopAllCoroutines();
            StartCoroutine(Flash());
            StartCoroutine(TypeLine(currentLine));
        }

        else
        {
            StopAllCoroutines();
            completeLine(currentLine);
        }
        
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            yield return new WaitForSeconds(textSpeed);
            dialogueText.text += letter;
            yield return null;
        }

    }
    private void completeLine(string line)
    {
        dialogueText.text = "";
        dialogueText.text = line;

        if (lineList.Count == 0)
        {
            EndDialogue();
            return;
        }
    }

    private void EndDialogue()
    {
        dialogueText.text = "";
        dialogueStarted = false;
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
