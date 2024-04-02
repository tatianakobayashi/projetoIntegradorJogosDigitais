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
        // Obt�m a posi��o atual do objeto
        Vector3 currentPosition = transform.position;

        // Calcula a nova posi��o do objeto movendo-se para a direita
        currentPosition.x -= velocity * Time.deltaTime;

        // Atualiza a posi��o do objeto
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


