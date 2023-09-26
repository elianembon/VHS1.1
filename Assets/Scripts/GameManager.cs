using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGamePaused = false;

    private void Awake()
    {
        // Aseg�rate de que solo haya una instancia de GameManager en la escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Resto del c�digo del GameManager...

    // M�todo para pausar el juego
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Pausa el tiempo
        // Aqu� puedes deshabilitar los controles de movimiento del jugador si es necesario
    }

    // M�todo para reanudar el juego
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Restaura el tiempo
        // Aqu� puedes habilitar nuevamente los controles de movimiento del jugador si es necesario
    }
}
