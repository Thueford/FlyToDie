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

    private void Awake() => self = this;

    private void Start() => SetPlayerType(PlayerType.FLY);

    void Update()
    {
        if (KeyHandler.ReadMaggotSwitch())
            TogglePlayerType();
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
}
