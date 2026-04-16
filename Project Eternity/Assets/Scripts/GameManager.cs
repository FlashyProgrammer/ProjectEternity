using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Enable/Disable")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float controllerDisableTime;

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
    }


    private IEnumerator EnableTime()
    {
      
        yield return new WaitForSeconds(controllerDisableTime);
        playerObject.SetActive(true);
        controller.enabled = true;


    }
}
