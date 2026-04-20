using JetBrains.Annotations;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class WinOrLoseMenuScript : MonoBehaviour, IWinOrLoseMenuScript
{ 
 // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }
      public void BacktoMain()
    {
        SceneManager.LoadSceneAsync(2);

    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
   

