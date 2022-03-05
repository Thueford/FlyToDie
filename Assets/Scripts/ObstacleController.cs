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
    public bool destroyed = false;

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
            if (g != null) g.SetActive(false);

        this.renderer = gameObject.GetComponent<Renderer>();
        if (this.hidden) this.Hide();

        if (this.destroyable)
            activeObject = destroyableObject;
        else
        { 
            activeObject = notDestroyableObject;

            if (destroyed)
            {
                activeObject = destroyedObject;
            }
        }


        if (activeObject != null) activeObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //handle movement
        if (enableCheckpoints)
        {
            moveToNextCheckpoint();
        }
    }

    void moveToNextCheckpoint()
    {
        if (checkpoints.Count > checkpointCounter )
        {
            if (Vector3.Distance(transform.position, checkpoints[checkpointCounter]) <= Time.fixedDeltaTime * speed)
            {
                if (checkpointCounter + 1 >= checkpoints.Count)
                    checkpointCounter = 0;
                else checkpointCounter++;
            }
            moveTo(checkpoints[checkpointCounter], speed);
        }
    }

    void moveTo(Vector3 pos, float speed)
    {
        Vector3 delta = transform.position - pos;

        transform.position -= delta.normalized * speed * Time.fixedDeltaTime;
    }

    void Hide()
    {
        renderer.enabled = false;
    }

    void Show()
    {
        renderer.enabled = true;
    }

    // if you can drag the object
    bool is_dragable()
    {
        return moveable && pushable && !hidden;
    }

    public void destroy()
    {
        if (this.destroyable)
        {
            if (destroyableObject != null)
            {

                this.activeObject.SetActive(false);
                this.activeObject = destroyedObject;
                if (this.activeObject) this.activeObject.SetActive(true);

            } else
            {
                Debug.LogError("Forgot to add a destroyed object to this");
            }
            transform.FindChild("ColliderContainer").gameObject.SetActive(false);
        }
    }
}
