using UnityEngine;

public class ActivationManager : MonoBehaviour
{
    [SerializeField] private EnemyAi activateEnemy;
    [SerializeField] private Platforms activatePlatform;
    [SerializeField] private ActivationManager nextActivation;
    [SerializeField] private Transform requiredKey;
    [SerializeField] private float keyAttachSpeed;
    [SerializeField] private bool isLocked;

    public int numberOfKeys;

    public int acuiredKeys;
    private Transform currentKey;

    private void Awake()
    {
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
                }
                
                if (activateEnemy != null)
                {
                    activateEnemy.enabled = true;
                }

                if (activatePlatform != null)
                {
                    activatePlatform.enabled = true;
                }

                if (nextActivation != null)
                {
                    nextActivation.enabled = true;
                }
            }

        }
        Debug.Log(currentKey);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && numberOfKeys == acuiredKeys)
        {
            currentKey = other.GetComponent<PlayerEffects>().followObject;

            if (currentKey != null)
            {
                if (currentKey == requiredKey)
                {
                    other.GetComponent<PlayerEffects>().isDropped = true;

                    if (nextActivation.enabled == true)
                    {
                        nextActivation.acuiredKeys++;
                        nextActivation.numberOfKeys = nextActivation.acuiredKeys;
                    }  
                }
  
            }

        }
    }
}
