using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CapsuleCollider triggerCollider;
    private Collider playerCollider;
    private bool moving = false;
    private Vector3 playerPos;

    private void Awake()
    {
        this.triggerCollider = this.gameObject.GetComponentInChildren<CapsuleCollider>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            playerPos = playerCollider.transform.position;
            Vector3 dplayerPos = transform.position - playerPos;
            Vector3 dVector = new Vector3(dplayerPos.x, 0, dplayerPos.z);
            transform.forward = dVector;

        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider player)
    {
        //Debug.Log(player.name);
        playerCollider = player;
        moving = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Player left");
        moving = false;
    }
}
