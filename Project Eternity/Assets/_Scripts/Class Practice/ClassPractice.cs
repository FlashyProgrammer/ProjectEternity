using System.Collections;
using UnityEngine;

public class ClassPractice : MonoBehaviour
{
    [SerializeField] private float rayDist;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform origin;

    [SerializeField] private float rotateTime;

    private void Update()
    {
        RaycastHit2D isGrounded = Physics2D.Raycast(origin.position, Vector2.down, rayDist, groundLayer);
        Debug.DrawRay(origin.position, Vector2.down * rayDist, Color.blue);

        if (isGrounded.collider != null)
        {

            transform.Rotate(0f,180f,0f);

        }
       
    }

    IEnumerator RotateTimer()
    {
        transform.Rotate(0f, 180f, 0f);
        yield return new WaitForSeconds(1f);
        transform.Rotate(0f, 180f, 0f);
    }
}
