using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Enable/Disable")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float controllerDisableTime;
    [SerializeField] private PlayerAbilities ability;
    [SerializeField] private PlayerEffects effects;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ActionManager actionManager;


    [Header("UI")]
    [SerializeField] private GameObject selectionWindow;
    [SerializeField] private GameObject selectionWindowBig;
    [SerializeField] private GameObject abilityWindow;
    [SerializeField] private GameObject keyPrompt;
    [SerializeField] private Button optionButtonOne;
    [SerializeField] private Button optionButtonTwo;
    [SerializeField] private Button optionButtonOneBig;
    [SerializeField] private Button optionButtonTwoBig;
    [SerializeField] private Button abilityButtonOne;
    [SerializeField] private Button abilityButtonTwo;
    [SerializeField] private GameObject pauseMenu;

    [Header("Animations")]
    [SerializeField] private Animator portaritCharacter;
    [SerializeField] private Animator gameViewWindow;

    [Header("Dialogue")]
    [SerializeField] private DialogueManager dialogueManager;


    private int pauseCounter;
    private PlayerController controller;

    private void Awake()
    {
        controller = playerObject.gameObject.GetComponent<PlayerController>();
    }
    private void Update()
    {
        
        if (!playerObject.activeInHierarchy)
        {
            StartCoroutine(EnableTime());  
        }

        if (selectionWindowBig.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                optionButtonOneBig.Select();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                optionButtonTwoBig.Select();
            }

            if (!dialogueManager.dialogueStarted)
            {
                DecreaseGameview();
            }

        }

        if (abilityWindow.activeInHierarchy)
        {

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                abilityButtonOne.Select();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                abilityButtonOne.Select();
            }


        }


        if (selectionWindow.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                selectionWindowBig.SetActive(true);
                selectionWindow.SetActive(false);
                optionButtonOne.Select();
                DecreaseGameview();

            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                selectionWindowBig.SetActive(true);
                selectionWindow.SetActive(false);
                optionButtonTwo.Select();
                DecreaseGameview();

            }

            if (!dialogueManager.dialogueStarted)
            {
                IncreaseGameview();
            }

        }

        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            if (!dialogueManager.dialogueStarted)
            {
                selectionWindow.SetActive(true);
                selectionWindowBig.SetActive(false);
                abilityWindow.SetActive(false);
                IncreaseGameview();
              
            }

            if (!selectionWindow.activeInHierarchy && !keyPrompt.activeInHierarchy)
            {
                selectionWindow.SetActive(true);
                selectionWindowBig.SetActive(false);
                abilityWindow.SetActive(false);
            }
           
        }

        if(SceneManager.GetSceneByName("Death Screen").isLoaded)
        {
            GetComponent<PlayerHealth>().soulHealth = GetComponent<PlayerHealth>().maxHealth;
            playerObject.transform.position = playerObject.GetComponent<PlayerEffects>().currentCheckPoint.position;
        }

 

        if ((Input.GetKeyUp(KeyCode.Escape)) && playerObject.activeInHierarchy)
        {
            switch(pauseCounter)
            {   case 0:
                    pauseCounter += 2;
                    Cursor.lockState = CursorLockMode.None;
                    pauseMenu.SetActive(true);
                    selectionWindowBig.SetActive(false);
                    selectionWindow.SetActive(false);
                    abilityWindow.SetActive(false);
                    playerController.enabled = false;
                    ability.enabled = false;
                    actionManager.enabled = false;


                    if (abilityWindow.activeInHierarchy)
                    {
                        abilityButtonOne.Select();
                    }

                    if (selectionWindowBig.activeInHierarchy)
                    {
                       optionButtonOneBig.Select();
                    }

                    if (selectionWindow.activeInHierarchy)
                    {
                        optionButtonOne.Select();
                    }

                    ability.ResetAbility();
                    Time.timeScale = 0f;
                    break;
                case 1:
                    break;
                case 2:
                    pauseMenu.SetActive(false);
                    if (!keyPrompt.activeInHierarchy)
                    {
                        selectionWindow.SetActive(true);

                    }
                    selectionWindowBig.SetActive(false);
                    abilityWindow.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    pauseCounter = 0;
                    Time.timeScale = 1f;
                    playerController.enabled = true;
                    ability.enabled = true;
                    actionManager.enabled = true;
                    break;
            }
        }

    }

    private void DecreaseGameview()
    {
        gameViewWindow.Play("Game View_Shrink");
        portaritCharacter.Play("Portrait_Grow");
    }

    private void IncreaseGameview()
    {
   
        gameViewWindow.Play("Game View_Grow");
        portaritCharacter.Play("Portrait_Shrink");
    }
    private IEnumerator EnableTime()
    {
        pauseCounter = 0;
        yield return new WaitForSeconds(controllerDisableTime);
        ability.DisableAbilities();
        playerObject.SetActive(true);
        controller.enabled = true;


    }


}
 