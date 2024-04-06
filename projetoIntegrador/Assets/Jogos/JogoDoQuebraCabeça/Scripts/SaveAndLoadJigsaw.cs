using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadJigsaw : MonoBehaviour
{
    public RandonPositions randon;

    void Awake()
    {
        if (PlayerPrefs.GetInt("saveIndex") == 1 && !PlayerPrefs.HasKey("Img0"))
        {
            PlayerPrefs.SetString("Img0", PlayerPrefs.GetString("randonName"));

            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img0"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            randon.isPlay = true;
        }
        else if (PlayerPrefs.GetInt("saveIndex") == 1  && PlayerPrefs.HasKey("Img0") && !PlayerPrefs.HasKey("Img1"))
        {
            PlayerPrefs.SetString("Img1", PlayerPrefs.GetString("randonName"));

            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img0"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img1"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            randon.isPlay = true;
        }
        else if (PlayerPrefs.GetInt("saveIndex") == 1 && PlayerPrefs.HasKey("Img0") && PlayerPrefs.HasKey("Img1") && !PlayerPrefs.HasKey("Img2"))
        {
            PlayerPrefs.SetString("Img2", PlayerPrefs.GetString("randonName"));

            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img0"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img1"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img2"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            randon.isPlay = true;
        }
        else if (PlayerPrefs.GetInt("saveIndex") == 1 && PlayerPrefs.HasKey("Img0") && PlayerPrefs.HasKey("Img1") && PlayerPrefs.HasKey("Img2"))
        {
            PlayerPrefs.SetString("Img3", PlayerPrefs.GetString("randonName"));

            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img0"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img1"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img2"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            } 
            for (int i = 0; i < randon.spriteFull.Count; i++)
            {
                if (randon.spriteFull[i].name == PlayerPrefs.GetString("Img3"))
                {
                    randon.spriteFull.RemoveAt(i);
                    PlayerPrefs.SetInt("saveIndex", 2);
                    PlayerPrefs.Save();
                }
            }
            randon.isPlay = true;
        }
    }
}
