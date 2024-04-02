using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SystemBooksJColeta : MonoBehaviour
{
    private int books;
    private GameControllerJC gameControllerJCScript;
    public List<GameObject> booksList;
    public int pontsBook1;
    public int pontsBook2;
    public int pontsBook3;
    public int pontsBook4;

    private void Awake()
    {
        FirstTime();
        books = 0;
    }
    private void Start()
    {
        gameControllerJCScript = GetComponent<GameControllerJC>();
        PaintBooks();

    }
    private void Update()
    {
        GetBooks();
    }
    public void BooksPoints()
    {
        //int points;
        //if (int.TryParse(gameControllerJCScript.score.text, out points))
        //{
        if (FishsFalling.points >= pontsBook1 && FishsFalling.points < pontsBook2) // 40 a 84 pontos = 1 livro 
        {
            books = 1;
        }
        if (FishsFalling.points >= pontsBook2 && FishsFalling.points < pontsBook3)// 85 s 129 = 2 livros
        {
            books = 2;
        }
        if (FishsFalling.points >= pontsBook3 && FishsFalling.points < pontsBook3)// 130 a 199 = 3 livros
        {
            books = 3;
        }
        if (FishsFalling.points > pontsBook4)// 200 ou mais = 4 livro
        {
            books = 4;
        }
        SaveBooks();
        //}
    }
    private void FirstTime()
    {
        if (!PlayerPrefs.HasKey("PastRoundJColeta") && !PlayerPrefs.HasKey("CollectedBooksJColeta"))
        {
            PlayerPrefs.SetInt("CollectedBooksJColeta", 0);
            PlayerPrefs.SetInt("PastRoundJColeta", 0);
            PlayerPrefs.Save();


        }
    }
    private void SaveBooks()
    {
        if (books > PlayerPrefs.GetInt("PastRoundJColeta"))
        {
            PlayerPrefs.SetInt("CollectedBooksJColeta", books);
            PlayerPrefs.SetInt("PastRoundJColeta", books);
            PlayerPrefs.Save();
        }
    }

    private void PaintBooks()
    {
        Color colorBook = new Color(booksList[0].GetComponent<SpriteRenderer>().color.r, booksList[0].GetComponent<SpriteRenderer>().color.g, booksList[0].GetComponent<SpriteRenderer>().color.b);
        colorBook.a = 1;

        if (PlayerPrefs.GetInt("CollectedBooksJColeta") == 1)
        {
            booksList[0].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (PlayerPrefs.GetInt("CollectedBooksJColeta") == 2)
        {
            booksList[0].GetComponent<SpriteRenderer>().color = colorBook;
            booksList[1].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (PlayerPrefs.GetInt("CollectedBooksJColeta") == 3)
        {
            booksList[0].GetComponent<SpriteRenderer>().color = colorBook;
            booksList[1].GetComponent<SpriteRenderer>().color = colorBook;
            booksList[2].GetComponent<SpriteRenderer>().color = colorBook;
        }
        else if (PlayerPrefs.GetInt("CollectedBooksJColeta") == 4)
        {
            booksList[0].GetComponent<SpriteRenderer>().color = colorBook;
            booksList[1].GetComponent<SpriteRenderer>().color = colorBook;
            booksList[2].GetComponent<SpriteRenderer>().color = colorBook;
            booksList[3].GetComponent<SpriteRenderer>().color = colorBook;
        }
    }

    private void GetBooks()
    {
        Color colorBook = new Color(booksList[0].GetComponent<SpriteRenderer>().color.r, booksList[0].GetComponent<SpriteRenderer>().color.g, booksList[0].GetComponent<SpriteRenderer>().color.b);
        colorBook.a = 1;

        if (FishsFalling.points >= pontsBook1 && FishsFalling.points < pontsBook2) // >=40 e <85
        {
            // Define o alpha para 1.0 (transparência de 100%)
            booksList[0].GetComponent<SpriteRenderer>().color = colorBook;
        }
        if (FishsFalling.points >= pontsBook2 && FishsFalling.points < pontsBook3) // >=85 e <130
        {
            // Define o alpha para 1.0 (transparência de 100%)
            booksList[1].GetComponent<SpriteRenderer>().color = colorBook;
        }
        if (FishsFalling.points >= pontsBook3 && FishsFalling.points < pontsBook4) // >=130 e <199
        {
            // Define o alpha para 1.0 (transparência de 100%)
            booksList[2].GetComponent<SpriteRenderer>().color = colorBook;
        }
        if (FishsFalling.points >= pontsBook4) // 200 ou mais
        {
            // Define o alpha para 1.0 (transparência de 100%)
            booksList[3].GetComponent<SpriteRenderer>().color = colorBook;
        }
    }

    public void SaveBooksToBackMap()
    {
        if (FishsFalling.points >= pontsBook1 && FishsFalling.points < pontsBook2) // >=40 e <85 == 1 livro
        {
            if (1 > PlayerPrefs.GetInt("PastRoundJColeta"))
            {
                PlayerPrefs.SetInt("CollectedBooksJColeta", 1);
                PlayerPrefs.SetInt("PastRoundJColeta", 1);
                PlayerPrefs.Save();
            }
        }
        if (FishsFalling.points >= pontsBook2 && FishsFalling.points < pontsBook3) // >=85 e <130 == 2 livros
        {
            if (2 > PlayerPrefs.GetInt("PastRoundJColeta"))
            {
                PlayerPrefs.SetInt("CollectedBooksJColeta", 2);
                PlayerPrefs.SetInt("PastRoundJColeta", 2);
                PlayerPrefs.Save();
            }
        }
        if (FishsFalling.points >= pontsBook3 && FishsFalling.points < pontsBook4) // >=130 e <199 == 3 livros
        {
            if (3 > PlayerPrefs.GetInt("PastRoundJColeta"))
            {
                PlayerPrefs.SetInt("CollectedBooksJColeta", 3);
                PlayerPrefs.SetInt("PastRoundJColeta", 3);
                PlayerPrefs.Save();
            }
        }
        if (FishsFalling.points >= pontsBook4) // 200 ou mais == 4 livros
        {
            PlayerPrefs.SetInt("CollectedBooksJColeta", 4);
            PlayerPrefs.SetInt("PastRoundJColeta", 4);
            PlayerPrefs.Save();
        }
    }
}
