using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController self;
    public static bool allowMoving = true;

    [Range(1, 50)]
    public float lazyness = 10;
    public GameObject target;
    public Vector3 offset = new Vector3(0, 7, -3);

    private void Awake() => self = this;

    private void FixedUpdate()
    {
        if (!target) return;

        Vector3 dir = target.transform.position - transform.position + offset;
        dir.y = 0;
        Vector3 pos = transform.position += dir / lazyness;

        transform.position = pos;
    }
}
