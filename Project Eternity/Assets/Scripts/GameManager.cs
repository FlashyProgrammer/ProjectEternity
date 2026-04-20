using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VectorGraphics;

public class GameManager : MonoBehaviour
{
    [Header("Enable/Disable")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float controllerDisableTime;
    [SerializeField] private PlayerAbilities ability;

    [Header("UI")]
    [SerializeField] private GameObject selectionWindow;
    [SerializeField] private GameObject abilityWindow;
    [SerializeField] private Button optionButtonOne;
    [SerializeField] private Button optionButtonTwo;
    [SerializeField] private Button abilityButtonOne;
    [SerializeField] private Button abilityButtonTwo;


 
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

        if (selectionWindow.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                optionButtonOne.Select();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                optionButtonTwo.Select();
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Button pressed");
            optionButtonOne.Select();
            selectionWindow.SetActive(true);
            abilityWindow.SetActive(false);
        }
 

    }


    private IEnumerator EnableTime()
    {
      
        yield return new WaitForSeconds(controllerDisableTime);
        ability.DisableAbilities();
        playerObject.SetActive(true);
        controller.enabled = true;


    }
}
 