using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsQuiz : MonoBehaviour
{
    public GameObject panelInstructions;
    public GameObject panelBackground;
    void Start()
    {
        // Verifica se as instruções já foram exibidas antes de iniciá-las.
        if (!PlayerPrefs.HasKey("InstructionsQuiz") || PlayerPrefs.GetInt("InstructionsQuiz") == 0)
        {
            StartCoroutine(SpawnInstructions());
        }
    }

    public IEnumerator SpawnInstructions()
    {
        yield return new WaitForSeconds(0.01f);
        panelInstructions.gameObject.SetActive(true);
        panelBackground.SetActive(true);
        PlayerPrefs.SetInt("InstructionsQuiz", 1); // Instruções exibidas.
    }

    public void GoGame()
    {
        panelInstructions.gameObject.SetActive(false);
        panelBackground.SetActive(false);

        //Time.timeScale = 1.0f; MUDAR
    }

    public void OpenInstructions()
    {
        //Time.timeScale = 0.0f; MUDAR
        panelBackground.SetActive(true);
        panelInstructions.gameObject.SetActive(true);
    }
}
