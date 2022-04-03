using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Unpause()
    {
        FindObjectOfType<GameManager>().Unpause();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
