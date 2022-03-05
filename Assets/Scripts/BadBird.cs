using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBird : MonoBehaviour
{
    public List<Vector3> checkpoints = new List<Vector3>();
    private int checkpointCounter = 0;

    public float speed = 1.0f;

    public bool sees_you = false;
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
        this.moveToNextCheckpoint();
        //setRotation();
    }

    private void setRotation()
    {
        //Vector3.Angle(transform.position, checkpoints[checkpointCounter])
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y , transform.rotation.z));
    }

    void moveToNextCheckpoint()
    {
        if (this.checkpoints.Count > this.checkpointCounter)
        {
            if (Vector3.Distance(transform.position, this.checkpoints[this.checkpointCounter]) <= Time.fixedDeltaTime * speed)
            {
                if (this.checkpointCounter + 1 >= this.checkpoints.Count)
                {
                    this.checkpointCounter = 0;
                }
                else
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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.tag == "Player")
        {
            sees_you = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sees_you = false;
        }
    }

}
