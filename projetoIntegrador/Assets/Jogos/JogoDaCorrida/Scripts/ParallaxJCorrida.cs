using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxJCorrida : MonoBehaviour
{
    private const int count = 10;
    public float objectSpeed;
    public float increaseObjectSpeed;
    private Progression progressionScript;
    private PauseJCorrida pauseJCorridaScript;
    public List<GameObject> transformsSpawn;
    public GameObject transformRemove;
    public bool progressParallaxJScript;


    private void Start()
    {
        progressionScript = FindObjectOfType<Progression>();
        pauseJCorridaScript = FindObjectOfType<PauseJCorrida>();
        progressParallaxJScript = false;
    }
    private void Update()
    {
        
        if (progressionScript.atingiuAMeta)
        {
            progressParallaxJScript = true;
            ParallaxJCorrida[] objectsGame = FindObjectsOfType<ParallaxJCorrida>(); // procura todos objetos com esse script
            foreach (ParallaxJCorrida obj in objectsGame)
            {
                obj.IncreaseObjectsSpeed();
            }
        }

        if (pauseJCorridaScript.gamePaused==false)
        {
            transform.Translate(Vector3.left * objectSpeed * Time.deltaTime);
        }

        // Verifica se o objeto saiu dos limites realinha a posicao
        if (transform.position.x <= transformRemove.transform.position.x) 
        {
            int indexLocalSpawn = UnityEngine.Random.Range(0, transformsSpawn.Count); // Sorteia o local
            int indexFlip = UnityEngine.Random.Range(0, 1); //Sorteia se vai flipar ou não 
            gameObject.transform.position = new Vector2(transformsSpawn[indexLocalSpawn].transform.position.x, transformsSpawn[indexLocalSpawn].transform.position.y); //coloca na posicao do local sorteado;
            if(indexFlip == 0)//0 == flip
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }                                                                                                                                                          

        }
    }
    void IncreaseObjectsSpeed()// Aumenta a velocidade dos objetos
    {
        objectSpeed = objectSpeed + increaseObjectSpeed;
    }
}


   