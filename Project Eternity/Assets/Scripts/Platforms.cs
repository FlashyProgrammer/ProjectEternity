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
        currentPoint = movePoints[index];
    }
    private void Update()
    {
        if (isStatic)
        {
            return;
        }

        Debug.Log(index);

        if (isMoving)
        {
            if (transform.position != currentPoint.position)
            {
                transform.position = Vector2.Lerp(transform.position, currentPoint.position, platformSpeed);
            }

            if (index == movePoints.Length)
            {
                index = 0;
                currentPoint = movePoints[index];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            if (isMoving)
            {
                index++;
                currentPoint = movePoints[index];
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isFragile)
        {
            StartCoroutine(platformDissolve());
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

