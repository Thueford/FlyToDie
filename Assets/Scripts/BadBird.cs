using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBird : MonoBehaviour
{
    public List<Vector3> checkpoints = new List<Vector3>();
    private int checkpointCounter = 0;

    public float speed = 1.0f;

    [ReadOnly]
    public bool sees_you = false;
    public int time_to_kill = 2;

    private SpriteRenderer birdie;

    //public GameObject schnabelPrefab;

    private void Awake()
    {
        birdie = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        moveToNextCheckpoint();
        //setRotation();
        //setColor();
    }

    private void setColor()
    {
        if (sees_you)
        {
            birdie.color = new Color(birdie.color.r - 10, birdie.color.g - 10, birdie.color.b - 10); 
        } else
        {
            birdie.color = new Color(birdie.color.r + 1, birdie.color.g + 1, birdie.color.b + 1);
        }
    }

    private void setRotation()
    {
        //Vector3.Angle(transform.position, checkpoints[checkpointCounter])
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y , transform.rotation.z));
    }

    void moveToNextCheckpoint()
    {
        if (checkpoints.Count > checkpointCounter)
        {
            if (Vector3.Distance(transform.position, checkpoints[checkpointCounter]) <= Time.fixedDeltaTime * speed)
            {
                Vector3 dcheckpoint;
                if (checkpointCounter + 1 >= checkpoints.Count)
                {
                    dcheckpoint = checkpoints[0] - checkpoints[checkpointCounter];
                    checkpointCounter = 0;
                    
                }
                else
                {
                    checkpointCounter++;
                    dcheckpoint = checkpoints[checkpointCounter - 1] - checkpoints[checkpointCounter];
                }

                float ang = Vector3.Angle(transform.forward, new Vector3(dcheckpoint.x, 0,dcheckpoint.z).normalized);
                //Debug.Log(transform.forward);
                //Debug.Log(dcheckpoint.normalized);
                //Debug.Log(ang);
                transform.eulerAngles = new Vector3(0, ang + transform.eulerAngles.y, 0);
            }
            moveTo(checkpoints[checkpointCounter], speed);
        }
    }

    void moveTo(Vector3 pos, float speed)
    {
        Vector3 delta = pos - transform.position;
        delta.y = 0;
        transform.position += delta.normalized * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm && other.CompareTag("Player") && pm.visible)
        {
            Debug.Log("Birdie sees you");
            sees_you = true;
            StartCoroutine(kill(pm.type));
        }
    }

    IEnumerator kill(PlayerType pt)
    {
        yield return new WaitForSeconds(time_to_kill);// Wait a bit
        if (sees_you)
        {
            Debug.Log("You die now");
            if (pt == GameController.self.playerType)
                Camera.main.GetComponentInChildren<Schnabel>().kill = true;
        }
    }

    

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            sees_you = false;
    }

}
