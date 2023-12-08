using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{


    public GameObject puzzle;

    private bool activePuzzle;

    private bool isConnecting = false;
    private Image currentStartImage;
    private LineRenderer currentLineRenderer;

    #region Luces fuera
    public Image lucesfuera1A;
    public Image lucesfuera2A;
    public Image lucesfuera3A;
    public Image lucesfuera4A;
    public Image lucesfuera5A;
    public Image lucesfuera6A;
    public Image lucesfuera1P;
    public Image lucesfuera2P;
    public Image lucesfuera3P;
    public Image lucesfuera4P;
    public Image lucesfuera5P;
    public Image lucesfuera6P;
    #endregion

    #region Luces negativas
    public Image lucesne1A;
    public Image lucesne2A;
    public Image lucesne3A;
    public Image lucesne1P;
    public Image lucesne2P;
    public Image lucesne3P;
    #endregion

    #region Luces positivas 
    public Image lucespo1A;
    public Image lucespo2A;
    public Image lucespo3A;
    public Image lucespo4A;
    public Image lucespo5A;
    public Image lucespo6A;
    public Image lucespo7A;
    public Image lucespo8A;
    public Image lucespo9A;
    public Image lucespo10A;
    public Image lucespo11A;
    public Image lucespo12A;
    public Image lucespo13A;
    public Image lucespo1P;
    public Image lucespo2P;
    public Image lucespo3P;
    public Image lucespo4P;
    public Image lucespo5P;
    public Image lucespo6P;
    public Image lucespo7P;
    public Image lucespo8P;
    public Image lucespo9P;
    public Image lucespo10P;
    public Image lucespo11P;
    public Image lucespo12P;
    public Image lucespo13P;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        activePuzzle = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!activePuzzle)
            {
                activePuzzle = true;
                puzzle.SetActive(true);
                PauseGame();
            }
            else
            {
                activePuzzle = false;
                puzzle.SetActive(false);
                ResumeGame();
            }
        }

    }

    void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }
    void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

}
