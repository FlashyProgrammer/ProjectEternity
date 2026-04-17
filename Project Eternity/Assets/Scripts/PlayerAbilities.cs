using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private float abilityTime;
    public void Freeze()
    {
        StartCoroutine(FreezeAbility());   

    }

    public void Sight()
    {

    }

    public IEnumerator FreezeAbility()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(abilityTime);
        Time.timeScale = 1f;    
        Debug.Log("Unfreezed");
    }
}
