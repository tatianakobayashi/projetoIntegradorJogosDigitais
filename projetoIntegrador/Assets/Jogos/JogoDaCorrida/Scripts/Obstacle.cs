using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float sideVelocity;
    private GameControllerJCorrida gameControllerJCorrida;
    public Transform transformDelete;

    private PauseJCorrida pauseJCorridaScript;


    private void Start()
    {
        gameControllerJCorrida = FindObjectOfType<GameControllerJCorrida>();
        pauseJCorridaScript = FindObjectOfType<PauseJCorrida>();
        transformDelete = GameObject.Find("Obstacles").transform;
    }

    public void SetObstacleSpeed(float speed)
    {
        sideVelocity = speed;
    }

    void Update() //faz o obstaculo se mover para a esquerda e destroy
    {
        if (!gameControllerJCorrida.gameOver && pauseJCorridaScript.gamePaused==false)
        {
            transform.Translate(Vector3.left * sideVelocity * Time.deltaTime);
        }

        if (transformDelete != null)
        {
            if (transform.position.x <= transformDelete.transform.position.x)
                Destroy(gameObject);
        }
    }
}
