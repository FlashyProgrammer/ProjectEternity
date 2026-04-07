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

                if (Vector2.Distance(currentKey.position,transform.position) < 0.01f)
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
                    other.GetComponent<PlayerEffects>().isDropped = true;
                    other.GetComponent<PlayerEffects>().followObject = null;
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
                    other.GetComponent<PlayerEffects>().isDropped = true;
                    other.GetComponent<PlayerEffects>().followObject = null;

                    numberOfKeys++;

                    if (numberOfKeys + nextActivation.numberOfKeys == maxKeys)
                    {
                        isCompleted = true;
                    }

                }
            }
        }
    }
}
