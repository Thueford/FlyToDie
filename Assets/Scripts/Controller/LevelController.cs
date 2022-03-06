using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Vector3 startPosFly, startPosMaggot;

    // Start is called before the first frame update
    void Start()
    {
        GameController.self.SetLevel(this);
    }
}
