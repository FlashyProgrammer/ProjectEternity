using System;
using UnityEngine;

public class CameraRoom : MonoBehaviour
{

    [SerializeField] private GameObject roomCamera;
    //[SerializeField] private GameObject playerCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roomCamera.SetActive(true);
            //playerCam.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roomCamera.SetActive(false);
            //playerCam.SetActive(true);
        }
    }
}
