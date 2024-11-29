using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveItem : MonoBehaviour
{
    protected InventoryManager inventoryManager;

    [SerializeField] string fuseName;

    private eventManager evenMan;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        evenMan = FindObjectOfType<eventManager>();
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
                evenMan.SendinteractionFuse(fuseName);
                Destroy(gameObject);
            }

            
        }
    }
}