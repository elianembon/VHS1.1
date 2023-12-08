using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{

    public GameObject puzzle;

    private bool activePuzzle;

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
