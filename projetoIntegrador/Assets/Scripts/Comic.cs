using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comic : MonoBehaviour
{
    public bool instructionAquarium = false;
    public bool endPageLeft = false;
    public GameObject backButton;
    public GameObject comic;
    public Transform posComicOnePage;
    public Transform posComicOnePageEndLeft;
    public Transform posComicTwoPages;
    public float pageSpeed = 0.5f;
    public List<GameObject> pagesGameObject;
    public List<Image> pagesImage1;
    public List<Image> pagesImage2;
    public List<Sprite> pagesSprite;
    public GameObject forwardButton;
    public GameObject goMapButton;
    public JigsawManager jigsawManager;
    public int index = -1;
    public bool rotate = false;
    public float angle1;


    private void Awake()
    {
        jigsawManager = FindObjectOfType<JigsawManager>();
        if (jigsawManager != null)
            jigsawManager.DisableParts();
    }
    private void Start()
    {
        comic.transform.position = posComicOnePage.position;
        backButton.SetActive(false);

        int cont1 = 0;
        int cont2 = 0;
        for (int i = 0; i < (pagesImage1.Count + pagesImage2.Count); i++)
        {
            if (i % 2 == 0)
            {
                pagesImage1[cont1].sprite = pagesSprite[i];
                cont1++;
            }
            else
            {
                pagesImage2[cont2].sprite = pagesSprite[i];
                cont2++;
            }
        }
        pagesImage1.ForEach(image => image.gameObject.SetActive(true));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && index - 1 >= -1)
        {
            RotateBack();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && index < pagesGameObject.Count - 1 )
        {
            RotateForward();
        }
    }
    public void RotateForward()
    {
        if (rotate == true) { return; }
        index++;
        float angle = -180;
        pagesGameObject[index].transform.SetAsLastSibling();
        ForwardButtonActions();
        StartCoroutine(Rotate(angle, true));
    }
    public void ForwardButtonActions()
    {
        if (backButton.activeInHierarchy == false)
        {
            comic.transform.position = posComicTwoPages.position;
            backButton.SetActive(true);

        }
        if (index == pagesGameObject.Count - 1 && endPageLeft)
        {
            comic.transform.position = posComicOnePageEndLeft.position;
            forwardButton.SetActive(false);
        }
        else if (index == pagesGameObject.Count - 1) //Última página
        {
            comic.transform.position = posComicTwoPages.position;
            forwardButton.SetActive(false);
        }
    }
    public void RotateBack()
    {
        if (rotate == true) { return; }
        float angle = 0;
        pagesGameObject[index].transform.SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }
    public void BackButtonActions()
    {
        if (forwardButton.activeInHierarchy == false)
        {
            comic.transform.position = posComicTwoPages.position;
            forwardButton.SetActive(true);
        }
        if (index - 1 == -1) // Capa
        {
            comic.transform.position = posComicOnePage.position;
            backButton.SetActive(false);
        }

    }
    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;

        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            if (!instructionAquarium)
            {
                value += Time.deltaTime * pageSpeed;
            }
            else
            {
                value += Time.fixedDeltaTime * pageSpeed;
            }
            pagesGameObject[index].transform.rotation = Quaternion.Slerp(pagesGameObject[index].transform.rotation, targetRotation, value);
            angle1 = Quaternion.Angle(pagesGameObject[index].transform.rotation, targetRotation);

            if (angle1 <= 90f && forward && index >= 0)
            {
                pagesImage1[index].gameObject.SetActive(false); // Desativa a imagem da página 1
                pagesImage2[index].gameObject.SetActive(true);
            }
            else if (angle1 <= 90f && !forward && index >= 0 && index < pagesGameObject.Count)
            {
                pagesImage1[index].gameObject.SetActive(true);   // Ativa a imagem da página 1
                pagesImage2[index].gameObject.SetActive(false);
            }

            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }

    public void ResetComic()
    {
        index = -1;
        rotate = false;
        angle1 = 0f;

        // Reinicie as posições e ativação/inativação de botões conforme necessário
        comic.transform.position = posComicOnePage.position;
        backButton.SetActive(false);
        forwardButton.SetActive(true);
        pagesGameObject[1].transform.SetAsLastSibling();
        pagesGameObject[0].transform.SetAsLastSibling();

        // Ative todas as imagens da página 1 e desative as imagens da página 2
        foreach (var image in pagesImage1)
        {
            image.gameObject.SetActive(true);
        }

        foreach (var image in pagesImage2)
        {
            image.gameObject.SetActive(false);
        }

        // Reinicie a rotação das páginas
        foreach (var page in pagesGameObject)
        {
            page.transform.rotation = Quaternion.identity;
        }
        if (jigsawManager != null)
            jigsawManager.EnableParts();
    }
}