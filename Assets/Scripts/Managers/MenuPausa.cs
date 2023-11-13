using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject MPausa;
    public bool Pausa = false;
    
    void Start()
    {
        // Desactivar el menú de pausa al inicio
        MPausa.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Pausa == false)
        {
            Pausa = true;
            activated();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Pausa == true)
        {
            Pausa = false;
            Desactive();
            Desactive();
        }
    }

    public void Desactive()
    {
        if (Pausa == false)
        {
            MPausa.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            
            Debug.Log("JUEGO no PAUSADO");
        }
    }

    public void activated()
    {
        if (Pausa == true)
        {
            MPausa.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            Debug.Log("JUEGO PAUSADO");
        }
            
    }
    
  
    
}
