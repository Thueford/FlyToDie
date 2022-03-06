using System;
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
    public bool visible = true;
    public bool birdie_sees_you = false;
    private Camera cam;
    [ReadOnly]
    public PlayerType type;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        cam = Camera.main;
    }

    public Vector3 vec = new Vector3(1, 0, 0);
    public float ang;

    void FixedUpdate()
    {
        Vector3 moveDir = KeyHandler.ReadDirInput().normalized;
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            // rb.AddForce(transform.forward);
            SoundHandler.StartWalk();

            ang = transform.forward.x * moveDir.z - transform.forward.z * moveDir.x;
            if (Mathf.Abs(ang) < 0.1)
            {
                ang = Vector3.Dot(transform.forward, moveDir.normalized);
                if (ang > 0) ang = 0;
            }
            if (Mathf.Abs(ang) > 0.1) transform.Rotate(0, -Mathf.Sign(ang) * Time.fixedDeltaTime * angleSpeed, 0);
            else transform.forward = moveDir;

            rb.AddForce(moveDir * moveMult);
        }
        else
            SoundHandler.StopWalk();

        float dist = Vector3.Distance(transform.position, cam.transform.position);
        LayerMask layerMask = LayerMask.GetMask("Player");
        layerMask = ~layerMask;
        bool hit = Physics.Raycast(cam.transform.position, -cam.GetComponent<CamController>().offset, dist, layerMask);
        visible = !hit;
    }

    internal void SetEnabled(bool state)
    {
        enabled = state;
        GetComponent<KillController>().enabled = state;
    }
}
