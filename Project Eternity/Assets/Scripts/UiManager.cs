using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

 
    public void RestartButtonCheckPoint()
    {
        if (SceneManager.GetSceneByName("Death Screen").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Death Screen");
        }
        Time.timeScale = 1f;
  


    }

    public void RestartButton()
    {
      
        SceneManager.LoadScene("Interaction Window", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Prototype", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Win Screen");


        Time.timeScale = 1f;
      

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
    

    }

    public void WinScreen()
    {
        SceneManager.UnloadSceneAsync("Interaction Window");
        SceneManager.UnloadSceneAsync("Prototype");
        SceneManager.LoadScene("Win Screen", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeathScreen()
    {
        SceneManager.LoadScene("Death Screen", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
}


    
