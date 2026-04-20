using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability Parameters")]
    [SerializeField] private DialogueManager talkDialogue;
    [SerializeField] private float abilityTime;
    [SerializeField] private float freezeCooldown;
    [SerializeField] private AudioManager audioManager;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Post-Processing")]
    [SerializeField] private Volume sceneVolume; 
    [SerializeField] private VolumeProfile normalVolume;
    [SerializeField] private VolumeProfile freezeVolume;
    [SerializeField] private VolumeProfile sightVolume;

    [HideInInspector] public bool abilityInUse = false;

    private int abilityInt;
    private float timeCounter;
    private bool startTimer = true;
    private bool canSight = true;
    private int sightCounter = 0;

    private void Update()
    {
        if (startTimer)
        {
            timeCounter -= Time.deltaTime;
        }


        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyUp(KeyCode.RightControl))
        {
            switch (abilityInt)
            {
                case 0:
                    break;
                case 1:
                    Freeze();
                    break;
                case 2:
                    Sight();
                    break;

            }
        }
    }

    public void Freeze()
    {
        abilityInt = 1;
        audioManager.SetClipAbility(audioManager.timeStop);
        audioManager.audioSourceAbilities.Play();
        if (!abilityInUse && gameObject.activeInHierarchy && timeCounter < 0f)
        {
            StartCoroutine(FreezeAbility());   
        }

    }

    public void Sight()
    {
        abilityInt = 2;
        if (canSight && gameObject.activeInHierarchy)
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

        startTimer = false;
        timeCounter = freezeCooldown;
        canSight = false;
        sceneVolume.profile = freezeVolume;
        animator.SetTrigger("freeze");

        //
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        Rigidbody2D[] allBodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        EnemyAi[] enemyObjects = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None);
        Platforms[] gamePlatforms = FindObjectsByType<Platforms>(FindObjectsSortMode.None);
        //

        foreach (GameObject projectile in projectiles)
        {
            if (projectile != null)
            {
                projectile.GetComponent<Rigidbody2D>().Sleep();
            }
        }

        foreach (Rigidbody2D rb in allBodies)
        {
            if (rb != null)
            {
                if (rb.gameObject.tag == "Player")
                {

                }

                else
                {
                    rb.Sleep();
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

        yield return new WaitForSeconds(abilityTime);

        EndFreeze();


    }
    private void StartSight()
    {
        abilityInUse = true;
        sceneVolume.profile = sightVolume;

        //
        EnemyAi[] enemyObjects = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None);
        Platforms[] gamePlatforms = FindObjectsByType<Platforms>(FindObjectsSortMode.None);
        //

        foreach (EnemyAi enemy in enemyObjects)
        {
            if (enemy.objHide)
            {
                if (!enemy.toBeActivated)
                {
                    enemy.enabled = true;
                }

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
                if (!platform.isToBeActivated)
                {
                    platform.enabled = true;

                }

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
                if (!enemy.toBeActivated)
                {
                    enemy.enabled = true;
                }

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
                if (!platform.isToBeActivated)
                {
                    platform.enabled = true;

                }

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

    private void EndFreeze()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        Rigidbody2D[] allBodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        EnemyAi[] enemyObjects = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None);
        Platforms[] gamePlatforms = FindObjectsByType<Platforms>(FindObjectsSortMode.None);

        startTimer = true;
        canSight = true;
        animator.SetTrigger("unfreeze");
        sceneVolume.profile = normalVolume;

        foreach (GameObject projectile in projectiles)
        {
            if (projectile != null)
            {
                projectile.GetComponent<Rigidbody2D>().WakeUp();
            }
        }

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
                        rb.IsAwake();
                    }


                }

            }

        }

        foreach (EnemyAi enemy in enemyObjects)
        {
            if (!enemy.objHide)
            {
                if (!enemy.toBeActivated)
                {
                    enemy.enabled = true;
                }
            }

            else
            {
                enemy.enabled = false;
                enemy.GetComponent<Rigidbody2D>().Sleep();
            }
        }

        foreach (Platforms platform in gamePlatforms)
        {
            if (!platform.isToBeActivated)
            {
                platform.enabled = true;
            }
        }
    }

  
    public void ResetAbility()
    {
        abilityInt = 0;
        sightCounter = 1;

    }

    public void DisableAbilities()
    {
        EndSight();
        EndFreeze();
    }
}
