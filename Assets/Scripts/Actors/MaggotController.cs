using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotController : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 lpos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    bool next;
    private void Update()
    {
        next = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (lpos != Vector3.zero) rb.position += collision.transform.position - lpos;
            lpos = collision.transform.position;
        }
        else lpos = Vector3.zero;
    }
}
