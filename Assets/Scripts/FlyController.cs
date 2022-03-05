using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    public List<ObstacleController> below = new List<ObstacleController>();
    [ReadOnly]
    public Rigidbody rb, dragged;
    public GameObject DeadPrefab;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        if (KeyHandler.ReadKillInput())
            Die();
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

    public void Die()
    {
        Debug.Log("DIE MTFK DIEEE!!");
        Drop();
        GameObject dead = Instantiate(DeadPrefab, transform.position, transform.rotation);
        dead.GetComponent<Rigidbody>().AddForce(150 * Vector3.up);
        dead.GetComponent<Rigidbody>().rotation = Random.rotation;
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        KeyHandler.enableMovement = false;

        yield return new WaitForSeconds(3f);

        transform.position = GameController.self.curLvl.startPos;
        transform.GetChild(0).gameObject.SetActive(true);
        KeyHandler.enableMovement = true;
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
