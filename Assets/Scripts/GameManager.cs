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
            OnDestroy();
        }

    }

    private void Start()
    {
        GeneratorManager.OnGeneratorCountReachedMax += NextLevel;
        Player.OnPlayerDead += GoToLoseScene;
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Pausa el tiempo
       
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Restaura el tiempo
       
    }

    public void NextLevel()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        currentSceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(currentSceneName);

    }

    public void ReloadScene()
    {
        //currentSceneName = SceneManager.GetActiveScene().name;
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

    public void GoToLoseScene()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Derrota");
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

    private void OnDestroy()
    {
        GeneratorManager.OnGeneratorCountReachedMax -= NextLevel;
    }

}

