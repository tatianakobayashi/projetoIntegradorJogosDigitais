using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LateralPoint : MonoBehaviour
{
    [Header("Booleanas")]
    public bool lateralPointInitial = true;
    public bool boolUp;
    public bool boolLeft;
    public bool boolRight;

    [Header("------------------------")]

    public GameObject player;
    public GameObject pointPositionDestiny;
    public Transform pointPositionMap;
    public float transitionDuration = 0.5f;
    private AnimationWhell[] wheels;
    private bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
    private void Update()
    {
        if (isColliding)
        {
            if (lateralPointInitial)
            {
                player.GetComponent<VanMoviment>().isMoving = true;
                pointPositionDestiny.GetComponent<LateralPoint>().lateralPointInitial = false;
                player.transform.position = pointPositionDestiny.transform.position;
            }
            else
            {
                if (boolUp && !boolLeft && !boolRight)
                {
                    player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<VanMoviment>().spriteVanUpAndDown;
                    player.GetComponent<VanMoviment>().wheelRight.SetActive(false);
                    player.GetComponent<VanMoviment>().wheelLeft.SetActive(false);
                    MoveTo(pointPositionMap, 0);
                }
                else if (!boolUp && boolLeft && !boolRight)
                {
                    player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<VanMoviment>().spriteVanLeft;
                    player.GetComponent<VanMoviment>().wheelRight.SetActive(false);
                    player.GetComponent<VanMoviment>().wheelLeft.SetActive(true);
                    MoveTo(pointPositionMap, 80);
                }
                else if (!boolUp && !boolLeft && boolRight)
                {
                    player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<VanMoviment>().spriteVanRight;
                    player.GetComponent<VanMoviment>().wheelRight.SetActive(true);
                    player.GetComponent<VanMoviment>().wheelLeft.SetActive(false);
                    MoveTo(pointPositionMap, -80);
                }
            }
        }
    }
    void MoveTo(Transform target, int value)
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;

            // Calcula a direção para onde a van/player está se movendo
            Vector3 moveDirection = (targetPosition - player.transform.position).normalized;

            // Calcula o ângulo de rotação em radianos
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            // Rotaciona a van/player ligeiramente na direção do movimento
            player.transform.rotation = Quaternion.Euler(0, 0, targetAngle);

            StartCoroutine(TransitionPlayer(targetPosition, value));
        }
    }

    IEnumerator TransitionPlayer(Vector3 targetPosition, int value)
    {
        wheels = FindObjectsOfType<AnimationWhell>();

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].speedRotation = value;
        }
        Vector3 startingPos = player.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            player.transform.position = Vector3.Lerp(startingPos, targetPosition, t);

            // Adiciona translação ao longo do eixo z
            float zOffset = Mathf.Lerp(0, 1, t) * 0.2f; // Ajuste o valor 0.2f conforme necessário
            Vector3 positionWithOffset = player.transform.position + player.transform.forward * zOffset;
            player.transform.position = positionWithOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = targetPosition;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].speedRotation = 0;
        }
        player.GetComponent<VanMoviment>().isMoving = false;
        yield return new WaitForSeconds(0.1f);
        lateralPointInitial = true;
    }

}
