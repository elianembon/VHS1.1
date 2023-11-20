using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Quicksort.Sort(Bag, 0, Bag.Count - 1);
        // Implementa la lógica para actualizar la interfaz de usuario del inventario si es necesario
        UpdateInventoryUI();
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
            (coll.CompareTag("Medic") && CountItemsWithTag("Medic") < maxMedicItems))
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
                // Acción de curación aquí
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



