using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schnabel : MonoBehaviour
{

    private Vector3 mt;
    private Vector3 rt;
    public float speed = 2f;
    public bool kill = false;
    // Start is called before the first frame update
    void Start()
    {
        mt = GameController.self.GetPlayer(GameController.self.playerType).transform.position - new Vector3(0, 0, 2);
        rt = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rt = Camera.main.transform.position - new Vector3(0,0,10);
        mt = GameController.self.GetPlayer(GameController.self.playerType).transform.position - new Vector3(0, 0, 2);
        if (Vector3.Distance(transform.position, mt)>1 && kill)
        {
            moveTo(mt, speed);
        } else if (Vector3.Distance(transform.position,mt)<0.9 && kill)
        {
            kill = false;
        } else
        {
            moveTo(rt, speed);
        }
    }

    void moveTo(Vector3 pos, float speed)
    {
        Vector3 delta = transform.position - pos;

        transform.position -= delta.normalized * speed * Time.fixedDeltaTime;
    }
}
