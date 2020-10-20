using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private GameObject skins = null;
    [SerializeField] private SkinSupport skinSupport = null;
    [SerializeField] private Color[] colors = null;
    [SerializeField] private Image player = null;
    [SerializeField] private Color playerColor = Color.white;
    [SerializeField] private int playerType = 0;
    [SerializeField] private Image[] images = null;

    public void DisplaySkinStore()
    {
        skins.SetActive(!skins.activeSelf);
    }

    public void SetColorA()
    {
        playerType = 0;
        playerColor = colors[0];
        player.color = colors[0];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
    }

    public void SetColorB()
    {
        playerType = 1;
        playerColor = colors[1];
        player.color = colors[1];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
    }
    public void SetColorC()
    {
        playerType = 2;
        playerColor = colors[2];
        player.color = colors[2];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
    }
    public void SetColorD()
    {
        playerType = 3;
        playerColor = colors[3];
        player.color = colors[3];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
    }

    public void InitInTitleScene()
    {
        playerColor = skinSupport.GetPlayerColor();
        playerType = skinSupport.GetPlayerType();
        player.color = playerColor;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = colors[i];
        }
    }

    public void SetSkinSupport(SkinSupport skin_support)
    {
        skinSupport = skin_support;
    }
}
