using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameControllerQuiz : MonoBehaviour
{
    public Image[] botao;
    public int numeroAleatorizado = 0;
    public GameObject gameOverQuiz;
    public Image coracao;
    public Image coracao2;
    public Image coracao3;
    public Sprite vidaCheia;
    public Sprite vidaUsada;
    public Sprite[] personagens;
    public Image PontoP;
    public GameObject painelGameOver;
    public GameObject paineloptions;
    public Image[] pontosTuristicos;
    public AudioSystem audioSystem;


    public TMP_Text pergunta;
    public TMP_Text respostaA;
    public TMP_Text respostaB;
    public TMP_Text respostaC;
    public TMP_Text respostaD;
    public TMP_Text infoRespostas;
    public TMP_Text pontuacao;
    public bool over;
    public List<SpriteRenderer> books;

    public AudioSource audioSourceAcerto;
    public AudioSource audioSourceErro;
    public AudioSource audioSourceLivro;


    [TextArea]
    public string[] perguntas;
    [TextArea]
    public string[] alternativaA;
    [TextArea]
    public string[] alternativaB;
    [TextArea]
    public string[] alternativaC;
    [TextArea]
    public string[] alternativaD;
    [TextArea]
    public string[] corretas;

    private List<int> indicesUtilizados = new List<int>(); // Lista para controlar índices utilizados
    private int idPergunta;
    private int endQuestions = 0;

    private float acertos;
    private float questoes;
    private float erros;
    private bool verificacao_pergunta = true;
    private int scoring;

    private void Awake()
    {
        scoring = PlayerPrefs.GetInt("QuizBooks");
        Books();


    }

    void Start()
    {
        int n = PlayerPrefs.GetInt("QuizBooks");
        over = false;
        gameOverQuiz.SetActive(false);
        idPergunta = 0;
        questoes = perguntas.Length;
        TrocaDePersonagens();

        ShuffleQuestions(); // Embaralha as perguntas no início do jogo
        ShowQuestion();
        FirstTime();
        audioSystem.PlaySound("QuizMusic");
        audioSystem.SetLooping(true);
    }

    void Update()
    {
       
        GameOver();
        Vida();
        audioSourceAcerto.volume = VolumeControl.volumeEffect;
        audioSourceErro.volume = VolumeControl.volumeEffect;
    }

    void ShuffleQuestions()
    {
        for (int i = 0; i < perguntas.Length; i++)
        {

            int temp = Random.Range(0, perguntas.Length);
            string tempPergunta = perguntas[temp];
            string tempAltA = alternativaA[temp];
            string tempAltB = alternativaB[temp];
            string tempAltC = alternativaC[temp];
            string tempAltD = alternativaD[temp];
            string tempCorreta = corretas[temp];

            perguntas[temp] = perguntas[i];
            alternativaA[temp] = alternativaA[i];
            alternativaB[temp] = alternativaB[i];
            alternativaC[temp] = alternativaC[i];
            alternativaD[temp] = alternativaD[i];
            corretas[temp] = corretas[i];

            perguntas[i] = tempPergunta;
            alternativaA[i] = tempAltA;
            alternativaB[i] = tempAltB;
            alternativaC[i] = tempAltC;
            alternativaD[i] = tempAltD;
            corretas[i] = tempCorreta;

        }

    }

    void ShowQuestion()
    {
        endQuestions++;
        if (indicesUtilizados.Count == perguntas.Length)
        {
            //TODO Talvez reiniciar o jogo, aqui ja foram todas as perguntas.
            StartCoroutine(Espera2());
            pontuacao.text = "Perguntas Corretas : " + acertos.ToString() + "/5";
            gameOverQuiz.SetActive(true);
            indicesUtilizados.Clear();
            //ShuffleQuestions();
        }

        int randomIndex = Random.Range(0, perguntas.Length);


        while (indicesUtilizados.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, perguntas.Length);
        }

        indicesUtilizados.Add(randomIndex); // Adicione o índice usado à lista

        idPergunta = randomIndex;
        pergunta.text = perguntas[idPergunta];
        respostaA.text = alternativaA[idPergunta];
        respostaB.text = alternativaB[idPergunta];
        respostaC.text = alternativaC[idPergunta];
        respostaD.text = alternativaD[idPergunta];
        infoRespostas.text = "Pergunta: " + (indicesUtilizados.Count).ToString() + "/5";

        verificacao_pergunta = true; // Reinicie a verificação da pergunta
    }

    public void resposta(string alternativa)
    {
        if (verificacao_pergunta)
        {

            StartCoroutine(Espera());
            verificacao_pergunta = false;

            if (alternativa == "A")
            {
                if (alternativaA[idPergunta] == corretas[idPergunta])
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[0]));
                    acertos++;
                    audioSourceAcerto.Play();
                }
                else
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[AchaCerta()]));
                    StartCoroutine(PiscaBotao(Color.white, Color.red, botao[0]));
                    erros++;
                    audioSourceErro.Play();
                }
            }
            else if (alternativa == "B")
            {
                if (alternativaB[idPergunta] == corretas[idPergunta])
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[1]));
                    acertos++;
                    audioSourceAcerto.Play();
                }
                else
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.red, botao[1]));
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[AchaCerta()]));
                    erros++;
                    audioSourceErro.Play();
                }
            }
            else if (alternativa == "C")
            {
                if (alternativaC[idPergunta] == corretas[idPergunta])
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[2]));
                    acertos++;
                    audioSourceAcerto.Play();
                }
                else
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.red, botao[2]));
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[AchaCerta()]));
                    erros++;
                    audioSourceErro.Play();
                }
            }
            else if (alternativa == "D")
            {
                if (alternativaD[idPergunta] == corretas[idPergunta])
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[3]));
                    acertos++;
                    audioSourceAcerto.Play();
                }
                else
                {
                    StartCoroutine(PiscaBotao(Color.white, Color.red, botao[3]));
                    StartCoroutine(PiscaBotao(Color.white, Color.green, botao[AchaCerta()]));
                    erros++;
                    audioSourceErro.Play();
                }
            }
        }
    }

    void Vida()
    {
        if (erros == 0)
        {
            coracao.sprite = vidaCheia;
            coracao2.sprite = vidaCheia;
            coracao3.sprite = vidaCheia;
        }
        else if (erros == 1)
        {
            coracao.sprite = vidaUsada;
            coracao2.sprite = vidaCheia;
            coracao3.sprite = vidaCheia;
        }
        else if (erros == 2)
        {
            coracao.sprite = vidaUsada;
            coracao2.sprite = vidaUsada;
            coracao3.sprite = vidaCheia;
        }
        else if (erros == 3)
        {
            coracao.sprite = vidaUsada;
            coracao2.sprite = vidaUsada;
            coracao3.sprite = vidaUsada;
        }
    }

    void GameOver()
    {
        if (erros == 3 || endQuestions > 5)
        {
            over = true;
            StartCoroutine(Espera2());
            pontuacao.text = "Perguntas Corretas : " + acertos.ToString() + "/5";
        }
        ScoringSystem();
    }
    void TrocaDePersonagens()
    {
        PontoP.sprite = personagens[Random.Range(0, 5)];
    }


    void ProximaPergunta()
    {
        verificacao_pergunta = true;
        TrocaDePersonagens();
        if (over)
        {
            GameOver();
        }
        else if (!over)
        {

            ShowQuestion();


        }
    }

    private IEnumerator PiscaBotao(Color corOriginal, Color corPisca, Image botao)
    {
        botao.color = corPisca;
        yield return new WaitForSeconds(1);
        botao.color = corOriginal;
        

    }

    private IEnumerator Espera()
    {
        yield return new WaitForSeconds(1);
        ProximaPergunta();
    }

    private IEnumerator Espera2()
    {
        yield return new WaitForSeconds(0.05f);
        painelGameOver.SetActive(true);
        gameOverQuiz.SetActive(true);

    }
    
    public int AchaCerta()
    {
        if (corretas[idPergunta] == alternativaA[idPergunta])
        {
            return 0;
        }
        if (corretas[idPergunta] == alternativaB[idPergunta])
        {
            return 1;
        }
        if (corretas[idPergunta] == alternativaC[idPergunta])
        {
            return 2;
        }
        if (corretas[idPergunta] == alternativaD[idPergunta])
        {
            return 3;
        }
        return 4;
    }

    public void ScoringSystem()
    {
        Color colorBook = new Color(books[0].GetComponent<SpriteRenderer>().color.r, books[0].GetComponent<SpriteRenderer>().color.g, books[0].GetComponent<SpriteRenderer>().color.b);
        colorBook.a = 1;

        if (acertos == 2)
        {
            scoring = 1;
            books[0].GetComponent<SpriteRenderer>().color = colorBook;
           
        }
        if (acertos == 3)
        {
            scoring = 2;
            books[1].GetComponent<SpriteRenderer>().color = colorBook;
        


        }
        if (acertos == 4)
        {
            scoring = 3;
            books[2].GetComponent<SpriteRenderer>().color = colorBook;
          

        }
        if (acertos == 5)
        {
            scoring = 4;
            books[3].GetComponent<SpriteRenderer>().color = colorBook;
          

        }
        if (scoring > PlayerPrefs.GetInt("RodadaPassada"))
        {
            PlayerPrefs.SetInt("QuizBooks", scoring);
            PlayerPrefs.SetInt("RodadaPassada", scoring);
            PlayerPrefs.Save();
        }

        //if (indexBooks == 1)
        //{
        //    books[0].GetComponent<SpriteRenderer>().color = colorBook;
        //}

    }

    public void Books()
    {
        Color colorBook = new Color(books[0].GetComponent<SpriteRenderer>().color.r, books[0].GetComponent<SpriteRenderer>().color.g, books[0].GetComponent<SpriteRenderer>().color.b);
        colorBook.a = 1;

        if (scoring == 1)
        {
            books[0].GetComponent<SpriteRenderer>().color = colorBook;
           
        }
        else if (scoring == 2)
        {
            books[0].GetComponent<SpriteRenderer>().color = colorBook;
            books[1].GetComponent<SpriteRenderer>().color = colorBook;
           
        }
        else if (scoring == 3)
        {
            books[0].GetComponent<SpriteRenderer>().color = colorBook;
            books[1].GetComponent<SpriteRenderer>().color = colorBook;
            books[2].GetComponent<SpriteRenderer>().color = colorBook;
          
        }
        else if (scoring == 4)
        {
            books[0].GetComponent<SpriteRenderer>().color = colorBook;
            books[1].GetComponent<SpriteRenderer>().color = colorBook;
            books[2].GetComponent<SpriteRenderer>().color = colorBook;
            books[3].GetComponent<SpriteRenderer>().color = colorBook;
           
        }
    }

    private void FirstTime()
    {
        if (!PlayerPrefs.HasKey("RodadaPassada"))
        {
            PlayerPrefs.SetInt("Rodadapassada", 0);

        }
    }
}
