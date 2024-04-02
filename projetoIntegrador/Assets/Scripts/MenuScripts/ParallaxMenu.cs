    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxMenu : MonoBehaviour
{
    [SerializeField] private Object image;
    [SerializeField] private float velocity;
    [SerializeField] private Transform destine;

    void Update()
    {
        Parallax();
    }

    public void Parallax()
    {
        // Obtém a posição atual do objeto
        Vector3 currentPosition = transform.position;

        // Calcula a nova posição do objeto movendo-se para a direita
        currentPosition.x -= velocity * Time.deltaTime;

        // Atualiza a posição do objeto
        transform.position = currentPosition;
    }
    /*
    public void Teleport()
    {
        transform.position = destine.position;
    }*/
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Menu"))
        {
            transform.position = new Vector3(destine.transform.position.x, transform.position.y);
        }
    }
}


