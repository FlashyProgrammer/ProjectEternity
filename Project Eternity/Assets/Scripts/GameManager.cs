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

    [Header("Scene Management")]
    [SerializeField] private Scene deathScene;
    [SerializeField] private Scene menuScene;
    [SerializeField] private Scene gameScene;
    [SerializeField] private Scene startScene;

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
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                optionButtonOne.Select();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                optionButtonTwo.Select();
            }
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
 