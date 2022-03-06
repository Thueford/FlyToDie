using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schnabel : MonoBehaviour
{
    public Vector3 mt;
    public Vector3 rt;
    public float speed = 3f;
    public bool kill = false;
    float epsilon;

    // Start is called before the first frame update
    void Start()
    {
        mt = GameController.self.GetPlayer().transform.position - new Vector3(0, 0, 2);
        rt = transform.position;
        epsilon = speed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        rt = Camera.main.transform.position - new Vector3(0,0,15);
        mt = GameController.self.GetPlayer().transform.position - new Vector3(0, 0, 2);

        float dist = Vector3.Distance(transform.position, mt);
        
        if (dist > epsilon && kill) moveTo(mt, speed);
        else if (dist < epsilon && kill)
        {
            GameController.self.GetPlayer().GetComponent<KillController>().Die(false);
            kill = false;
        }
        else moveTo(rt, speed);
    }

    void moveTo(Vector3 pos, float speed)
    {
        Vector3 delta = pos - transform.position;
        if (delta.sqrMagnitude > epsilon*epsilon)
        transform.position += delta.normalized * speed * Time.fixedDeltaTime;
    }
}
