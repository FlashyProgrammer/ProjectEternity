using UnityEngine;

public class MusicCall : MonoBehaviour
{

    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMenuMusic();
    }
}


