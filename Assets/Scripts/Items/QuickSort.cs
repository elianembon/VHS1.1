using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public static class Quicksort<T>
{
    public static void Sort(List<T> list, Comparison<T> comparison, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, comparison, left, right);
            Sort(list, comparison, left, pivotIndex - 1);
            Sort(list, comparison, pivotIndex + 1, right);
        }
    }

    private static int Partition(List<T> list, Comparison<T> comparison, int left, int right)
    {
        int pivotIndex = (left + right) / 2; 
        T pivotValue = list[pivotIndex];
        Swap(list, pivotIndex, right);

        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (comparison(list[j], pivotValue) < 0)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, right);
        return i + 1;
    }

    private static void Swap(List<T> list, int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
