using System;
using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("Enemy Properties")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform spawnArea;
    [SerializeField] private float circleDetectionRadius;
    [SerializeField] private float lineDistance;
    [SerializeField] private bool isLineDetection;
    [SerializeField] private bool isCircleDetection;

    [Header("Grounded Parameters")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius;

    [Header("Enemy Behaviour")]
    [SerializeField] private bool isPatrol; 
    [SerializeField] private bool isOneTime;
    [SerializeField] private bool isProjectile;
    [SerializeField] private bool canCeilingWalk;

    [Header("Enemy Parameters")]
    [SerializeField] private float spawnRate;
    [SerializeField] private float despawnTime;
    [SerializeField] private float objectDespawnTime;
    [SerializeField] private GameObject spawnObject;

    [Header("Projectile Parameters")]
    [SerializeField] private float minForceAngle;
    [SerializeField] private float maxForceAngle;
    [SerializeField] private float projectileForce;

    [Header("Patrol Parameters")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float waitTime;

    private bool isGrounded;
    private float forceAngle;
    private GameObject projectile;
    private Rigidbody2D enemyRb;
    private int index = 0;
    private Vector3 currentPosition;
    private Transform currentPoint;
    private bool hasSpawned = false;
    private float waitCounter;
    private bool inCircleSight;
    private bool isLinetRight;
    private bool isLineLeft;


    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        
        if (isPatrol)
        {
            currentPosition = transform.position;
            currentPoint = patrolPoints[index];
        }

    }
    private void Update()
    {
        EnemyPatrol();
        
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, circleRadius, groundLayer);

        Debug.Log(isGrounded);

        if (!isGrounded)
        {
            transform.parent = null;
        }

        if (isCircleDetection)
        {
            inCircleSight = Physics2D.OverlapCircle(transform.position, circleDetectionRadius, playerLayer);
        }

        if (isLineDetection)
        {
            isLinetRight = Physics2D.Raycast(transform.position, transform.right, lineDistance, playerLayer);
            isLineLeft = Physics2D.Raycast(transform.position, -transform.right, lineDistance, playerLayer);
        }

        if (inCircleSight && !hasSpawned)
        {
            StartCoroutine(SpawnTimer());
        }

        if (isLinetRight && !hasSpawned)
        {
            StartCoroutine(SpawnTimer());
        }

        if (isLineLeft && !hasSpawned)
        {
            StartCoroutine(SpawnTimer());
        }
        Debug.DrawRay(transform.position, transform.right * lineDistance, Color.purple);
        Debug.DrawRay(transform.position, -transform.right * lineDistance, Color.purple);
    }

    private IEnumerator SpawnTimer()
    {
        if (isOneTime)
        {
            Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
            Destroy(this.gameObject, despawnTime);
            hasSpawned = true;
        }

        if (isProjectile)
        {
            if (isLinetRight)
            {
                float radianAngle = forceAngle * Mathf.Deg2Rad;
                Vector2 directionAngle = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;
                Destroy(projectile, objectDespawnTime);
                hasSpawned = true;

            }

            if (isLineLeft)
            {
                float radianAngle = forceAngle * Mathf.Deg2Rad;
                Vector2 directionAngle = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;
                projectileRb.linearVelocityX = -projectileRb.linearVelocityX;
                Destroy(projectile, objectDespawnTime);
                hasSpawned = true;
            }

            if (inCircleSight)
            {
                float radianAngle = forceAngle * Mathf.Deg2Rad;
                Vector2 directionAngle = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;
                projectileRb.linearVelocityX = UnityEngine.Random.Range(-projectileRb.linearVelocityX, projectileRb.linearVelocityX);

                Destroy(projectile, objectDespawnTime);
                hasSpawned = true;
            }
        }

        yield return new WaitForSeconds(spawnRate);

        if (isProjectile)
        {
            var randomForceAngle = UnityEngine.Random.Range(minForceAngle, maxForceAngle);

            forceAngle = randomForceAngle;

            if (isLinetRight)
            {
                float radianAngle = forceAngle * Mathf.Deg2Rad;
                Vector2 directionAngle = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;
                Destroy(projectile, objectDespawnTime);

            }

            if (isLineLeft)
            {
                float radianAngle = forceAngle * Mathf.Deg2Rad;
                Vector2 directionAngle = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;
                projectileRb.linearVelocityX = -projectileRb.linearVelocityX; 

                Destroy(projectile, objectDespawnTime);
            }

        }

        hasSpawned = false;
    }

    private void EnemyPatrol()
    {
        if (isPatrol)
        {
            currentPoint = patrolPoints[index];

            if (MathF.Abs(currentPosition.x - currentPoint.position.x) < 0.01f)
            {
                waitCounter -= Time.deltaTime;
                
                if (waitCounter < 0f)
                {
                    waitCounter = waitTime;
                    index++;
                }
            }

            else
            {
                currentPosition.x = Mathf.MoveTowards(transform.position.x, currentPoint.position.x, patrolSpeed * Time.deltaTime);
                transform.position = currentPosition;
            }

            if (index == patrolPoints.Length)
            {
                index = 0;
            }
        }
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, circleDetectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

}
