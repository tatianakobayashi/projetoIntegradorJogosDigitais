using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerJC : MonoBehaviour
{
    public GameObject btn_Exit;
    public GameObject btn_Pause;
    public GameObject box_Points;

    public GameObject[] prefabFishs;
    public GameObject gameOverObject;
    public TextMeshProUGUI score_txt;
    public TextMeshProUGUI missScore_txt;
    public TextMeshProUGUI waveInfoText;
    public TextMeshProUGUI score;

    public int maxEnemiesPerWave = 10;
    public float initialSpawnDelay = 3f;
    public float spawnInterval = 3f;
    public float spawnRateIncrease = 0.2f;

    private int currentWave = 1;
    private int enemiesSpawned = 0;
    private float nextSpawnTime = 0f;

    public Canvas scrollViewInstructions;

    public bool isOptionsMenuActive = false;
    private bool isPaused = false;
    private bool isUnpauseDelayed = false;
    private float unpauseTimer = 3f;
    public GameObject optionsMenu; // Arraste o prefab do menu de opções para este campo no Inspector

    [SerializeField] private TextMeshProUGUI unpauseCount;
    private GameObject instantiatedPrefabs;
    private SystemBooksJColeta systemBooksJColetaScript;

    public AudioSystem audioSystem;

    public bool gameOver;

    private void Awake()
    {
        gameOver = false;

    }
    void Start()
    {
        systemBooksJColetaScript = GetComponent<SystemBooksJColeta>();
        gameOverObject.SetActive(false);
        PlayerColeta.missingObjects = 5;
        StartCoroutine(SpawnObjects());
        FishsFalling.points = 0;

        audioSystem.PlaySound("MenuMusic");
        audioSystem.SetLooping(true);

        // Verifica se as instruções já foram exibidas antes de iniciá-las.
        /*if (!PlayerPrefs.HasKey("InstructionsShown") || PlayerPrefs.GetInt("InstructionsShown") == 0)
        {
            StartCoroutine(SpawnInstructions());
        }*/

    }

    public void Update()
    {
        Score();

        if (isUnpauseDelayed)
        {

            // Desativa o menu de opções
            optionsMenu.SetActive(false);
            isOptionsMenuActive = false;


            unpauseCount.text = unpauseTimer.ToString("F0");



            unpauseTimer -= Time.unscaledDeltaTime; // Usando Time.unscaledDeltaTime para contar o tempo independentemente do Time.timeScale

            if (unpauseTimer <= 0)
            {
                btn_Pause.gameObject.SetActive(true);
                Time.timeScale = 1;
                isPaused = false;
                isUnpauseDelayed = false;

                unpauseTimer = 3f;


                unpauseCount.gameObject.SetActive(false);

            }
        }

        GameOver();
        // Check if the game over condition is met
        if (PlayerColeta.missingObjects <= 0)
        {
            // Disable fish and trash spawning
            nextSpawnTime = float.MaxValue;
            Destroy(instantiatedPrefabs);

            unpauseCount.gameObject.SetActive(false);


        }
    }


    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            unpauseTimer = 3;


            if (isOptionsMenuActive)
            {
                // Se o menu de opções estiver ativo, desative-o
                optionsMenu.SetActive(false);
                isOptionsMenuActive = false;
            }
            else
            {
                // Se o menu de opções estiver inativo, ative-o
                optionsMenu.SetActive(true);
                isOptionsMenuActive = true;
            }
        }
    }

    public void Pause()
    {
        if (!PlayerPrefs.HasKey("contPauseJC"))
        {
            Time.timeScale = 0;
            isPaused = true;
            unpauseTimer = 3;
            PlayerPrefs.SetInt("contPauseJC", 1);
        }
    }
    public void PauseAndUnpause()
    {
        if (!isPaused && PlayerColeta.missingObjects > 0)
        {
            PauseGame();
        }
        else // Se já está pausado
        {
            if (!isUnpauseDelayed)
            {
                isUnpauseDelayed = true;
                unpauseTimer = 3f; // Reseta o contador ao pressionar 'P'
                unpauseCount.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            if (Time.time >= nextSpawnTime && enemiesSpawned < maxEnemiesPerWave)
            {
                float x = Random.Range(-8f, 8f);
                float y = 7f;
                Vector2 spawnPosition = new Vector2(x, y);
                instantiatedPrefabs = Instantiate(prefabFishs[Random.Range(0, prefabFishs.Length)], spawnPosition, Quaternion.identity);
                enemiesSpawned++;
                nextSpawnTime = Time.time + spawnInterval;
            }
            if (enemiesSpawned >= maxEnemiesPerWave)
            {
                currentWave++;
                maxEnemiesPerWave += Mathf.RoundToInt(maxEnemiesPerWave * spawnRateIncrease);
                enemiesSpawned = 0;
                nextSpawnTime = Time.time + initialSpawnDelay;
                spawnInterval -= 0.1f;
            }
            waveInfoText.text = string.Format("Wave: {0}\nEnemies Spawned: {1}/{2}\nSpawn Interval: {3:F1}s", currentWave, enemiesSpawned, maxEnemiesPerWave, spawnInterval);

            yield return null;
        }
    }

    public void Score()
    {
        // Texts that appear in the game
        missScore_txt.text = PlayerColeta.missingObjects.ToString();
        score_txt.text = FishsFalling.points.ToString();
    }

    public void GameOver()//Se tiver dado game over
    {
        int resetScore = 0;
        if (PlayerColeta.missingObjects <= 0)
        {
            btn_Exit.SetActive(false);
            btn_Pause.SetActive(false);
            box_Points.SetActive(false);
            score_txt.gameObject.SetActive(false);
            score.text = score_txt.text;
            gameOverObject.SetActive(true);
            score_txt.text = resetScore.ToString();
            gameOver = true;
            systemBooksJColetaScript.BooksPoints();
        }

    }

    /*public IEnumerator SpawnInstructions()
    {
        yield return new WaitForSeconds(1);
        scrollViewInstructions.gameObject.SetActive(true);
        PlayerPrefs.SetInt("InstructionsShown", 1); // Instruções exibidas.
    }

    public void GoGame()
    {
        scrollViewInstructions.gameObject.SetActive(false);
    }*/
}
