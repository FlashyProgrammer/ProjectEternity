using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerEffects : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float airForce;
    [SerializeField] private float followSpeed;
    [SerializeField] private float massChange;
    [SerializeField] private float dampChange;
    [SerializeField] private float angularDampChange;
    [SerializeField] private float bounceForce;

    [Header("Health Parameters")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float healthPackAmount;
    [SerializeField] private float soulDamage;
    [SerializeField] private float physicalDamage;
    [SerializeField] private float gradualDamage;
    [SerializeField] private float gradualDecreaseTime;

    [Header("Misc Parameters")]
    [SerializeField] private float controllerDisableTime;
    [SerializeField] private Transform soulArea;

    [Header("UI")]
    [SerializeField] private UiManager uiManager;


    public Transform followObject;
    

    [HideInInspector] public bool isDropped;


    private bool startCounter = true;
    private bool isInitialized = false;
    private float timeCounter;
    private PlayerController controller;
    private float originalDamping;
    private float originalAngularDaming;
    private float originalMass;
    public Transform currentCheckPoint;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isDropped = true;
        timeCounter = gradualDecreaseTime;
        originalDamping = rb.linearDamping;
        originalAngularDaming = rb.angularDamping;
        originalMass = rb.mass;
        
    }

    private void FixedUpdate()
    {
        if (!isDropped && followObject != null)
        {
            followObject.transform.position = Vector2.MoveTowards(followObject.gameObject.transform.position, 
                soulArea.position, followSpeed * Time.fixedDeltaTime);
        }
        if (startCounter && isInitialized)
        {
            timeCounter -= Time.fixedDeltaTime;
        }

        if (timeCounter < 0f)
        {
            playerHealth.TakeDamage(gradualDamage);
            timeCounter = gradualDecreaseTime;
        }

        if (followObject == null)
        {
            startCounter = true;
        }

        else if (followObject.gameObject.name == "Soul")
        {
            startCounter = false;
        }

        else
        {
           startCounter = true;
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check environment hazard (Spikes)
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Disable();
            if (followObject != null)
            {
                if (followObject.gameObject.name == "Soul")
                {
                    playerHealth.TakeDamage(soulDamage);
                }
            }

            playerHealth.TakeDamage(physicalDamage);

         
        }

        // Soul Collisions
        if (collision.gameObject.CompareTag("Key") && isDropped)
        {
          
            followObject = collision.transform;    
            followObject.parent = soulArea;
            isInitialized = true;

            if (transform.localScale.x < 0f)
            {
                followObject.localScale = new Vector3(-followObject.localScale.x, followObject.localScale.y, followObject.localScale.z);

            }

            if (transform.localScale.x > 0f)
            {
                followObject.localScale = new Vector3(Mathf.Abs(followObject.localScale.x), followObject.localScale.y, followObject.localScale.z);
            }

            followObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            followObject.gameObject.GetComponent<Collider2D>().isTrigger = true;
            isDropped = false;
        }

        if (collision.gameObject.CompareTag("Bounce")) 
        {
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Disable();
            if (followObject != null)
            {
                if (followObject.gameObject.name == "Soul")
                {
                    playerHealth.TakeDamage(soulDamage);
                }
            }
            Destroy(collision.gameObject, 0.1f);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks for checkpoint collisions
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckPoint = collision.transform;
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {
            Disable();
            if (followObject != null)
            {
                if (followObject.gameObject.name == "Soul")
                {
                    playerHealth.TakeDamage(soulDamage);
                }
            }

        }

        if (collision.gameObject.CompareTag("Key") && isDropped)
        {
            isInitialized = true;
            followObject = collision.transform;
            followObject.parent = soulArea;
            followObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            followObject.gameObject.GetComponent<Collider2D>().isTrigger = true;
            isDropped = false;
        }

        if (collision.gameObject.CompareTag("Extraction"))
        {
            if (followObject != null && followObject.name == "Soul")
            {
                uiManager.WinScreen();
            }
        }

        if (collision.gameObject.CompareTag("Health") && playerHealth.soulHealth < playerHealth.maxHealth)
        {
            playerHealth.GainHealth(healthPackAmount);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Jump Pad Collisions
        if (collision.CompareTag("Jump Pad"))
        {
            Vector2 amount = new Vector2(0f, airForce);
            rb.AddForce(amount, ForceMode2D.Force);
            rb.gravityScale = 1f;
        }
        // Check environment hazard (Slippery)
        if (collision.CompareTag("Slippery"))
        {
            rb.linearDamping = dampChange;
            rb.angularDamping = angularDampChange;
            rb.mass = massChange;
        }
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckPoint = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Slippery"))
        {
            rb.linearDamping = originalDamping;
            rb.angularDamping = originalDamping;
            rb.mass = originalMass;

        }
    }

    public void Disable()
    {
        transform.position = currentCheckPoint.position;
        gameObject.SetActive(false);
        controller.enabled = false;

    }
 
    public void DropSoul()
    {
        if (followObject != null && isDropped == false)
        {
            followObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            followObject.parent = null;
            followObject.transform.position = soulArea.position;
            followObject.gameObject.GetComponent<Collider2D>().isTrigger = false;
            followObject = null;
            isDropped = true;
        }
    }
}
