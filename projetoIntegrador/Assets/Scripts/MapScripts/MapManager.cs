using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject cameraMain;
    public GameObject panelTransition;
    public GameObject scrollViewInstructions;
    public GameObject player;
    public Image panel;
    public Button btnInstruction;
    public Button btnSound;


    void Start()
    {
        // Verifica se as instruções já foram exibidas antes de iniciá-las.
        if (!PlayerPrefs.HasKey("InstructionsShown") || PlayerPrefs.GetInt("InstructionsShown") == 0)
        {
            cameraMain.GetComponent<Animator>().enabled = true;
            btnInstruction.enabled = false;
            btnSound.enabled = false;
            StartCoroutine(SpawnInstructions());
        }
        else
        {
            ColliderVan();
        }
    }

    public void ColliderVan()
    {
        player.GetComponent<BoxCollider2D>().enabled = true;
    }
    public IEnumerator SpawnInstructions()
    {
        yield return new WaitForSeconds(13.50f);
        cameraMain.GetComponent<Animator>().SetBool("CameraIdle", true);

        panelTransition.SetActive(true);
        yield return new WaitForSeconds(1);
        panel.gameObject.SetActive(true);
        player.GetComponent<BoxCollider2D>().enabled = false;
        scrollViewInstructions.SetActive(true);
        PlayerPrefs.SetInt("InstructionsShown", 1); // Instruções exibidas.
    }

    public void ButtonGoMap()
    {
        StartCoroutine(GoMap());
    }
    public IEnumerator GoMap()
    {
        scrollViewInstructions.SetActive(false);
        player.GetComponent<BoxCollider2D>().enabled = true;
        panelTransition.GetComponent<Animator>().SetBool("PanelTransitionFadeOut", true);
        yield return new WaitForSeconds(1.3f);
        cameraMain.GetComponent<Animator>().enabled = false;
        btnInstruction.enabled = true;
        btnSound.enabled = true;
        panelTransition.SetActive(false);
        panel.gameObject.SetActive(false);
    }
}
