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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

        Debug.Log("Reparación completada. Cambiando a la escena Victoria.");
        SceneManager.LoadScene("Victoria");
    }

    // Lógica para verificar la proximidad al generador
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

    // Este método se llama cada vez que un objeto con el tag "Item" entra en el área de la térmica
    public void ReceiveItemFromInventory(GameObject item)
    {
        if (storedItems.Count < 10)
        {
            audioSource.PlayOneShot(repair);
            storedItems.Add(item);
            Debug.Log("Item transferido a la térmica. Cantidad actual de items en la térmica: " + storedItems.Count);
        }
        else
        {
            Debug.Log("La térmica ya tiene la cantidad máxima de items (10).");
        }
    }
}
