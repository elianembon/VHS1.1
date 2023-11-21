using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafosMA : MonoBehaviour
{
    // Start is called before the first frame update
    
    
        public int[,] MAdy { get; set; }
        public int cantNodos { get; set; }
        public int[] Etiqs { get; set; }

        public GrafosMA(int numNodos)
        {
            cantNodos = numNodos;
            MAdy = new int[numNodos, numNodos];
            Etiqs = new int[numNodos];
        }

        public void AgregarArista(int nodoOrigen, int nodoDestino, int peso)
        {
            MAdy[nodoOrigen, nodoDestino] = peso;
            // Si el grafo es no dirigido, también asignar el peso en el sentido opuesto.
            // MAdy[nodoDestino, nodoOrigen] = peso;
        }
 
    

}
