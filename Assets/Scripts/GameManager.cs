using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGamePaused = false;

    private string currentSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        
       
    }

    // M�todo para pausar el juego
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Pausa el tiempo
       
    }

    // M�todo para reanudar el juego
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Restaura el tiempo
       
    }

    public void NextLevel()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(currentSceneName);

    }

    public void ReloadScene()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            SceneManager.LoadScene(currentSceneName);
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }


    public void GoComoJugar()
    {
        SceneManager.LoadScene("ComoJugar");
    }

    public void level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Victoy()
    {
        SceneManager.LoadScene("Victoria");
    }
   

}

