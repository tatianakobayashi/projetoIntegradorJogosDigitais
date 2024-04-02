using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Attractions : MonoBehaviour
{
    public Canvas canvasDialogue;
    public GameObject attraction0;
    public GameObject attraction1;
    public GameObject attraction2;
    public Image panel; 
    private bool isColliding = false;
    private VanMoviment player;

    private void Start()
    {
        player = FindObjectOfType<VanMoviment>();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attraction1 != null)
            {
                attraction0.gameObject.SetActive(true);
                attraction1.gameObject.SetActive(false);
                attraction2.gameObject.SetActive(false);
            }
            else
            {
                if (attraction0 != null)
                    attraction0.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            isColliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvasDialogue.gameObject.SetActive(false);
            if (attraction1 != null)
            {
                attraction0.gameObject.SetActive(false);
                attraction1.gameObject.SetActive(true);
                attraction2.gameObject.SetActive(true);
            }
            else
            {
                if (attraction0 != null)
                    attraction0.transform.localScale = new Vector3(1, 1, 1);
            }
            isColliding = false;
        }
    }

    private void Update()
    {
        if (isColliding)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving)
            {
                panel.gameObject.SetActive(true);
                canvasDialogue.gameObject.SetActive(true);
            }

        }
    }

}
