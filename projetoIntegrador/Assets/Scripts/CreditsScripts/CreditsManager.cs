using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    private bool isColliding = false;
    private VanMoviment player;
    private FadeInOut fadeInOutScript;
    private bool podeContar;

    public Image componentImage;
    public Canvas canvas;
    public float time;


    private void Start()
    {
        player = FindObjectOfType<VanMoviment>();
        fadeInOutScript = FindObjectOfType<FadeInOut>();
        time = 3;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private void Update()
    {
        if (isColliding)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving)
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }

    public IEnumerator FadeIn()
    {
        //Deixa transparente, ativa e chama o IN
        //Color colorBook = new Color();
        //colorBook.a = 0;
        componentImage.gameObject.SetActive(true);
        //componentImage.GetComponent<Image>().color = colorBook;

        componentImage.GetComponent<Animator>().SetTrigger("In");
        SceneManager.LoadScene("Credits");
        yield return new WaitForSeconds(3);
        //componentImage.gameObject.SetActive(false);
        //podeContar = true;
    }

    public IEnumerator FadeOut()
    {
        componentImage.gameObject.SetActive(true);
        componentImage.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(3);
    }
}
