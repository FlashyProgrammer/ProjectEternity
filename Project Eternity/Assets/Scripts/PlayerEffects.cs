using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private PlayerHealth soulHealth;
    [SerializeField] private float generalDamage;
    [SerializeField] private float gradualDamage;
    [SerializeField] private float gradualDecreaseTime;

    [Header("Misc Parameters")]
    [SerializeField] private float controllerDisableTime;
    [SerializeField] private Transform soulArea;



    public Transform followObject;
    

    [HideInInspector] public bool isDropped;


    private bool startCounter = true;
    private float timeCounter;
    private PlayerController controller;
    private float originalDamping;
    private float originalAngularDaming;
    private float originalMass;
    private Transform currentCheckPoint;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isDropped = true;
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
        if (startCounter)
        {
            timeCounter -= Time.fixedDeltaTime;
        }

        if (timeCounter < 0)
        {
            soulHealth.TakeDamage(gradualDamage);
            timeCounter = gradualDecreaseTime;
        }

        if (followObject == null)
        {
            startCounter = true;
        }

        else
        {
            if (followObject.gameObject.name == "Soul")
            {
                startCounter = false;
            }

            else
            {
                startCounter = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check environment hazard (Spikes)
        if (collision.gameObject.CompareTag("Hazard"))
        {
            transform.position = currentCheckPoint.position;
            if (soulArea.gameObject.name == "Soul")
            {
                soulHealth.TakeDamage(generalDamage);
            }

            Disable();
        }

        // Soul Collisions
        if (collision.gameObject.CompareTag("Key") && isDropped)
        {
            followObject = collision.transform;
            followObject.parent = soulArea;
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
                    soulHealth.TakeDamage(generalDamage);
                }
            }
            Destroy(collision.gameObject, 0.1f);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            transform.position = currentCheckPoint.position;

            if (followObject != null)
            {
                if (followObject.gameObject.name == "Soul")
                {
                    soulHealth.TakeDamage(generalDamage);
                }
            }
            Disable();

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
            transform.position = currentCheckPoint.position;

            if (followObject != null)
            {
                if (followObject.gameObject.name == "Soul")
                {
                    soulHealth.TakeDamage(generalDamage);
                }
            }
            Disable();
        }

        if (collision.gameObject.CompareTag("Key") && isDropped)
        {
            followObject = collision.transform;
            followObject.parent = soulArea;
            followObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            followObject.gameObject.GetComponent<Collider2D>().isTrigger = true;
            isDropped = false;
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

    private void Disable()
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
