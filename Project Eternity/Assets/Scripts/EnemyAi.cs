using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("Enemy Properties")]
    [SerializeField] private bool isTypeOne;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float circleDetectionRadius;
    [SerializeField] private Transform spawnArea;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject spawnObject;

    private float spawnTimer;
    private bool isInSight;
    private void FixedUpdate()
    {
        isInSight = Physics2D.OverlapCircle(transform.position, circleDetectionRadius, playerLayer);

        if (isInSight)
        {
            spawnTimer -= Time.fixedDeltaTime;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        spawnTimer = spawnRate;
        if (spawnTimer > 0f)
        {
            Instantiate(spawnObject, spawnArea.position, Quaternion.identity);

            if (isTypeOne)
            {
                Destroy(this.gameObject, 2f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, circleDetectionRadius);
    }

}
