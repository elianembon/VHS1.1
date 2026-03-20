using UnityEngine;

public class InitGame : MonoBehaviour
{
    void Start()
    {
        // Mostrar el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Tiempo normal
        Time.timeScale = 1f;
    }
}