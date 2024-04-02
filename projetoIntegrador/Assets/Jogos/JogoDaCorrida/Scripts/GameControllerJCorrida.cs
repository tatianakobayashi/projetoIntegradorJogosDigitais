using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class GameControllerJCorrida : MonoBehaviour
{
    public bool gameOver;
    public GameObject buttonPause;
    public GameObject panelGameOver;
    public GameObject panelOptions;
    public TMP_Text pointsText;
    private MovimentPlayer movimentPlayer;

    public AudioSystem audioSystem;

    private void Awake()
    {
        Time.timeScale = 1;
        movimentPlayer = FindObjectOfType<MovimentPlayer>();
    }

    private void Start()
    {
        audioSystem.PlaySound("GameMusicCorrida");
        audioSystem.SetLooping(true);
    }
    private void FixedUpdate()
    {
        if (gameOver) //verifica se perdeu
        {
            Time.timeScale = 0;
            pointsText.text = movimentPlayer.distance.ToString("F0");
            panelGameOver.SetActive(true);
            buttonPause.SetActive(false);
        }
    }

    public void OpenOptions()
    {
        panelOptions.gameObject.SetActive(true);
    }
    
    public void ExitOptions()
    {
        panelOptions.gameObject.SetActive(false);
    }

    void RestartGame()
    {
        
    }


}
