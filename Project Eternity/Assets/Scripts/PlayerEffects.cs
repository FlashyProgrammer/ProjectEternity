using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float airForce;
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform soulArea;

    private Transform characterSoul;
    private bool isDropped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isDropped = true;
    }

    private void FixedUpdate()
    {
        if (!isDropped && characterSoul != null)
        {
            Debug.Log("Is following character");
            characterSoul.transform.position = Vector2.Lerp(characterSoul.gameObject.transform.position, soulArea.position, followSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Jump Pad"))
        {
            Vector2 amount = new Vector2(0f, airForce);
            rb.AddForce(amount, ForceMode2D.Force);
            rb.gravityScale = 1f;
        }

        if (collision.CompareTag("Soul") && isDropped)
        {
            characterSoul = collision.transform;
            isDropped = false;
        }
    }
}
