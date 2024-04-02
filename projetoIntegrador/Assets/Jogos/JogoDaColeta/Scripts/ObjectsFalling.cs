using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFalling : MonoBehaviour
{
    public float fallSpeed = 1f;

    public AudioSource audioSource;

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Destruir o objeto pego
    //        Destroy(gameObject);

    //        Aumentar os erros
    //        PlayerColeta.MissingObject();
    //    }
    //}

    void Update()
    {
        // Ajusta o volume de acordo com o VolumeControl
        audioSource.volume = VolumeControl.volume;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, -fallSpeed);

        if (transform.position.y < -4.5f)
        {
            //audioSource.Play();
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);


            // Destruir o objeto que caiu no ch�o
            Destroy(gameObject);

            // Aumentar o n�mero de objetos perdidos do jogador
            PlayerColeta.missingObjects--;
        }
    }

}


