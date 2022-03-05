using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public bool destroyable = false;
    public bool moveable = false;
    public bool pushable = false;
    public bool enableCheckpoints = false;
    public bool hidden = false;

    public float speed = 1.0f;

    public enum ObstacleStatus { NOTDESTROYABLE, DESTROYABLE, DESTROYED}
    public ObstacleStatus obstacleStatus = ObstacleStatus.NOTDESTROYABLE;


    public GameObject notDestroyableObject;
    public GameObject destroyableObject;
    public GameObject destroyedObject;


    private GameObject[] gameObjects;
    private GameObject activeObject;

    // route where to go 
    public List<Vector3> checkpoints = new List<Vector3>();
    private int checkpointCounter = 0;

    private Renderer renderer;

    private void Awake()
    {
        //if (!notDestroyableObject) notDestroyableObject = gameObject.GetComponent<Sp>
        gameObjects = new GameObject[3] { notDestroyableObject, destroyableObject, destroyedObject};
        foreach (GameObject g in gameObjects)
        {
            if (g != null)
            {
                g.SetActive(false);
            }
        }

        this.renderer = gameObject.GetComponent<Renderer>();
        if (this.hidden) {
            this.Hide();
        }

        if (this.destroyable)
        {
            activeObject = destroyableObject;

        } else
        {
            activeObject = notDestroyableObject;
        }

        if (activeObject != null) activeObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponentInChildren<BoxCollider>().isTrigger)
        {
            Debug.Log("Trigger");
        }
    }

    private void FixedUpdate()
    {
        //handle movement
        if (this.enableCheckpoints)
        {
            this.moveToNextCheckpoint();
        }
    }

    void moveToNextCheckpoint()
    {
        if (this.checkpoints.Count > this.checkpointCounter )
        {
            if (Vector3.Distance(transform.position, this.checkpoints[this.checkpointCounter]) <= Time.fixedDeltaTime * speed)
            {
                if (this.checkpointCounter + 1 >= this.checkpoints.Count)
                {
                    this.checkpointCounter = 0;
                } else
                {
                    this.checkpointCounter++;
                }
            }
            this.moveTo(this.checkpoints[this.checkpointCounter], this.speed);
        }
    }

    void moveTo(Vector3 pos, float speed)
    {
        Vector3 delta = transform.position - pos;

        transform.position -= delta.normalized * speed * Time.fixedDeltaTime;
    }

    void Hide()
    {
        this.renderer.enabled = false;
    }

    void Show()
    {
        this.renderer.enabled = true;
    }

    // if you can drag the object
    bool is_dragable()
    {
        if (this.moveable && this.pushable && !this.hidden)
        {
            return true;
        }
        return false;
    }

    public void destroy()
    {
        if (this.destroyable)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                
            } else
            {
                Debug.LogError("Forgot to add a destroyed object to this");
            }
        }
    }
}
