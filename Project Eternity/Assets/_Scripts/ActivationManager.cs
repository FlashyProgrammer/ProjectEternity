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

    public int numberOfKeys;
    private bool isCompleted;
    private Transform placedKey;
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
        if (placedKey != null)
        {
            if (placedKey == requiredKey)
            {
                placedKey.parent = null;
                placedKey.transform.position = Vector2.MoveTowards(placedKey.transform.position, transform.position, keyAttachSpeed);

                if (Vector2.Distance(placedKey.position, transform.position) < 0.01f)
                {
                    placedKey.gameObject.SetActive(false);
                    currentKey = null;
                    placedKey = null;
                }

                if (activateEnemy != null && isCompleted)
                {
                    activateEnemy.enabled = true;
                }

                if (activatePlatform != null && isCompleted)
                {
                    activatePlatform.enabled = true;
                    activatePlatform.isToBeActivated = false;

                }

            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !isTwoPart)
        {
            currentKey = other.GetComponent<PlayerEffects>().followObject;

            if (currentKey != null)
            {
                if (currentKey == requiredKey)
                {
                    placedKey = requiredKey;
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
                    placedKey = requiredKey;
                    numberOfKeys++;
                
                    if (numberOfKeys + nextActivation.numberOfKeys == maxKeys)
                    {
                        isCompleted = true;
                    }

                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTwoPart)
        {
            currentKey = other.GetComponent<PlayerEffects>().followObject;

            if (currentKey != null)
            {
                if (currentKey == requiredKey)
                {
                    other.GetComponent<PlayerEffects>().DropSoul();

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
                    other.GetComponent<PlayerEffects>().DropSoul();

                    if (numberOfKeys + nextActivation.numberOfKeys == maxKeys)
                    {
                        other.GetComponent<PlayerEffects>().DropSoul();
                    }

                }
            }
        }
    }
}
