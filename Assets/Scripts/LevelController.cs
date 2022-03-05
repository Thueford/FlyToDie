using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = GameController.self.GetPlayer(PlayerType.FLY).transform.position;
        GameController.self.SetLevel(this);
    }
}
