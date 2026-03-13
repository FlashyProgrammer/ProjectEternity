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
    private bool hasSpawned = false;
    private bool isInSight;

    private void FixedUpdate()
    {
        isInSight = Physics2D.OverlapCircle(transform.position, circleDetectionRadius, playerLayer);

        spawnTimer -= Time.fixedDeltaTime;

        if (isInSight && spawnTimer > 0f)
        {
            Debug.Log(spawnTimer);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        spawnTimer = spawnRate;

        Instantiate(spawnObject, spawnArea.position, Quaternion.identity);
        
        spawnTimer = 0f;

        if (isTypeOne)
        {
            hasSpawned = true;
            Destroy(this.gameObject, 5f);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, circleDetectionRadius);
    }

}
