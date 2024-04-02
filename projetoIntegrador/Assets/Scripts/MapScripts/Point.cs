using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Point : MonoBehaviour
{
    [Header("Posições")]
    public Transform pointUp;
    public Transform pointDown;
    public Transform pointLeft;
    public Transform pointRight;

    [Header("Booleanas")]
    public bool boolUp = false;
    public bool boolDown = false;
    public bool boolLeft = false;
    public bool boolRight = false;

    public GameObject player;
    public float transitionDurationUp = 0.5f;
    public float transitionDurationDown = 0.5f;
    public float transitionDurationLeft = 0.5f;
    public float transitionDurationRight = 0.5f;

    private AnimationWhell[] wheels;
    private VanMoviment vanMoviment;
    private bool isColliding = false;

    private void Start()
    {
        vanMoviment = player.GetComponent<VanMoviment>();
    }
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
        if (isColliding && !vanMoviment.isMoving)
        {
            if (vanMoviment.moveVector.x == 0 && vanMoviment.moveVector.y > 0 && boolUp)
            {
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<VanMoviment>().spriteVanUpAndDown;
                player.GetComponent<VanMoviment>().wheelRight.SetActive(false);
                player.GetComponent<VanMoviment>().wheelLeft.SetActive(false);
                MoveTo(pointUp, 0, transitionDurationUp);
            }
            else if (vanMoviment.moveVector.x == 0 && vanMoviment.moveVector.y < 0 && boolDown)
            {
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<VanMoviment>().spriteVanUpAndDown;
                player.GetComponent<VanMoviment>().wheelRight.SetActive(false);
                player.GetComponent<VanMoviment>().wheelLeft.SetActive(false);
                MoveTo(pointDown,0, transitionDurationDown);
            }
            else if (vanMoviment.moveVector.x < 0 && vanMoviment.moveVector.y == 0 && boolLeft)
            {
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<VanMoviment>().spriteVanLeft;
                player.GetComponent<VanMoviment>().wheelRight.SetActive(false);
                player.GetComponent<VanMoviment>().wheelLeft.SetActive(true);
                MoveTo(pointLeft, 80, transitionDurationLeft);
            }
            else if (vanMoviment.moveVector.x > 0 && vanMoviment.moveVector.y == 0 && boolRight)
            {
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<VanMoviment>().spriteVanRight;
                player.GetComponent<VanMoviment>().wheelRight.SetActive(true);
                player.GetComponent<VanMoviment>().wheelLeft.SetActive(false);
                MoveTo(pointRight, -80, transitionDurationRight);
            }
        }
    }

    void MoveTo(Transform target, int value, float transitionDuration)
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

            StartCoroutine(TransitionPlayer(targetPosition, value, transitionDuration));
        }
    }

    IEnumerator TransitionPlayer(Vector3 targetPosition, int value, float transitionDuration)
    {
        wheels = FindObjectsOfType<AnimationWhell>();

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].speedRotation = value;
        }
        vanMoviment.isMoving = true;
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

        vanMoviment.isMoving = false;
        
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].speedRotation = 0;
        }
    }

}
