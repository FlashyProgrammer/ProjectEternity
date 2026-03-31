using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Platforms : MonoBehaviour
{
    [SerializeField] private bool isFragile;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isStatic;

    [Header("Move Parameters")]
    [SerializeField] private float platformSpeed;
    [SerializeField] private Transform[] movePoints;

    [Header("Dissolve Parameters")]
    [SerializeField] private float disappearTime;
    [SerializeField] private float reappearTime;

 
    private Transform currentPoint;
    private int index = 0;
    private SpriteRenderer platformRenderer;
    private Collider2D platformCollider;

    private void Awake()
    {
      

        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();

        if (platformRenderer == null)
        {
            platformRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (platformCollider == null)
        {
            platformCollider = GetComponentInChildren<Collider2D>();
        }

        if (isMoving)
        {
            currentPoint = movePoints[index];

        }
    }
    private void Update()
    {
        if (isMoving)
        {
            currentPoint = movePoints[index];

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.01f)
            {
                index++;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, platformSpeed * Time.deltaTime);

            }

            if (index == movePoints.Length)
            {
                index = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isFragile)
        {
            StartCoroutine(platformDissolve());
        }

        if (collision.gameObject.CompareTag("Key") && isFragile)
        {
            StartCoroutine(platformDissolve());
        }

        if (collision.gameObject.CompareTag("Player") && isMoving)
        {
            
            collision.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isMoving)
        {
            collision.transform.parent = null;
        }
    }

    private IEnumerator platformDissolve() 
    {
        yield return new WaitForSeconds(disappearTime);

        platformRenderer.enabled = false;
        platformCollider.enabled = false;

        yield return new WaitForSeconds(reappearTime);
        platformRenderer.enabled = true;
        platformCollider.enabled = true;
    }

   
}

