using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private Transform playerView;
    private static int currentSkinCounter = 47;

    private void Start()
    {
        SetSkin(currentSkinCounter);
    }

    public void ChangeToNext()
    {
        currentSkinCounter++;
        
        if (currentSkinCounter >= playerView.childCount)
        {
            SetSkin(0);
            currentSkinCounter = 0;
        }
        else
        {
            SetSkin(currentSkinCounter);
        }
    }

    public void ChangeToPrevious()
    {
        currentSkinCounter--;
        
        if (currentSkinCounter < 0)
        {
            SetSkin(playerView.childCount - 1);
            currentSkinCounter = playerView.childCount - 1;
        }
        else
        {
            SetSkin(currentSkinCounter);
        }
    }

    private void OffAllSkins()
    {
        for (int i = 0; i < playerView.childCount; i++)
        {
            playerView.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetSkin(int number)
    {
        OffAllSkins();
        for (int i = 0; i < playerView.childCount; i++)
        {
            playerView.GetChild(number).gameObject.SetActive(true);
        }
    }
}
