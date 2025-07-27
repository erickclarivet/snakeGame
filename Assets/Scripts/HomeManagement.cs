using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }  
}
