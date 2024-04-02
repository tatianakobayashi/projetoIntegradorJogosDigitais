using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{    
    public Image componentImage;
    public Canvas canvas;
  
    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(FadeSystem());
       
     
    }

    public IEnumerator FadeSystem()
    {
        componentImage.gameObject.SetActive(true);
        componentImage.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(3);
        componentImage.gameObject.SetActive(false);
    }
}
