using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelOptions;
    [SerializeField] private GameObject panelBackground;
    //public FadeInOut fadeInOut;
    public Image componentImage;

    public void Play()
    {
        StartCoroutine(FadeSystem());
    }

    public void OpenOptions()
    {
        panelOptions.SetActive(true);
        if (panelMainMenu != null)
            panelMainMenu.SetActive(false);
        if (panelBackground != null)
            panelBackground.SetActive(true);
    }

    public void CloseOptions()
    {
        panelOptions.SetActive(false);
        if (panelMainMenu != null)
            panelMainMenu.SetActive(true);
        if (panelBackground != null)
            panelBackground.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void ChangeSceneEvent()
    {

        SceneManager.LoadScene(menuSceneName);
    }
    public IEnumerator FadeSystem()
    {
        Color colorImage = componentImage.GetComponent<Image>().color;
        colorImage.a = 0.0f;
        componentImage.GetComponent<Image>().color = colorImage;
        componentImage.gameObject.SetActive(true);
        componentImage.GetComponent<Animator>().SetTrigger("In");
        yield return new WaitForSeconds(3);
    }
}