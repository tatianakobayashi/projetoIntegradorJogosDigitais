using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraMoviment : MonoBehaviour
{
    [Header("PATRULHAMENTO PREVIS�VEL:")]
    public bool presiblePatrolling = false;
    public List<GameObject> waypointsList;
    public bool isLoop = true;
    private int index = 0;

    [Header("PATRULHAMENTO IMPREVIS�VEL:")]
    public bool unpredictablePatrolling = false;
    public Transform[] waypoints;
    private int randomSpot;

    [Header("PATRULHAMENTO IMPREVIS�VEL LIMITADO:")]
    public bool limitedUnpredictablePatrolling = false;
    public Transform waypoint;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    [Header("GERAIS - PREVIS�VEL/IMPREVIS�VEL/LIMITADO: ")]
    private float waitTime;
    public float velocidade;
    public float startWaitTime;

    public Animator animator;
    public bool isMoving = true;
    void Start()
    {
        animator = GetComponent<Animator>();

        if (unpredictablePatrolling)
        {
            waitTime = startWaitTime;
            randomSpot = UnityEngine.Random.Range(0, waypoints.Length);
        }
        else if (limitedUnpredictablePatrolling)
        {
            waitTime = startWaitTime;
            waypoint.position = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
        }
        StartCoroutine(StartPatrolling());
    }

    private IEnumerator StartPatrolling()
    {
        while (true)
        {
            // Verificar se est� em movimento antes de iniciar o patrulhamento.
            if (isMoving)
            {
                if (presiblePatrolling)
                {
                    yield return StartCoroutine(PresiblePatrolling());
                }
                else if (unpredictablePatrolling)
                {
                    yield return StartCoroutine(UnpredictablePatrolling());
                }
                else if (limitedUnpredictablePatrolling)
                {
                    yield return StartCoroutine(LimitedUnpredictablePatrolling());
                }
            }

            // Espera antes de continuar para o pr�ximo waypoint.
            yield return new WaitForSeconds(5.0f); // Tempo de espera entre os waypoints.
        }
    }

    public void SetAnimation(Vector2 direction)
    {
        // Verificar se o movimento � diagonal.
        bool isDiagonal = Mathf.Abs(direction.x) > 0.1f && Mathf.Abs(direction.y) > 0.1f;

        // Verificar se o movimento � mais horizontal do que vertical.
        bool isMoreHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);

        if (isDiagonal)
        {
            // Movimento nas diagonais, priorizar a anima��o com base na dire��o do movimento.
            if (direction.y > 0 && direction.x == 0)
            {
                animator.SetFloat("Y", 1);
                animator.SetFloat("X", 0);
            }
            else if (direction.y < 0 && direction.x == 0)
            {
                animator.SetFloat("Y", -1);
                animator.SetFloat("X", 0);
            }
            else if (direction.x > 0)
            {
                animator.SetFloat("X", 1);
                animator.SetFloat("Y", 0);
            }
            else if (direction.x < 0)
            {
                animator.SetFloat("X", -1);
                animator.SetFloat("Y", 0);
            }
        }
        else
        {
            // Movimento em uma �nica dire��o (horizontal ou vertical).
            if (isMoreHorizontal)
            {
                // Movimento mais horizontal, priorizar a anima��o lateral.
                animator.SetFloat("X", Mathf.Sign(direction.x));
                animator.SetFloat("Y", 0);
            }
            else
            {
                // Movimento mais vertical, priorizar a anima��o vertical.
                animator.SetFloat("X", 0);
                animator.SetFloat("Y", Mathf.Sign(direction.y));
            }
        }
    }
    private IEnumerator PresiblePatrolling()
    {
        Vector2 destination = waypointsList[index].transform.position;

        while (Vector2.Distance(transform.position, destination) > 0.1f && isMoving)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            SetAnimation(direction);

            // Movimentar para o pr�ximo ponto.
            transform.position = Vector2.MoveTowards(transform.position, destination, velocidade * Time.deltaTime);

            yield return null;
        }

        // NPC chegou ao waypoint, vamos definir a anima��o de idle.
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);

        // Pausa antes de ir para o pr�ximo ponto.
        yield return new WaitForSeconds(2.0f);

        // Atualizar o �ndice para avan�ar para o pr�ximo waypoint.
        if (index < waypointsList.Count - 1)
        {
            index++;
        }
        else
        {
            if (isLoop)
            {
                index = 0;
            }
        }
    }
    private IEnumerator UnpredictablePatrolling()
    {
        Vector2 destination = waypoints[randomSpot].position;
        while (Vector2.Distance(transform.position, destination) > 0.1f && isMoving)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            SetAnimation(direction);

            // Movimentar para o pr�ximo ponto.
            transform.position = Vector2.MoveTowards(transform.position, destination, velocidade * Time.deltaTime);

            yield return null;
        }

        // NPC chegou ao waypoint, vamos definir a anima��o de idle.
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);

        // Pausa antes de ir para o pr�ximo ponto.
        yield return new WaitForSeconds(2.0f);

        // Atualizar o �ndice para avan�ar para o pr�ximo waypoint.
        randomSpot = UnityEngine.Random.Range(0, waypoints.Length);
    }
    private IEnumerator LimitedUnpredictablePatrolling()
    {
        Vector2 destination = waypoint.position;
        while (Vector2.Distance(transform.position, destination) > 0.1f && isMoving)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;

            // Chama o m�todo SetAnimation para definir a anima��o correta
            SetAnimation(direction);

            // Movimentar para o pr�ximo ponto.
            transform.position = Vector2.MoveTowards(transform.position, destination, velocidade * Time.deltaTime);

            yield return null;
        }

        // NPC chegou ao waypoint, vamos definir a anima��o de idle.
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);

        // Pausa antes de ir para o pr�ximo ponto.
        yield return new WaitForSeconds(2.0f);

        // Atualizar o ponto de destino para o pr�ximo movimento.
        waypoint.position = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
    }
}