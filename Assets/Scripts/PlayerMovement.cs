using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement self;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider coll;

    [Range(0, 100)] public float moveMult = 20f;
    [Range(0, 100)] public float maxHSpeed = 10;

    private void Awake()
    {
        self = this;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    public Vector3 vec = new Vector3(1, 0, 0);

    void FixedUpdate()
    {
        Vector3 moveForce = KeyHandler.ReadDirInput() * moveMult;
        if (moveForce.x != 0 || moveForce.z != 0)
        {
            rb.AddForce(moveForce);
            SoundHandler.StartWalk();
            if (rb.velocity.sqrMagnitude > 0.001) {
                transform.forward = rb.velocity;
            }
        }
        else
        {
            SoundHandler.StopWalk();
        }
    }
}
