using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveItem : MonoBehaviour
{

    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
