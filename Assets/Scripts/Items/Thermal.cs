using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thermal : MonoBehaviour
{
    public bool isRepaired = false;
    private bool canSpaceInp = false;
    private List<GameObject> storedItems = new List<GameObject>();

    public Transform generatorTransform;
    public float minimumDistance = 5f;

    private AudioSource audioSource;
    public AudioClip repair;

    public EventHandler Victory;
    private eventManager _eventManager;
    private Inventory _inv;
    private Player _player;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _eventManager = FindObjectOfType<eventManager>();
        _inv = FindObjectOfType<Inventory>();
        _player = FindAnyObjectByType<Player>();
    }


    private void Update()
    {
        if (storedItems.Count == 6)
        {
            Repaired();
        }
    }

    void TryRepairGenerator()
    {
        if (!isRepaired && storedItems.Count == 6)
        {
            //RepairGenerator();
        }
    }

    void Repaired()
    {
        _eventManager.SendSanityPillsEvent(_inv._pillsUsed);
        _eventManager.SendSanatyEvent(FindAnyObjectByType<PlayerManager>().LightTimer);
        _eventManager.SendSanityDowngradeEvent(FindAnyObjectByType<PlayerManager>().DarkTimer);
        _eventManager.SendEnemyTimeChaseEvent(FindAnyObjectByType<Enemy>().chaseTime);
        _eventManager.SendFinalSanityEvent(_player.CurrentLife);

        SceneManager.LoadScene("Victoria");

    }

    // L�gica para verificar la proximidad al generador
    public bool IsNearGenerator()
    {
        float distance = Vector3.Distance(transform.position, generatorTransform.position);
        if (distance < minimumDistance)
        {
            return true;
        }
        else
            return false;
        
    }

    // Este metodo se llama cada vez que un objeto con el tag "Item" entra en el area de la termica
    public void ReceiveItemFromInventory(GameObject item)
    {
        if (storedItems.Count < 6)
        {
            audioSource.PlayOneShot(repair);
            storedItems.Add(item);
            //Debug.Log("Item transferido a la termica. Cantidad actual de items en la termica: " + storedItems.Count);
        }
        else
        {
            //Debug.Log("La termica ya tiene la cantidad maxima de items (6).");
        }
    }
}
