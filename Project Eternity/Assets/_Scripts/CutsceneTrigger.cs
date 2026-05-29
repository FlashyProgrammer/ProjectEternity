using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;

    //[SerializeField] private GameObject playerCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cutscene.Play();
            StartCoroutine(cutsceneDisable());

        }
    }

    IEnumerator cutsceneDisable()
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime((float)cutscene.duration);

        this.gameObject.SetActive(false);
        Time.timeScale = 1f;

    }
}
