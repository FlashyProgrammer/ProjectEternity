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
    [SerializeField] private bool isStatic;
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


    private bool hasSpawned = false;
    private bool isInSight;
    private bool isLinetRight;
    private bool isLineLeft;

    private void FixedUpdate()
    {
        if (isCircleDetection)
        {
            isInSight = Physics2D.OverlapCircle(transform.position, circleDetectionRadius, playerLayer);
        }

        if (isLineDetection)
        {
            isLinetRight = Physics2D.Raycast(transform.position, transform.right, lineDistance, playerLayer);
            isLineLeft = Physics2D.Raycast(transform.position, -transform.right, lineDistance, playerLayer);
        }

        if (isInSight && !hasSpawned)
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

        if (isStatic)
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

        }

        hasSpawned = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, circleDetectionRadius);
    }

}
