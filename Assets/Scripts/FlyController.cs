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


    public void Drag(ObstacleController o)
    {
        if (!o) return;
        dragged = o.GetComponentInChildren<Rigidbody>();
        dragged.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        dragged.drag = 0;
    }
    public void Drop()
    {
        if (!dragged) return;
        dragged.drag = 10;
        dragged.constraints = RigidbodyConstraints.FreezeAll;

        Transform txt = dragged.transform.Find("txtDrag");
        if (txt) txt.gameObject.SetActive(false);

        dragged = null;
    }

    // return if corpse should be spawned
    public bool handleDeath()
    {
        if (flyType == FlyType.DEFAULT)
        {
            Drop();
        }
        else if (flyType == FlyType.EXPLOSION)
        {
            SoundHandler.PlayClip("explosion");
            GameObject explosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            foreach(ObstacleController o in below) o.Destroy();
            return false;
        }
        return true;
    }


    private void FixedUpdate()
    {
        // if (dragged) dragged.velocity = rb.velocity;
        // Debug.Log(dragged);
        if (dragged) dragged.position += rb.velocity * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        ObstacleController o = other.GetComponentInParent <ObstacleController>();
        if (!o) return;
        below.Add(o);
        
        Transform txt = o.transform.Find("txtDrag");
        if (txt) txt.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ObstacleController o = other.GetComponent<ObstacleController>();
        if (!o) return;
        below.Remove(o);

        if (dragged && o.gameObject == dragged.gameObject) Drop();
        Transform txt = o.transform.Find("txtDrag");
        if (txt) txt.gameObject.SetActive(false);
    }
}
