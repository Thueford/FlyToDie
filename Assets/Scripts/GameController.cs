using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { FLY, MAGGOT }
public enum FlyType { DEFAULT, FIRE, ICE, BOMB }

public class GameController : MonoBehaviour
{
    public PlayerType playerType = PlayerType.FLY;
    public FlyType flyType = FlyType.DEFAULT;
}
