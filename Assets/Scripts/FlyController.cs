using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    public List<ObstacleController> below = new List<ObstacleController>();
    [ReadOnly]
    public Rigidbody rb, dragged;
    public GameObject DeadPrefab;
    public GameObject ExplosionPrefab;

    public enum FlyType
    {
        DEFAULT, EXPLOSION, ICE
    }

    public FlyType flyType = FlyType.DEFAULT;

    public Dictionary<FlyType, int>FlyCounter;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        FlyCounter = new Dictionary<FlyType, int>();
    }

    private void Update()
    {
        if (KeyHandler.ReadDragInput())
        {
            if (!dragged)
                Drag(below.Find(o => o.pushable));
            else
                Drop();
        }
    }


    private void Drag(ObstacleController o)
    {
        dragged = o.GetComponent<Rigidbody>();
        dragged.drag = 0;
    }
    private void Drop()
    {
        if (dragged) dragged.drag = 10;
        dragged = null;
    }

    private void FixedUpdate()
    {
        if (dragged) dragged.velocity = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        ObstacleController o = other.GetComponent<ObstacleController>();
        if (o) below.Add(o);
    }

    private void OnTriggerExit(Collider other)
    {
        ObstacleController o = other.GetComponent<ObstacleController>();
        if (dragged && o.gameObject == dragged.gameObject) Drop();
        if (o) below.Remove(o);
    }
}
