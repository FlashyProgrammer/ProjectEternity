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

    [Header("Enemy Type")]
    [SerializeField] private bool isPatrol; 
    [SerializeField] private bool isOneTime;
    [SerializeField] private bool isProjectile;

    [Header("Enemy Parameters")]
    [SerializeField] private float spawnRate;
    [SerializeField] private float despawnTime;
    [SerializeField] private float objectDespawnTime;
    [SerializeField] private GameObject spawnObject;

    [Header("Projectile Parameters")]
    [SerializeField] private float minForceAngle;
    [SerializeField] private float maxForceAngle;
    [SerializeField] private float projectileForce;
    [SerializeField] private float forceAngle;

    [Header("Patrol Parameters")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float waitTime;

    private int index = 0;
    private Vector3 currentPosition;
    private Transform currentPoint;
    private bool hasSpawned = false;
    private float waitCounter;
    private bool inCircleSight;
    private bool isLinetRight;
    private bool isLineLeft;


    private void Awake()
    {
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
        hasSpawned = true;

        if (isOneTime)
        {
            Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
            Destroy(this.gameObject, despawnTime);
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
                var projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;

                Destroy(projectile, objectDespawnTime);

            }

            if (isLineLeft)
            {
                float radianAngle = forceAngle * Mathf.Deg2Rad;
                Vector2 directionAngle = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                var projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;
                projectileRb.linearVelocityX = -projectileRb.linearVelocityX; 

                Destroy(projectile, objectDespawnTime);
            }

            if (inCircleSight)
            {
                float radianAngle = forceAngle * Mathf.Deg2Rad;
                Vector2 directionAngle = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                var projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
                var projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.linearVelocity = directionAngle * projectileForce;
                projectileRb.linearVelocityX = UnityEngine.Random.Range(-projectileRb.linearVelocityX, projectileRb.linearVelocityX);

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
    }

}
