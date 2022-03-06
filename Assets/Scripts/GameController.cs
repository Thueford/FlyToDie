using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { FLY, MAGGOT }
public enum FlyType { DEFAULT, FIRE, ICE, BOMB }

public class GameController : MonoBehaviour
{
    public PlayerType playerType = PlayerType.FLY;
    public FlyType flyType = FlyType.DEFAULT;

    public PlayerMovement[] players;

    public static GameController self;
    public LevelController curLvl;
    

    public GameObject flyIcon;
    public GameObject maggotIcon;

    private void Awake()
    {
        self = this;
    }

    void Update()
    {
        if (KeyHandler.ReadMaggotSwitch())
            TogglePlayerType();
    }

    internal void SetLevel(LevelController level)
    {
        curLvl = level;
    }

    public void TogglePlayerType()
    {
        if (playerType == PlayerType.FLY)
        {
            SetPlayerType(PlayerType.MAGGOT);
            IconController.selectIcon(maggotIcon);
            IconController.unselectIcon(flyIcon);
        }
        else
        {
            SetPlayerType(PlayerType.FLY);
            IconController.selectIcon(flyIcon);
            IconController.unselectIcon(maggotIcon);
        }
    }

    public void SetPlayerType(PlayerType type)
    {
        playerType = type;
        foreach (PlayerMovement pm in players) pm.enabled = false;
        players[(int)type].enabled = true;
        CamController.self.target = players[(int)type].gameObject;
    }

    public PlayerMovement GetPlayer(PlayerType type)
    {
        return players[(int)type];
    }
}
