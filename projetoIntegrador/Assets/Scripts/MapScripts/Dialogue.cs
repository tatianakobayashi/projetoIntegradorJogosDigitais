using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Timers;
using UnityEngine.InputSystem;
using UnityEngine.Networking.PlayerConnection;

public class Dialogue : MonoBehaviour
{
    public GameObject player;
    public GameObject space;
    public Canvas canvas;
    public List<Sprite> images;
    public List<string> texts;
    public TextMeshProUGUI textComponent;
    public int currentCharIndex = 0;
    public int currentIndex = 0;
    public GameObject button;
    public bool completedText = false, startLeft = false;
    public Image panel;
    public Attractions attractions;
    private int cont = 0;
    private float textSpeed = 0.04f;
    private bool withBtn = false;
    private float textTimer = 0.0f;

    private Bubble currentBubble;

    void Awake()
    {
        GetComponent<Image>().sprite = images[currentIndex];
    }
    void Start()
    {
        if (startLeft)
        {
            LeftBubble();
        }
        else
        {
            RightBubble();
        }

    }

    void Update()
    {
        cont++;
        if (cont == 1)
        {
            player.GetComponent<VanMoviment>().enabled = false;
        }

        if (!withBtn && currentIndex < images.Count)
        {
                GetComponent<Image>().sprite = images[currentIndex]; //Atualiza a imagem atual
            
            if (textComponent.text == texts[currentIndex] && Input.GetKeyDown(KeyCode.Space) )
            {
                currentCharIndex = 0;
                currentIndex++;
                textTimer = 0.0f; //Reinicia o temporizador
                completedText = false;
                NextDialogue();

                if (currentIndex >= images.Count && button != null)
                {
                    textComponent.text = " ";
                    button.SetActive(true);
                    space.SetActive(false);
                    withBtn = true;
                }
                else if (currentIndex >= images.Count)
                 {
                     CloseDialogue();
                 }
            }
            else if (textComponent.text != texts[currentIndex] && Input.GetKeyDown(KeyCode.Space))
            {
                completedText = true;
                textComponent.text = texts[currentIndex];
            }

            if (texts.Count != 0 && !completedText && !withBtn && currentIndex < images.Count)
            {
                //Se ainda houver caracteres do texto para serem exibidos
                if (currentCharIndex < texts[currentIndex].Length)
                {
                    textTimer += Time.deltaTime; //Incrementa o temporizador do texto

                    if (textTimer >= textSpeed) //Verifica se o atraso foi alcan�ado
                    {
                        textTimer = 0.0f; //Reinicia o temporizador
                        currentCharIndex++;    //Incrementa o �ndice do caractere atual
                        textComponent.text = texts[currentIndex].Substring(0, currentCharIndex); //Exibe o texto a partir do primeiro caractere at� o caractere atual
                    }
                }
            }
        }
    }

    private void RightBubble()
    {
        currentBubble = Bubble.Right;
        GetComponent<Animator>().SetBool("dialogueRight", true);
        GetComponent<Animator>().SetBool("dialogueLeft", false);
    }

    public void LeftBubble()
    {
        currentBubble = Bubble.Left;
        GetComponent<Animator>().SetBool("dialogueRight", false);
        GetComponent<Animator>().SetBool("dialogueLeft", true);
    }

    public void NextDialogue()
    {
        if (currentBubble == Bubble.Left)
        {
            if (attractions == Attractions.Museu && currentIndex == 4 || attractions == Attractions.Aquario && currentIndex == 9)
            {
                LeftBubble();
            }
            else
            {
                RightBubble();
            }
        }
        else if (currentBubble == Bubble.Right)
        {
            LeftBubble();
        }
    }
    public void CloseDialogue()
    {
        canvas.gameObject.SetActive(false);
        currentCharIndex = 0;
        currentIndex = 0;
        cont = 0;
        completedText = false;
        player.GetComponent<VanMoviment>().enabled = true;
        panel.gameObject.SetActive(false);
        if (button != null)
        {
            space.SetActive(true);
            button.SetActive(false);
            withBtn = false;
        }
        if (attractions == Attractions.Aquario || attractions == Attractions.Morada)
            currentBubble = Bubble.Left;
        else
            currentBubble = Bubble.Right;
    }
    public enum Attractions
    {
        Aquario,
        Museu,
        Morada,
        Default
    }

    private enum Bubble { Left, Right }
}