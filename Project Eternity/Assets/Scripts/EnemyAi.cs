using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("Enemy Properties")]
    [SerializeField] private bool isTypeOne;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float circleDetectionRadius;
    [SerializeField] private Transform spawnArea;
    [SerializeField] private float spawnRate;
    [SerializeField] private float despawnTime;
    [SerializeField] private GameObject spawnObject;

    private bool hasSpawned = false;
    private bool isInSight;

    private void FixedUpdate()
    {
        isInSight = Physics2D.OverlapCircle(transform.position, circleDetectionRadius, playerLayer);

 

        if (isInSight && !hasSpawned)
        {
            StartCoroutine(SpawnTimer());
        }
    }

    private IEnumerator SpawnTimer()
    {
        hasSpawned = true;

        Instantiate(spawnObject, spawnArea.position, Quaternion.identity);

        if (isTypeOne)
        {
            Destroy(this.gameObject, despawnTime);
        }

        yield return new WaitForSeconds(spawnRate);

        hasSpawned = false;

        Instantiate(spawnObject, spawnArea.position, Quaternion.identity);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, circleDetectionRadius);
    }

}
