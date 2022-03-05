using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [Tooltip("Duration")]
    public float duration = 180;
    public float offsetAngle = 30;

    private void Update()
    {
        transform.forward = Quaternion.Euler(-360*Time.time/duration - offsetAngle, 0, 0) * new Vector3(0, 0, -1);
    }
}
