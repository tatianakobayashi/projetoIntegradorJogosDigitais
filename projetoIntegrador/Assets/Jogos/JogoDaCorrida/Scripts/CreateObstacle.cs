using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacle : MonoBehaviour
{
    public List<GameObject> prefabObstacle;
    private GameControllerJCorrida gameControllerJCorrida;
    private MovimentPlayer movimentPlayerScript;
    private PauseJCorrida pauseJCorridaScript;
    public float maxTime;
    public float minTime;
    public float decreaseMaxTime;
    private Progression progressionScript;
    public float currentSpeedInfor;
    public bool progressCreateOScript;
    private List<Obstacle> spawnedObstacles { get; set; } = new List<Obstacle>(); // Lista de obstáculos instanciados

    private void Start()
    {
        gameControllerJCorrida = FindObjectOfType<GameControllerJCorrida>();
        movimentPlayerScript = FindObjectOfType<MovimentPlayer>();
        progressionScript = FindObjectOfType<Progression>();
        pauseJCorridaScript = FindObjectOfType<PauseJCorrida>();
        StartCoroutine(Spawn());
        progressCreateOScript = false;
    }

    IEnumerator Spawn() //instancia os obstaculos
    {
        while (!gameControllerJCorrida.gameOver)
        {
            float time;
            if (pauseJCorridaScript.gamePaused)
            {
                
            }
            else
            {
                int obstacleIndex = Random.Range(0, prefabObstacle.Count);
                GameObject newObstacle = Instantiate(prefabObstacle[obstacleIndex], transform.position, Quaternion.identity);
                Obstacle obstacleScript = newObstacle.GetComponent<Obstacle>();
                obstacleScript.SetObstacleSpeed(currentSpeedInfor);
                spawnedObstacles.Add(obstacleScript);
                newObstacle.transform.position = new Vector2(transform.position.x, transform.position.y);
            }
            time = Random.Range(minTime, maxTime);//Os obstaculos voltam a ser instaciados dentro do tempo
            yield return new WaitForSeconds(time);

        }
    }

    void Update() //verifica se atingiu a meta 
    {
        if (progressionScript.atingiuAMeta) //estabiliza a velocidade da instanciacao, dependnedo da distancia percorrida
        {
            if (movimentPlayerScript.distance >= 1500f && movimentPlayerScript.distance < 2750f)
            {
                maxTime = 2f;

            }
            else if (movimentPlayerScript.distance >= 2750f && movimentPlayerScript.distance < 3000f)
            {
                maxTime = 1.75f;

            }
            else if (movimentPlayerScript.distance >= 3000f && movimentPlayerScript.distance < 4000)
            {
                maxTime = 1f;

            }
            else
            {
                maxTime = maxTime - decreaseMaxTime;
            }
            progressCreateOScript = true;
            currentSpeedInfor++;
            IncreaseObstacleSpeed();

        }
    }

    void IncreaseObstacleSpeed()  //muda a vellocidade doos obstaculos que estao na cena
    {
        foreach (Obstacle obstacle in spawnedObstacles)
        {
            if (obstacle != null)
            {
                obstacle.SetObstacleSpeed(currentSpeedInfor);
            }
        }
    }
}