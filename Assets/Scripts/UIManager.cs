using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager gameManagerInstance;

    private void Awake()
    {
        gameManagerInstance = FindObjectOfType<GameManager>();

        if (gameManagerInstance == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    public void PauseGame()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.PauseGame();
        }
    }

    public void ResumeGame()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.ResumeGame();
        }
    }

    public void NextLevel()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.NextLevel();
        }
    }

    public void ReloadScene()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.ReloadScene();
        }
    }

    public void GoToMenu()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.GoToMenu();
        }
    }

    public void GoComoJugar()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.GoComoJugar();
        }
    }

    public void QuitGame()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.QuitGame();
        }
    }

    public void Victory()
    {
        if (gameManagerInstance != null)
        {
            gameManagerInstance.Victory();
        }
    }
}
