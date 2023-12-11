using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveItem : MonoBehaviour
{
    protected InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            string tag = gameObject.tag;

            if (tag == "Medic" && inventoryManager.CountMeducUsed < 3)
            {
                inventoryManager.IncrementMeducUsed();
                Destroy(gameObject);
            }
            else if (tag == "Item" && inventoryManager.CountOtherUsed < 3)
            {
                inventoryManager.IncrementOtherUsed();
                Destroy(gameObject);
            }

            
        }
    }
}