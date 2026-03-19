using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float airForce;
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform soulArea;
    private Transform characterSoul;
    private bool isDropped;

    private float originalDamping;
    private float originalAngularDaming;
    private float originalMass;
    private Transform currentCheckPoint;
    private void Awake()
    {
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
        if (!isDropped && characterSoul != null)
        {
            characterSoul.transform.position = Vector2.Lerp(characterSoul.gameObject.transform.position, 
                soulArea.position, followSpeed * Time.fixedDeltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check environment hazard (Spikes)
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Player hit hazard");
            transform.position = currentCheckPoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks for checkpoint collisions
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckPoint = collision.transform;
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
        // Soul Collisions
        if (collision.CompareTag("Soul") && isDropped)
        {
            characterSoul = collision.transform;
            isDropped = false;
        }
        // Check environment hazard (Slippery)
        if (collision.CompareTag("Slippery"))
        {
            rb.linearDamping = 0f;
            rb.angularDamping = 0f;
            rb.mass = 0.5f;
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

    public void DropSoul()
    {
        Debug.Log("Input detected");
        if (characterSoul != null && isDropped == false)
        {
            characterSoul.transform.position = soulArea.position;
            isDropped = true;
        }
    }
}
