using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Inventory : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inventory;
    public GameObject Selector;
    public int ID;

    private bool isGamePaused;
    private bool activeInventory;

    private int maxMedicItems = 3;
    private int maxOtherItems = 3;

    private List<Vector2> originalPositions = new List<Vector2>();

    void Awake()
    {
        isGamePaused = false;
        activeInventory = false;
        RecordOriginalPositions();
    }

    void Update()
    {
        Nav();

        if (Input.GetKeyDown(KeyCode.I))
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveItemFromInventory();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UseMedicItem();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SortInventory();
        }
    }

    private void SortInventory()
    {
        List<GameObject> activeItems = Bag.FindAll(item => item.activeSelf);

        // Comparador para ordenar por tipo de objeto
        Comparison<GameObject> itemComparison = (item1, item2) =>
        {
            int item1Value = GetSortValue(item1.tag);
            int item2Value = GetSortValue(item2.tag);
            return item1Value.CompareTo(item2Value);
        };

        // Ordenar la lista de objetos activos usando Quicksort gen�rico
        Quicksort<GameObject>.Sort(activeItems, itemComparison, 0, activeItems.Count - 1);

        // Limpiar la bolsa original
        Bag.Clear();

        // Agregar los elementos ordenados en la bolsa original, manteniendo los elementos existentes
        Bag.AddRange(activeItems);

        // Implementar la l�gica para actualizar la interfaz de usuario del inventario si es necesario
        UpdateInventoryUI();
    }

    // Obtener el valor de ordenamiento para un tipo de objeto
    private int GetSortValue(string tag)
    {
        switch (tag)
        {
            case "Item":
                return 0;
            case "Medic":
                return 1;
            case "Objects":
                return 2;
            default:
                return int.MaxValue; // Valor predeterminado para manejar casos desconocidos
        }
    }


    private void UpdateInventoryUI()
    {
        for (int i = 0; i < Bag.Count; i++)
        {
            Bag[i].GetComponent<RectTransform>().anchoredPosition = originalPositions[i];
        }
    }

    private void RecordOriginalPositions()
    {
        originalPositions.Clear();
        foreach (var item in Bag)
        {
            originalPositions.Add(item.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.CompareTag("Item") && CountItemsWithTag("Item") < maxOtherItems) ||
            (coll.CompareTag("Medic") && CountItemsWithTag("Medic") < maxMedicItems) || 
            (coll.CompareTag("Objects") && CountItemsWithTag("Objects") < maxOtherItems))
        {
            AddItemToInventory(coll);
        }
    }

    void AddItemToInventory(Collider2D coll)
    {
        for (int i = 0; i < Bag.Count; i++)
        {
            if (!Bag[i].GetComponent<Image>().enabled)
            {
                Bag[i].GetComponent<Image>().enabled = true;
                Bag[i].GetComponent<Image>().sprite = coll.GetComponent<SpriteRenderer>().sprite;
                Bag[i].tag = coll.tag;
                break;
            }
        }
    }

    int CountItemsWithTag(string tag)
    {
        return Bag.FindAll(item => item.tag == tag).Count;
    }

    void UseMedicItem()
    {
        if (ID >= 0 && ID < Bag.Count)
        {
            GameObject selectedSlot = Bag[ID];
            Image selectedImage = selectedSlot.GetComponent<Image>();

            if (selectedImage.enabled && selectedSlot.CompareTag("Medic"))
            {
                Debug.Log("Te estoy curando");
                // Acci�n de curaci�n aqu�
            }
        }
    }

    void ToggleInventory()
    {
        activeInventory = !activeInventory;
        inventory.SetActive(activeInventory);

        if (activeInventory)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void RemoveItemFromInventory()
    {
        if (ID >= 0 && ID < Bag.Count)
        {
            Bag[ID].GetComponent<Image>().enabled = false;
            Bag[ID].GetComponent<Image>().sprite = null;
            Bag[ID].tag = "Untagged";
        }
    }

    void Nav()
    {
        if (!isGamePaused)
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

    void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}



