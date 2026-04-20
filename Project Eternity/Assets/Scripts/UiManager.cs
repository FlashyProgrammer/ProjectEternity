using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private int pauseCounter = 0;

    private bool isScreened;


    private void Update()
    {
        if (pauseMenu != null && !isScreened)
        {
            if (Input.GetKeyUp(KeyCode.Escape) && pauseCounter == 0)
            {
                pauseCounter++;
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && pauseCounter == 1)
            {
                pauseCounter++;
            }

            if (Input.GetKeyUp(KeyCode.Escape) && pauseCounter == 2)
            {
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                pauseCounter = 0;
                Time.timeScale = 1f;
            }
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Interaction Window", LoadSceneMode.Additive);
        SceneManager.LoadScene("Prototype", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Time.timeScale = 1f;
        isScreened = false;

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Interaction Window", LoadSceneMode.Additive);
        SceneManager.LoadScene("Prototype", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        isScreened = false;

    }

    public void WinScreen()
    {
        SceneManager.LoadScene("Win Screen", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        SceneManager.UnloadSceneAsync("Interaction Window");
        SceneManager.UnloadSceneAsync("Prototype");
        isScreened = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeathScreen()
    {
        SceneManager.LoadScene("Death Screen", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Interaction Window");
        SceneManager.UnloadSceneAsync("Prototype");
        Time.timeScale = 0f;
        isScreened = true;
        Cursor.lockState = CursorLockMode.None;
    }
}


    
