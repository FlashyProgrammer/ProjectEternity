using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability Parameters")]
    [SerializeField] private float abilityTime;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Post-Processing")]
    [SerializeField] private Volume sceneVolume; 
    [SerializeField] private VolumeProfile normalVolume;
    [SerializeField] private VolumeProfile freezeVolume;
    [SerializeField] private VolumeProfile sightVolume;


    private bool abilityInUse = false;
    private bool canSight = true;
    private int sightCounter = 0;

    public void Freeze()
    {
        if (!abilityInUse)
        {
            StartCoroutine(FreezeAbility());   
        }
    }

    public void Sight()
    {
        if (canSight)
        {
            switch (sightCounter)
            {
                case 0:
                    animator.SetTrigger("freeze");
                    StartSight();
                    sightCounter++;
                    abilityInUse = true;
                    break;
                case 1:
                    animator.SetTrigger("unfreeze");
                    abilityInUse = false;
                    EndSight();
                    break;
            }
        }
            
    }

    public IEnumerator FreezeAbility()
    {
        canSight = false;
        sceneVolume.profile = freezeVolume;
        animator.SetTrigger("freeze");
        Rigidbody2D[] allBodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        EnemyAi[] enemyObjects = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None);
        Platforms[] gamePlatforms = FindObjectsByType<Platforms>(FindObjectsSortMode.None);


        foreach (Rigidbody2D rb in allBodies)
        {
            if (rb != null)
            {
                if (rb.gameObject.tag == "Player")
                {

                }

                else
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;
                    rb.gravityScale = 0f;
                }
            }
        }

        foreach (EnemyAi enemy in enemyObjects)
        {
            enemy.enabled = false;
        }

        foreach (Platforms platform in gamePlatforms)
        {
            platform.enabled = false;
        }

        yield return new WaitForSecondsRealtime(abilityTime);

        canSight = true;
        animator.SetTrigger("unfreeze");
        sceneVolume.profile = normalVolume;

        foreach (Rigidbody2D rb in allBodies)
        {
            if (rb != null)
            {
                if (rb.gameObject.tag == "Player")
                {
                }
                else
                {
                    if (!rb.gameObject.GetComponent<Collider2D>().isTrigger)
                    {
                        rb.constraints = RigidbodyConstraints2D.None;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        rb.gravityScale = 1f;
                    }

                   
                }
              
            }

        }

        foreach (EnemyAi enemy in enemyObjects)
        {
            if (!enemy.objHide)
            {
                enemy.enabled = true;
            }

            else
            {
                enemy.enabled = false;
                enemy.GetComponent<Rigidbody2D>().Sleep();
            }
        }

        foreach (Platforms platform in gamePlatforms)
        {
            platform.enabled = true;
        }

    }
    private void StartSight()
    {
        abilityInUse = true;
        sceneVolume.profile = sightVolume;
        EnemyAi[] enemyObjects = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None);
        Platforms[] gamePlatforms = FindObjectsByType<Platforms>(FindObjectsSortMode.None);

        foreach (EnemyAi enemy in enemyObjects)
        {
            if (enemy.objHide)
            {
                enemy.enabled = true;

                if (enemy.gameObject.GetComponent<Collider2D>() != null)
                {
                    enemy.GetComponent<Collider2D>().enabled = true;
                }

                if (enemy.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    enemy.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                }

                if (enemy.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    enemy.gameObject.GetComponent<Rigidbody2D>().WakeUp();
                }
            }

            if (enemy.objShow)
            {
                enemy.enabled = false;

                if (enemy.gameObject.GetComponent<Collider2D>() != null)
                {
                    enemy.GetComponent<Collider2D>().enabled = false;
                }

                if (enemy.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    var enemyColour = enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color;
                    enemyColour.a = 0.1f;
                    enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = enemyColour;
                }

                if (enemy.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                  
                    enemy.gameObject.GetComponent<Rigidbody2D>().Sleep();
                }
            }
        }

        foreach (Platforms platform in gamePlatforms)
        {
            if (platform.objHide)
            {
                platform.enabled = true;

                if (platform.gameObject.GetComponent<Collider2D>() != null)
                {
                    platform.GetComponent<Collider2D>().enabled = true;
                }

                if (platform.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    platform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                }

            }

            if (platform.objShow)
            {
                platform.enabled = false;

                if (platform.gameObject.GetComponent<Collider2D>() != null)
                {
                    platform.GetComponent<Collider2D>().enabled = false;
                }

                if (platform.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    var platformColour = platform.gameObject.GetComponentInChildren<SpriteRenderer>().color;
                    platformColour.a = 0.1f;
                    platform.gameObject.GetComponentInChildren<SpriteRenderer>().color = platformColour;


                }
            }
        }
    }

    private void EndSight()
    {
        sceneVolume.profile = normalVolume;
        abilityInUse = false;
        EnemyAi[] enemyObjects = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None);
        Platforms[] gamePlatforms = FindObjectsByType<Platforms>(FindObjectsSortMode.None);

        foreach (EnemyAi enemy in enemyObjects)
        {
            if (enemy.objHide)
            {
                enemy.enabled = false;

                if (enemy.gameObject.GetComponent<Collider2D>() != null)
                {
                    enemy.GetComponent<Collider2D>().enabled = false;
                }

                if (enemy.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    enemy.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }

                if (enemy.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    enemy.gameObject.GetComponent<Rigidbody2D>().Sleep();
                }
            }

            if (enemy.objShow)
            {
                enemy.enabled = true;

                if (enemy.gameObject.GetComponent<Collider2D>() != null)
                {
                    enemy.GetComponent<Collider2D>().enabled = true;
                }

                if (enemy.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    var enemyColour = enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color;
                    enemyColour.a = 1f;
                    enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = enemyColour;
                }
            }
        }

        foreach (Platforms platform in gamePlatforms)
        {
            if (platform.objHide)
            {
                platform.enabled = false;

                if (platform.gameObject.GetComponent<Collider2D>() != null)
                {
                    platform.GetComponent<Collider2D>().enabled = false;
                }

                if (platform.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    platform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }

            if (platform.objShow)
            {
                platform.enabled = true;

                if (platform.gameObject.GetComponent<Collider2D>() != null)
                {
                    platform.GetComponent<Collider2D>().enabled = true;
                }

                if (platform.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    var platformColour = platform.gameObject.GetComponentInChildren<SpriteRenderer>().color;
                    platformColour.a = 1f;
                    platform.gameObject.GetComponentInChildren<SpriteRenderer>().color = platformColour;
                }
            }
        }

        sightCounter = 0;
    }
}
