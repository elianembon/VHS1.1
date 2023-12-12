using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject MPausa;
    public UIManager UIManager;
    public bool Pausa = false;
    
    void Start()
    {
        // Desactivar el menú de pausa al inicio
        MPausa.SetActive(false);
        UIManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !Pausa)
        {
            Activated();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Pausa)
        {
            Deactived();
        }
    }

    public void Deactived()
    {
        Pausa = false;
        MPausa.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.ResumeGame();
            
    }

    public void Activated()
    {
        Pausa = true;
        MPausa.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UIManager.PauseGame();
            
    }
    
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }
  
    
}
