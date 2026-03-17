using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private float projectileForce;

    private bool hasSpawned = false;
    private bool isInSight;
    private bool isInSightRight;
    private bool isInSightLeft;

    private void FixedUpdate()
    {
        if (isCircleDetection)
        {
            isInSight = Physics2D.OverlapCircle(transform.position, circleDetectionRadius, playerLayer);
        }

        if (isLineDetection)
        {
            isInSightRight = Physics2D.Raycast(transform.position, transform.right, lineDistance, playerLayer);
            isInSightLeft = Physics2D.Raycast(transform.position, -transform.right, lineDistance, playerLayer);
        }

        if (isInSight && !hasSpawned)
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

        if (isProjectile)
        {
            var projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);

            var projectileRb = projectile.GetComponent<Rigidbody2D>();

            projectileRb.AddTorque(projectileForce, ForceMode2D.Impulse);

            Destroy(projectile, objectDespawnTime);
        }

        yield return new WaitForSeconds(spawnRate);

        hasSpawned = false;

        if (isProjectile)
        {
            var projectile = Instantiate(spawnObject, spawnArea.position, Quaternion.identity);

            var projectileRb = projectile.GetComponent<Rigidbody2D>();

            if (isInSightRight)
            {
                projectileRb.AddTorque(-projectileForce, ForceMode2D.Impulse);
                projectileRb.AddForce(- transform.up * projectileForce, ForceMode2D.Impulse);
            }

            if(isInSightLeft)
            {
                projectileRb.AddTorque(projectileForce, ForceMode2D.Impulse);
                projectileRb.AddForce(transform.up * projectileForce, ForceMode2D.Impulse);
            }

            Destroy(projectile, objectDespawnTime);

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, circleDetectionRadius);
    }

}
