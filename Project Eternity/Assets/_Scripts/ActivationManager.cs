using UnityEngine;

public class ActivationManager : MonoBehaviour
{
    [SerializeField] private EnemyAi activateEnemy;
    [SerializeField] private Platforms activatePlatform;
    [SerializeField] private ActivationManager nextActivation;
    [SerializeField] private Transform requiredKey;
    [SerializeField] private float keyAttachSpeed;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isTwoPart;
    [SerializeField] private int maxKeys;

    [SerializeField] private PlayerEffects playerConditions;
    [HideInInspector] public int numberOfKeys;
    private bool isCompleted;
    private Transform currentKey;

    private void Awake()
    {
        numberOfKeys = 0;

        if (isLocked)
        {
            if (activateEnemy != null)
            {
                activateEnemy.enabled = false;
            }

            if (activatePlatform != null)
            {
                activatePlatform.enabled = false;

            }

        }
    }
    private void Update()
    {

        if (currentKey != null)
        {
            if (currentKey == requiredKey)
            {
                currentKey.parent = null;
                currentKey.transform.position = Vector2.MoveTowards(currentKey.transform.position, transform.position, keyAttachSpeed);

                if (Vector2.Distance(currentKey.position, transform.position) < 0.01f)
                {
                    currentKey.gameObject.SetActive(false);
                    currentKey = null;
                }

                if (activateEnemy != null && isCompleted)
                {
                    activateEnemy.enabled = true;
                }

                if (activatePlatform != null && isCompleted)
                {
                    activatePlatform.enabled = true;
                    if (playerConditions != null)
                    {
                        playerConditions.GetComponent<PlayerEffects>().DropSoul();

                    }
                    activatePlatform.isToBeActivated = false;

                }

            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerConditions = other.GetComponent<PlayerEffects>();
        if (other.CompareTag("Player") && !isTwoPart)
        {
            currentKey = other.GetComponent<PlayerEffects>().followObject;

            if (currentKey != null)
            {
                if (currentKey == requiredKey)
                {
                    playerConditions.DropSoul();
                    playerConditions.DropSoul();
                    isCompleted = true;

                }
            }

        }

        if (other.CompareTag("Player") && isTwoPart)
        {

            currentKey = other.GetComponent<PlayerEffects>().followObject;

            if (currentKey != null)
            {
                if (currentKey == requiredKey)
                {
                    playerConditions.DropSoul();
                    numberOfKeys++;

                    if (numberOfKeys + nextActivation.numberOfKeys == maxKeys)
                    {
                        playerConditions.DropSoul();
                        isCompleted = true;
                    }

                }
            }
        }




    }
    private void OnTriggerStay2D(Collider2D other)
    {
        playerConditions = other.GetComponent<PlayerEffects>();
        if (other.CompareTag("Player") && !isTwoPart)
        {

            if (currentKey == requiredKey)
            {
                playerConditions.DropSoul();
                isCompleted = true;

            }

        }

        if (other.CompareTag("Player") && isTwoPart)
        {
            currentKey = other.GetComponent<PlayerEffects>().followObject;

            if (currentKey != null)
            {
                if (currentKey == requiredKey)
                {
                    if (numberOfKeys + nextActivation.numberOfKeys == maxKeys)
                    {
                        playerConditions.DropSoul();
                        isCompleted = true;
                    }

                }
            }
        }
    }
}
