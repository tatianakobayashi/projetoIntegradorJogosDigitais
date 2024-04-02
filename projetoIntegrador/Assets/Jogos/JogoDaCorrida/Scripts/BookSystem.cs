using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BookSystem : MonoBehaviour
{
    //A cada 300 pontos ganha um livro
    public float metaTakeBook1;
    public float metaTakeBook2;
    public float metaTakeBook3;
    public float metaTakeBook4;
    public List<SpriteRenderer> spriteBook; //lista de livros/sprites da cena
    public int nBooksJCorrida;
    private MovimentPlayer movimentPlayerScript;
    private GameControllerJCorrida gameControllerJCorridaScript;
    private bool takeBook1;
    private bool takeBook2;
    private bool takeBook3;
    private bool takeBook4;
    private void Awake()
    {
        nBooksJCorrida = 0;
        FirstTime();
        PaintBooks();

    }
    private void Start()
    {
        takeBook1 = false;
        takeBook2 = false;
        takeBook3 = false;
        takeBook4 = false;
        movimentPlayerScript = FindObjectOfType<MovimentPlayer>();
        gameControllerJCorridaScript = FindObjectOfType<GameControllerJCorrida>();
    }

    private void FixedUpdate()
    {
        Color colorBook = new Color(spriteBook[0].GetComponent<SpriteRenderer>().color.r, spriteBook[0].GetComponent<SpriteRenderer>().color.g, spriteBook[0].GetComponent<SpriteRenderer>().color.b);
        colorBook.a = 1;

        if (nBooksJCorrida == 1)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (nBooksJCorrida == 2)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[1].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (nBooksJCorrida == 3)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[1].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[2].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (nBooksJCorrida == 4)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[1].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[2].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[3].GetComponent<SpriteRenderer>().color = colorBook;
        }
    }
    void Update()
    {
        if (movimentPlayerScript.distance >= metaTakeBook1 && nBooksJCorrida < 4 && !takeBook1)
        {
            takeBook1 = true;
            nBooksJCorrida++;
        }
        if (movimentPlayerScript.distance >= metaTakeBook2 && nBooksJCorrida < 4 && !takeBook2)
        {
            takeBook2 = true;
            nBooksJCorrida++;
        }
        if (movimentPlayerScript.distance >= metaTakeBook3 && nBooksJCorrida < 4 && !takeBook3)
        {
            takeBook3 = true;
            nBooksJCorrida++;
        }
        if (movimentPlayerScript.distance >= metaTakeBook4 && nBooksJCorrida < 4 && !takeBook4)
        {
            takeBook4 = true;
            nBooksJCorrida++;
        }
        if (gameControllerJCorridaScript.gameOver)
        {
            if(nBooksJCorrida > PlayerPrefs.GetInt("PastRoundJCorrida"))
            {
                PlayerPrefs.SetInt("CollectedBooksJCorrida", nBooksJCorrida);
                PlayerPrefs.SetInt("PastRoundJCorrida", nBooksJCorrida);
                PlayerPrefs.Save();
            }
        }
    }
    private void FirstTime()
    {
        if (!PlayerPrefs.HasKey("PastRoundJCorrida") && !PlayerPrefs.HasKey("CollectedBooksJCorrida"))
        {
            PlayerPrefs.SetInt("CollectedBooksJCorrida", 0);
            PlayerPrefs.SetInt("PastRoundJCorrida", 0);
            PlayerPrefs.Save();
        }
    }
    private void PaintBooks()
    {
        Color colorBook = new Color(spriteBook[0].GetComponent<SpriteRenderer>().color.r, spriteBook[0].GetComponent<SpriteRenderer>().color.g, spriteBook[0].GetComponent<SpriteRenderer>().color.b);
        colorBook.a = 1;

        if (PlayerPrefs.GetInt("PastRoundJCorrida") == 1)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (PlayerPrefs.GetInt("PastRoundJCorrida") == 2)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[1].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (PlayerPrefs.GetInt("PastRoundJCorrida") == 3)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[1].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[2].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (PlayerPrefs.GetInt("PastRoundJCorrida") == 4)
        {
            spriteBook[0].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[1].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[2].GetComponent<SpriteRenderer>().color = colorBook;
            spriteBook[3].GetComponent<SpriteRenderer>().color = colorBook;
        }
    }

    public void SaveBooksToBack()
    {
        if (nBooksJCorrida == 1)
        {
            if (nBooksJCorrida > PlayerPrefs.GetInt("PastRoundJCorrida"))
            {
                PlayerPrefs.SetInt("CollectedBooksJCorrida", nBooksJCorrida);
                PlayerPrefs.SetInt("PastRoundJCorrida", nBooksJCorrida);
                PlayerPrefs.Save();
            }

        }
        if (nBooksJCorrida == 2)
        {
            if (nBooksJCorrida > PlayerPrefs.GetInt("PastRoundJCorrida"))
            {
                PlayerPrefs.SetInt("CollectedBooksJCorrida", nBooksJCorrida);
                PlayerPrefs.SetInt("PastRoundJCorrida", nBooksJCorrida);
                PlayerPrefs.Save();
            }
        }
        if(nBooksJCorrida == 3)
        {
            if (nBooksJCorrida > PlayerPrefs.GetInt("PastRoundJCorrida"))
            {
                PlayerPrefs.SetInt("CollectedBooksJCorrida", nBooksJCorrida);
                PlayerPrefs.SetInt("PastRoundJCorrida", nBooksJCorrida);
                PlayerPrefs.Save();
            }
        }
        if(nBooksJCorrida == 4)
        {
           
            PlayerPrefs.SetInt("CollectedBooksJCorrida", nBooksJCorrida);
            PlayerPrefs.SetInt("PastRoundJCorrida", nBooksJCorrida);
            PlayerPrefs.Save();
            
        }
    }
}