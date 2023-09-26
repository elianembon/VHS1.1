using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public PlayerManager getLife;
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inventory;

    public GameObject Selector;
    public int ID;

    private bool isGamePaused;
    private bool activeInventory;

    private void Awake()
    {
        isGamePaused = false;
        activeInventory = false;
    }

    // Update is called once per frame
    void Update()
    {
        Nav();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!activeInventory)
            {
                activeInventory = true;
                inventory.SetActive(true);
                PauseGame();
            }
            else
            {
                activeInventory = false;
                inventory.SetActive(false);
                ResumeGame();
            }
        }

        // Añade un manejo para quitar un objeto del inventario
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveItemFromInventory();
        }

        // Verifica si se presiona 'F' y el objeto seleccionado tiene la etiqueta "Medic"
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ID >= 0 && ID < Bag.Count)
            {
                GameObject selectedSlot = Bag[ID];
                Image selectedImage = selectedSlot.GetComponent<Image>();

                // Verifica si el objeto en el slot tiene la etiqueta "Medic"
                if (selectedImage.enabled && selectedSlot.CompareTag("Medic"))
                {
                    // Realiza la acción de curación
                    Debug.Log("Te estoy curando");
                    getLife.GetLife();
                    RemoveItemFromInventory();
                    // Puedes agregar aquí la lógica para curar al jugador si es necesario
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Item") || coll.CompareTag("Medic"))
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].GetComponent<Image>().enabled == false)
                {
                    Bag[i].GetComponent<Image>().enabled = true;
                    Bag[i].GetComponent<Image>().sprite = coll.GetComponent<SpriteRenderer>().sprite;
                    Bag[i].tag = coll.tag; // Conserva la etiqueta del objeto en el inventario
                    break;
                }
            }
        }
    }


    private void RemoveItemFromInventory()
    {
        if (ID >= 0 && ID < Bag.Count)
        {
            Bag[ID].GetComponent<Image>().enabled = false;
            Bag[ID].GetComponent<Image>().sprite = null;
            Bag[ID].tag = "Untagged";
        }
    }

    // Llama al método de pausa del GameManager
    private void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    // Llama al método de reanudación del GameManager
    private void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }


    public void Nav()
    {
        if (!isGamePaused) // Solo permite la navegación si el juego no está pausado
        {
            if (Input.GetKeyDown(KeyCode.D) && ID < Bag.Count - 1)
            {
                ID++;
            }
            if (Input.GetKeyDown(KeyCode.A) && ID > 0)
            {
                ID--;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (ID > 2)
                {
                    ID -= 3;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (ID < 6)
                {
                    ID += 3;
                }
            }

            Selector.transform.position = Bag[ID].transform.position;
        }
    }

    // Método para pausar el juego debido al inventario
    private void PauseGameDueToInventory()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Establece el tiempo a 0 para pausar el juego
    }

    // Método para reanudar el juego debido al inventario
    private void ResumeGameDueToInventory()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Restaura el tiempo original
    }

    // Método para activar el inventario
    public void ActivateInventory()
    {
        activeInventory = true;
        PauseGameDueToInventory(); // Pausa el juego cuando se abre el inventario
        inventory.SetActive(true);
    }

    // Método para desactivar el inventario
    public void DeactivateInventory()
    {
        activeInventory = false;
        ResumeGameDueToInventory(); // Reanuda el juego cuando se cierra el inventario
        inventory.SetActive(false);
    }
}



