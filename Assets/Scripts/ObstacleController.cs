using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public bool destroyable = false;
    public bool moveable = false;
    public bool pushable = false;
    public bool enableCheckpoints = true;
    public bool hidden = false;

    public float speed = 1.0f;

    // route where to go 
    public List<Vector3> checkpoints = new List<Vector3>();
    private int checkpointCounter = 0;

    private Renderer renderer;
    

    private void Awake()
    {
        this.renderer = gameObject.GetComponent<Renderer>();
        if (this.hidden) {
            this.Hide();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
