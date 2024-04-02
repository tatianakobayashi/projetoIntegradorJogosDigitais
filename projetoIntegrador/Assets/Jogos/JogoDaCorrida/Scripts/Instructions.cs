using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Instructions : MonoBehaviour
{
    public GameObject panelBackgroundInstruction;
    public GameObject panelInstructions;
    public GameObject panelPause;
    public GameObject score;
    private PauseJCorrida pauseJCorrida;
    void Start()
    {
        pauseJCorrida = GetComponent<PauseJCorrida>();
        // Verifica se as instruções já foram exibidas antes de iniciá-las.
        if (!PlayerPrefs.HasKey("InstructionsCorrida") || PlayerPrefs.GetInt("InstructionsCorrida") == 0)
        {
            pauseJCorrida.bt_pause.gameObject.SetActive(false);
            StartCoroutine(SpawnInstructions());
        }
        else
        {
            score.SetActive(true);
        }
    }

    public IEnumerator SpawnInstructions()
    {
        yield return new WaitForSeconds(0.01f);
        panelInstructions.gameObject.SetActive(true);
        panelBackgroundInstruction.gameObject.SetActive(true);
        PlayerPrefs.SetInt("InstructionsCorrida", 1); // Instruções exibidas.
        pauseJCorrida.gamePaused = true;//Time.timeScale = 0.0f;
    }

    public void GoGame()
    {
        panelInstructions.gameObject.SetActive(false);
        //Time.timeScale = 1.0f; MUDAR
        pauseJCorrida.bt_pause.gameObject.SetActive(true);
        pauseJCorrida.gamePaused = false;

    }

    public void OpenInstructions()
    {
        //Time.timeScale = 0.0f; MUDAR
        panelPause.SetActive(false);
        panelInstructions.gameObject.SetActive(true);
        panelBackgroundInstruction.gameObject.SetActive(true);
        pauseJCorrida.gamePaused = true;
    }
}
