using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawManager : MonoBehaviour
{
    public Button btnPause;
    public Button btnExit;
    public RandonPositions randon;
    public GameObject instructions;
    public SpriteRenderer panel;
    public Comic comic;
    public List<DragEndDrop> dragEndDrop;
    private bool instruction = false;

    public AudioSystem audioSystem;
    private void Awake()
    {

        randon.pause = false;
        DragEndDrop[] dragEndDropArray = FindObjectsOfType<DragEndDrop>();

        // Inicializar a lista com os objetos encontrados
        dragEndDrop = new List<DragEndDrop>(dragEndDropArray);

        // Verifica se as instruções já foram exibidas antes de iniciá-las.
        if (!PlayerPrefs.HasKey("InstructionsQuebraCabeca") || PlayerPrefs.GetInt("InstructionsQuebraCabeca") == 0)
        {
            btnPause.enabled = false;
            instruction = true;
            StartCoroutine(SpawnInstructions());
            Pause();
        }
        else
        {
            EnableParts();
            Play();
        }
    }

    private void Start()
    {
        audioSystem.PlaySound("JigsawSong");
        audioSystem.SetLooping(true);
    }

    public void Play()
    {
        if (randon.reset != 1)
        { 
            randon.StartCoroutine(randon.RandSpriteButton());
        }
    }
    public IEnumerator SpawnInstructions()
    {
        panel.gameObject.SetActive(true);
        instructions.SetActive(true);
        PlayerPrefs.SetInt("InstructionsQuebraCabeca", 1); // Instruções exibidas.
        yield return null;
    }
    public void Pause()
    {
        btnExit.enabled = false;
        btnPause.enabled = false;
        DisableParts();
        randon.pause = true;
    }
    public void NoInstruction()
    {
        btnExit.enabled = false;
        btnPause.enabled = true;
        randon.pause = false;
    }
    public void NoPause()
    {
        EnableParts();
        btnExit.enabled = true;
        btnPause.enabled = true;
        randon.pause = false;
    }
    public void GoGame()
    {
        btnExit.enabled = true;
        btnPause.enabled = true;
        instructions.SetActive(false);
        panel.gameObject.SetActive(false);
        comic.rotate = false;
        comic.ResetComic();
        if (instruction)
        {
            randon.StartCoroutine(randon.RandSpriteButton());
            instruction = false;
        }
    }
    public void DisableParts()
    {
        for (int i = 0; i < dragEndDrop.Count; i++)
        {
            dragEndDrop[i].blockMove = false;
        }
    }
    public void EnableParts()
    {
        for (int i = 0; i < dragEndDrop.Count; i++)
        {
            if (!dragEndDrop[i].ok)
                dragEndDrop[i].blockMove = true;
        }
    }
}
