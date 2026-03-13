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

    /*
     * private void Start()
     * {
     *  // Activity 2
     *  
     * var string charName;
     * 
     * if (charName == "Mario")
     * 
     * {
     *    Debug.Log("Hello Mario!");
     * }
     * 
     * else
     * {
     *     Debug.Log("Hello Stranger!");
     * }
     * 
     *  // Activity 2
     * var int a = 8;
     * var int b = 12;
     * 
     * if (a > b)
     * {
     *     Debug.Log(" A is larger than B!")
     * }
     * 
     * else
     * {
     *     Debug.Log(" A is not larger than B!")
     * }
     * 
     * }
     * 
     *   // Activity 4
     *   
     * [Range(1, 10)]
     * private int playerScore;
     * 
     * if(playerScore <= 2) 
     * 
     * {
     *    Debug.Log("YOU SUCK AT THIS GAME!");
     * }
     * else if (playerScore >= 3  && playerScore <= 4)
     * 
     * {
     *     Debug.Log("YOU ARE OKAY AT THIS GAME ");
     * }
     * 
     * else if (playerScore >= 5 && playerScore <= 6)
     * 
     * {
     *     Debug.Log("YOU ARE GOOD AT THIS GAME ");
     * }
     * 
     * else if (playerScore >= 7 && playerScore <= 10)
     * 
     * {
     *     Debug.Log("YOU ARE GREAT AT THIS GAME ");
     * }
     * 
     */

}
