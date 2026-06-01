using UnityEngine;

public class MusicCall2 : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayGameMusic();

    }
}
