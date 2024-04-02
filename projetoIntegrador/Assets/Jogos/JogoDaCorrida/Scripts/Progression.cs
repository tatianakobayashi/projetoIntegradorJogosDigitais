using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Progression : MonoBehaviour
{
    public float meta;
    public float increaseMeta;
    private MovimentPlayer movimentPlayerScript;
    private ParallaxJCorrida parallaxJCorridaScript;
    private CreateObstacle createobstacleScript;
    public bool atingiuAMeta;
    public int cont;
    void Start()
    {
        movimentPlayerScript = FindObjectOfType<MovimentPlayer>();
        parallaxJCorridaScript = FindObjectOfType<ParallaxJCorrida>();
        createobstacleScript = FindObjectOfType<CreateObstacle>();

        atingiuAMeta = false;
        cont = 0;
    }

    
    void Update()//verifica se atingiu a meta
    {
        if (movimentPlayerScript.distance >= meta)  
        {
            atingiuAMeta = true;
            meta = meta + increaseMeta;
            cont++;
        }

        if (parallaxJCorridaScript.progressParallaxJScript && movimentPlayerScript.progressMovimentPScript && createobstacleScript.progressCreateOScript) //verifica se as variaveis estao verdadeiras para deixar falsa novamente o atingiu a meta
        {
            parallaxJCorridaScript.progressParallaxJScript = false;
            movimentPlayerScript.progressMovimentPScript = false;
            createobstacleScript.progressCreateOScript = false;
            atingiuAMeta = false;
        }
    }

}