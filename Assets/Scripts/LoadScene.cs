using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    public void Over()
    {
        gameObject.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene("Level");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Game()
    {
        SceneManager.LoadScene("Level");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Win()
    {
        gameObject.SetActive(true );
        player.GetComponent<Player>().GG();
    }
    public void Parametre()
    {
        SceneManager.LoadScene("Parametre");
    }
}
