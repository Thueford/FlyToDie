using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerType.MAGGOT == other.gameObject.GetComponentInParent<PlayerMovement>().type)
        {
            Debug.Log("YOu won");
        }
    }
}
