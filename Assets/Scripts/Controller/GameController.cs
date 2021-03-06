using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { FLY, MAGGOT }
public enum FlyType { DEFAULT, BOMB }

public class GameController : MonoBehaviour
{
    public PlayerType playerType = PlayerType.FLY;
    public FlyType flyType = FlyType.DEFAULT;

    public PlayerMovement[] players;
    public FlyController flyController;

    public static GameController self;
    public LevelController curLvl;

    public IconController icon;

    private void Awake() => self = this;

    private void Start() => SetPlayerType(PlayerType.FLY);

    void Update()
    {
        if (KeyHandler.ReadMaggotSwitch())
            TogglePlayerType();

        if (KeyHandler.ReadTypeSelect1())
        {
            Debug.Log("TOGGLE Default");
            ToggleFlyType(FlyType.DEFAULT);
        }
        else if (KeyHandler.ReadTypeSelect2())
        {
            Debug.Log("TOGGLE Bomb");
            ToggleFlyType(FlyType.BOMB);
        }

    }

    internal void SetLevel(LevelController level)
    {
        curLvl = level;
        int i = 0;
        foreach (PlayerMovement p in players)
        {
            p.GetComponent<KillController>().SetRespawnPos();
            p.type = (PlayerType)i++;
        }
    }

    public void TogglePlayerType()
    {
        if (playerType == PlayerType.FLY)
        {
            SoundHandler.PlayClip("changeToMade");
            SetPlayerType(PlayerType.MAGGOT);
        }
        else
        {
            SoundHandler.PlayClip("changeToFly");
            SetPlayerType(PlayerType.FLY);
        }
    }

    public void SetPlayerType(PlayerType type)
    {
        icon.SetIcon(type == PlayerType.FLY);
        playerType = type;
        PlayerMovement cur = players[(int)type];

        GetPlayer(PlayerType.FLY).GetComponent<FlyController>().Drop();
        CamController.self.target = cur.gameObject;

        foreach (PlayerMovement pm in players)
        {
            pm.SetEnabled(cur == pm);
            pm.GetComponent<Rigidbody>().constraints = cur != pm ? 
                RigidbodyConstraints.FreezeAll : RigidbodyConstraints.FreezeRotation;
        }
    }

    public PlayerMovement GetPlayer(PlayerType type) => players[(int)type];
    public PlayerMovement GetPlayer() => GetPlayer(playerType);

    public void ToggleFlyType(FlyType type)
    {
        flyType = type;
        switch(type)
        {
            case FlyType.DEFAULT:
                if(icon.setIconColor(FlyType.DEFAULT))
                {

                }
                else
                {
                    icon.enableIcon(FlyType.DEFAULT);       
                }
                break;
            case FlyType.BOMB:
                if (icon.setIconColor(FlyType.BOMB))
                {

                }
                else
                {
                    icon.enableIcon(FlyType.BOMB);
                }
                break;
        }
    }
}
