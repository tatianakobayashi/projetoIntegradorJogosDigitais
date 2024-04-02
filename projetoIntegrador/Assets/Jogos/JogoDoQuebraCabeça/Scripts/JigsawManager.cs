using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class JigsawManager : MonoBehaviour
{
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

            instruction = true;
            StartCoroutine(SpawnInstructions());
            Pause();
        }
        else
        {
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
        DisableParts();
        randon.pause = true;
    }
    public void NoInstruction()
    {
        randon.pause = false;
    }
    public void NoPause()
    {
        EnableParts();
        randon.pause = false;
    }
    public void GoGame()
    {
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
