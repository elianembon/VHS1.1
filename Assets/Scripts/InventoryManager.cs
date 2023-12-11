using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    protected Inventory inventory;

    [SerializeField] public int CountMeducUsed;
    [SerializeField] public int CountOtherUsed;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        CountMeducUsed = 0;
        CountOtherUsed = 0;
    }

    public void IncrementMeducUsed()
    {
        CountMeducUsed++;
    }

    public void IncrementOtherUsed()
    {
        CountOtherUsed++;
    }
}