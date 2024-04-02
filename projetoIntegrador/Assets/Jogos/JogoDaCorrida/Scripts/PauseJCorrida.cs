using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseJCorrida : MonoBehaviour
{
    public GameObject panelPause;
    public bool gamePaused;
    public bool pressButtonNoPause; //caso obotao de despausar seja apertado, a contagem regressiva é ativada
    public Button bt_pause;
    public int time;
    public TMP_Text txt_Countdown;
    // public bool pauseWithP;


    private void Awake()
    {
        pressButtonNoPause = false;
        //pauseWithP = true;
    }
    private void Start()
    {
        panelPause.SetActive(false);
        time = 3;

    }

    private void Update()
    {
        //if (pressButtonNoPause)
        //{
        //    time -= 0.003f;
        //    txt_Countdown.text = time.ToString("F0");
        //    if (time <= 0.0f)
        //    {
        //        pressButtonNoPause = false;
        //        txt_Countdown.gameObject.SetActive(false);
        //        gamePaused = false;
        //        //Time.timeScale = 1f;
        //        time = 3.0f;
        //        bt_pause.gameObject.SetActive(true);

        //    }
        //    else
        //    {
        //        pressButtonNoPause = true;
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.P) && pauseWithP && !pressButtonNoPause) //se o jogo nao tiver pausado, ele pode apertar o pause
        //{
        //    Time.timeScale = 0.0f;
        //    panelPause.gameObject.SetActive(true);
        //    bt_pause.gameObject.SetActive(false);
        //    pauseWithP = false;
        //    gamePaused = true;
        //}

        //else if (Input.GetKeyDown(KeyCode.P) && gamePaused)//se o jogo tiver pausado, aperta pra despasuar
        //{
        //    gamePaused = false;
        //    pauseWithP = true;
        //    NoPause();
        //}
    }
    public void Pause() //aciona o pause
    {
        //Time.timeScale = 0.0f;
        gamePaused = true;
        panelPause.gameObject.SetActive(true);
        bt_pause.gameObject.SetActive(false);

    }

    public void NoPause() //tira do pause
    {
        panelPause.gameObject.SetActive(false);
        txt_Countdown.gameObject.SetActive(true);
        pressButtonNoPause = true;
        StartCoroutine(Countdown());

    }
    public void GoMap(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
    public void GoMapInstructions()
    {
        pressButtonNoPause = true;
        gamePaused = true;
    }

    private IEnumerator Countdown()//contador regressivo

    {
        for (int i = time; i >= 0; i--)
        {
            txt_Countdown.text = i.ToString();
            yield return new WaitForSeconds(1);

            if (i <= 0)
            {
                pressButtonNoPause = false;
                txt_Countdown.gameObject.SetActive(false);
                gamePaused = false;
                time = 3;
                bt_pause.gameObject.SetActive(true);

            }
        }
       
    }

}