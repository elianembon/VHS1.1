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

    private void Update()
    {
        if (!isRepaired && storedItems.Count == 10 && IsNearGenerator())
        {
            RepairGenerator();
        }
    }

    void RepairGenerator()
    {
        isRepaired = true;
        GameManager.Instance.Victoy();

        Debug.Log("Reparaci�n completada. Cambiando a la escena Victoria.");
        SceneManager.LoadScene("Victoria");
    }

    // L�gica para verificar la proximidad al generador
    public bool IsNearGenerator()
    {
        return true;
    }

    // Este m�todo se llama cada vez que un objeto con el tag "Item" entra en el �rea de la t�rmica
    public void ReceiveItemFromInventory(GameObject item)
    {
        if (storedItems.Count < 10)
        {
            storedItems.Add(item);
            Debug.Log("Item transferido a la t�rmica. Cantidad actual de items en la t�rmica: " + storedItems.Count);
        }
        else
        {
            Debug.Log("La t�rmica ya tiene la cantidad m�xima de items (10).");
        }
    }
}
