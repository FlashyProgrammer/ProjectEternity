using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Enable/Disable")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float controllerDisableTime;


    [Header("UI")]
    [SerializeField] private GameObject selectionWindow;
    [SerializeField] private GameObject abilityWindow; 
    [SerializeField] private Button optionButtonOne;
    [SerializeField] private Button optionButtonTwo;

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
            if (Input.GetKeyDown(KeyCode.E))
            {
                optionButtonOne.Select();
            }
        }

        if (abilityWindow.activeInHierarchy)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                optionButtonTwo.Select();
            }
        }
    }


    private IEnumerator EnableTime()
    {
      
        yield return new WaitForSeconds(controllerDisableTime);
        playerObject.SetActive(true);
        controller.enabled = true;


    }
}
 