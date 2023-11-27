using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class Inventory : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inventory;
    public GameObject Selector;
    public int ID;
    private PlayerManager player;

    private bool isGamePaused;
    private bool activeInventory;

    private int maxMedicItems = 3;
    private int maxOtherItems = 3;

    public Image fase1;
    public Image fase2;
    public Image fase3;
    public Image fase4;

    private AudioSource audioSource;
    public AudioClip remove;
    public AudioClip collectMedic;
    public AudioClip useMedic;
    public AudioClip collectItem;
    public AudioClip collectObject;

    private List<Vector2> originalPositions = new List<Vector2>();

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isGamePaused = false;
        activeInventory = false;
        RecordOriginalPositions();

        player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        Nav();

        if (Input.GetKeyDown(KeyCode.T))
        {
            TransferItemToThermal();
        }

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

        if (player.CurrentLife <= 120)
        {
            fase1.gameObject.SetActive(true);
            fase2.gameObject.SetActive(false);
            fase3.gameObject.SetActive(false);
            fase4.gameObject.SetActive(false);
        }
        if (player.CurrentLife <= 90)
        {
            fase1.gameObject.SetActive(false);
            fase2.gameObject.SetActive(true);
            fase3.gameObject.SetActive(false);
            fase4.gameObject.SetActive(false);
        }
        if (player.CurrentLife <= 60)
        {
            fase1.gameObject.SetActive(false);
            fase2.gameObject.SetActive(false);
            fase3.gameObject.SetActive(true);
            fase4.gameObject.SetActive(false);
        }
        if (player.CurrentLife <= 30)
        {
            fase1.gameObject.SetActive(false);
            fase2.gameObject.SetActive(false);
            fase3.gameObject.SetActive(false);
            fase4.gameObject.SetActive(true);
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

        // Ordenar la lista de objetos activos usando Quicksort genérico
        Quicksort<GameObject>.Sort(activeItems, itemComparison, 0, activeItems.Count - 1);

        // Limpiar la bolsa original
        Bag.Clear();

        // Agregar los elementos ordenados en la bolsa original, manteniendo los elementos existentes
        Bag.AddRange(activeItems);

        // Implementar la lógica para actualizar la interfaz de usuario del inventario si es necesario
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
                return int.MaxValue; 
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

    void TransferItemToThermal()
    {
        if (ID >= 0 && ID < Bag.Count)
        {
            GameObject selectedSlot = Bag[ID];
            Image selectedImage = selectedSlot.GetComponent<Image>();

            Thermal thermalScript = FindObjectOfType<Thermal>(); // Buscar el script Thermal en la escena

            if (thermalScript != null && selectedImage.enabled && selectedSlot.CompareTag("Item") && thermalScript.IsNearGenerator())
            {
                thermalScript.ReceiveItemFromInventory(selectedSlot); // Llama al método en Thermal para transferir el objeto
                RemoveItemFromInventory(); // Remueve el objeto del inventario
            }
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

        if (coll.CompareTag("Medic"))
        {
            audioSource.PlayOneShot(collectMedic);
        }

        if (coll.CompareTag("Item"))
        {
            audioSource.PlayOneShot(collectItem);
        }

        if (coll.CompareTag("Objects"))
        {
            audioSource.PlayOneShot(collectObject);
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
            audioSource.PlayOneShot(useMedic);
            GameObject selectedSlot = Bag[ID];
            Image selectedImage = selectedSlot.GetComponent<Image>();

            if (selectedImage.enabled && selectedSlot.CompareTag("Medic"))
            {
                Debug.Log("Te estoy curando");
                player.GetLife();
                RemoveItemFromInventory();
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
            audioSource.PlayOneShot(remove);
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