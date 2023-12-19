using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class Inventory : MonoBehaviour
{
    protected InventoryManager inventoryManager;
    public UIManager UIManager;

    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inventory;
    public GameObject Selector;
    public GameObject DescripcionMedic;
    public GameObject DescripcionTermica;
    public int ID;
    public PlayerManager player;
    public int fusibleColocados;

    private bool isGamePaused;
    private bool activeInventory;
    

    //Descripcion de los items del inventario
    private bool onMedicItem = false;
    private bool onTermicItem = false;

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
    public AudioClip openInv;
    public AudioClip closeInv;

    private List<Vector2> originalPositions = new List<Vector2>();
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isGamePaused = false;
        activeInventory = false;
        RecordOriginalPositions();

        player = GetComponent<PlayerManager>();

        
    }

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        UIManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        
        Nav();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!activeInventory)
            {
                audioSource.PlayOneShot(openInv);
                activeInventory = true;
                FasesInventory(player.CurrentLife);
                inventory.SetActive(true);
                UIManager.PauseGame();
                
            }
            else
            {
                audioSource.PlayOneShot(closeInv);
                activeInventory = false;
                inventory.SetActive(false);
                UIManager.ResumeGame();
            }
        }

      /*  if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveItemFromInventory();
        }*/

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseMedicItem();
            TransferItemToThermal();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SortInventory();
        }

        TermicDescription();
    }


    private void FasesInventory(float currentLife)
    {
        if (currentLife <= 120)
        {
            fase1.gameObject.SetActive(true);
            fase2.gameObject.SetActive(false);
            fase3.gameObject.SetActive(false);
            fase4.gameObject.SetActive(false);
        }
        if (currentLife <= 90)
        {
            fase1.gameObject.SetActive(false);
            fase2.gameObject.SetActive(true);
            fase3.gameObject.SetActive(false);
            fase4.gameObject.SetActive(false);
        }
        if (currentLife <= 60)
        {
            fase1.gameObject.SetActive(false);
            fase2.gameObject.SetActive(false);
            fase3.gameObject.SetActive(true);
            fase4.gameObject.SetActive(false);
        }
        if (currentLife <= 30)
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
        if (ID >= 0 && ID < Bag.Count && activeInventory == true)
        {
            GameObject selectedSlot = Bag[ID];
            Image selectedImage = selectedSlot.GetComponent<Image>();

            Thermal thermalScript = FindObjectOfType<Thermal>(); // Buscar el script Thermal en la escena

            if (thermalScript != null && selectedImage.enabled && selectedSlot.CompareTag("Item") && thermalScript.IsNearGenerator())
            {
                thermalScript.ReceiveItemFromInventory(selectedSlot); // Llama al metodo en Thermal para transferir el objeto
                RemoveItemFromInventory(); // Remueve el objeto del inventario
                inventoryManager.CountOtherUsed--;
                fusibleColocados++;
            }
        }
    }



    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.CompareTag("Item") & CountItemsWithTag("Item") < maxOtherItems) ||
            (coll.CompareTag("Medic") && CountItemsWithTag("Medic") < maxMedicItems) ||
            (coll.CompareTag("Objects") && CountItemsWithTag("Objects") < maxOtherItems))
        {
            AddItemToInventory(coll);
        }

        if (coll.CompareTag("Medic") && inventoryManager.CountMeducUsed <3)
        {
            audioSource.PlayOneShot(collectMedic);
        }

        if (coll.CompareTag("Item") && inventoryManager.CountOtherUsed <3)
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

        if (ID >= 0 && ID < Bag.Count && activeInventory == true)
        {
            GameObject selectedSlot = Bag[ID];
            Image selectedImage = selectedSlot.GetComponent<Image>();

           

            if (selectedImage.enabled && selectedSlot.CompareTag("Medic"))
            {
                audioSource.PlayOneShot(useMedic);
                //Debug.Log("Te estoy curando");
                player.GetLife();

                
                if (player != null && player.audioSource != null)
                {
                   
                    player.audioSource.volume += 0.3f;

                   
                    player.audioSource.volume = Mathf.Clamp01(player.audioSource.volume);
                }
                inventoryManager.CountMeducUsed--;
                RemoveItemFromInventory();
            }
        }
    }

    //void ToggleInventory()
    //{
    //    activeInventory = !activeInventory;
    //    inventory.SetActive(activeInventory);

    //    if (activeInventory)
    //    {
    //        PauseGame();
    //    }
    //    else
    //    {
    //        ResumeGame();
    //    }
    //}

    void RemoveItemFromInventory()
    {
        if (ID >= 0 && ID < Bag.Count && activeInventory == true)
        {
            audioSource.PlayOneShot(remove);
            Bag[ID].GetComponent<Image>().enabled = false;
            Bag[ID].GetComponent<Image>().sprite = null;
            Bag[ID].tag = "Untagged";
        }
    }

    void Nav()
    {
        if (!isGamePaused && activeInventory == true)
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


    


    private void TermicDescription()
    {
        
            GameObject selectedSlot = Bag[ID];
            Image selectedImage = selectedSlot.GetComponent<Image>();

            if (selectedImage.enabled && selectedSlot.CompareTag("Item"))
            {
                onTermicItem = true;
               
            }
            else
            {
                
                onTermicItem = false;
            }

            if (selectedImage.enabled && selectedSlot.CompareTag("Medic"))
            {
                onMedicItem = true;
            }
            else
            {
                onMedicItem = false;
            }

            DescripcionMedic.SetActive(onMedicItem);

            DescripcionTermica.SetActive(onTermicItem);
            
        
        
    }

}