using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider coll;

    [Range(0, 100)] public float moveMult = 20f;
    [Range(0, 100)] public float maxHSpeed = 10;
    private const int angleSpeed = 360;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    public Vector3 vec = new Vector3(1, 0, 0);
    public float ang;

    void FixedUpdate()
    {
        Vector3 moveForce = KeyHandler.ReadDirInput() * moveMult;
        if (moveForce.x != 0 || moveForce.z != 0)
        {
            // rb.AddForce(transform.forward);
            SoundHandler.StartWalk();

            ang = transform.forward.x * moveForce.z - transform.forward.z * moveForce.x;
            // if (Mathf.Abs(ang) < 0.001) ang = Vector3.Dot(transform.forward, moveForce.normalized);
            if (Mathf.Abs(ang) > 5) transform.Rotate(0, -Mathf.Sign(ang) * Time.fixedDeltaTime * angleSpeed, 0);
            else transform.forward = moveForce;
            rb.AddForce(transform.forward * moveMult);
        }
        else
            SoundHandler.StopWalk();
    }
}