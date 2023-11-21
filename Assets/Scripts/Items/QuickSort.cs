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
        int pivotValue = 2; // Valor correspondiente a "Medic"
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            int currentTagValue = GetTagValue(list[j].tag);
            if (currentTagValue < pivotValue)
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

    private static int GetTagValue(string tag)
    {
        // Asigna valores numéricos a las etiquetas
        switch (tag)
        {
            case "Medic":
                return 2;
            case "Item":
                return 1;
            case "Objects":
                return 3;
            default:
                return 0; // Valor predeterminado en caso de etiqueta desconocida
        }
    }
}
