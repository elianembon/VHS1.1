using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Quicksort
{
    public static void Sort(List<GameObject> list, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, left, right);
            Sort(list, left, pivotIndex - 1);
            Sort(list, pivotIndex + 1, right);
        }
    }

    private static int Partition(List<GameObject> list, int left, int right)
    {
        string pivotTag = "Medic";
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (list[j].CompareTag(pivotTag))
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, right);
        return i + 1;
    }

    private static void Swap(List<GameObject> list, int i, int j)
    {
        GameObject temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
