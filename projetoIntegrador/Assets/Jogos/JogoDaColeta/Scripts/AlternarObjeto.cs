using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternarObjeto : MonoBehaviour
{
    public GameObject objeto1;
    public GameObject objeto2;
    private bool ativarObjeto1 = true;

    void Start()
    {
        AtivarObjetos();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (Time.timeScale != 0)
            {
                ativarObjeto1 = !ativarObjeto1;
                AtivarObjetos();
            }
        }
    }

    void AtivarObjetos()
    {
        if (ativarObjeto1)
        {
            objeto1.GetComponent<SpriteRenderer>().enabled = true;
            objeto1.GetComponent<Collider2D>().enabled = true;
            objeto2.GetComponent<SpriteRenderer>().enabled = false;
            objeto2.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            objeto1.GetComponent<SpriteRenderer>().enabled = false;
            objeto1.GetComponent<Collider2D>().enabled = false;
            objeto2.GetComponent<SpriteRenderer>().enabled = true;
            objeto2.GetComponent<Collider2D>().enabled = true;
        }
    }
}
