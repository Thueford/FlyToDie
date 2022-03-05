using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    public enum EffectType { EXPLOSION, FREEZE }
    public EffectType effectType = EffectType.EXPLOSION;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.GetComponent<ObstacleController>().destroy();
        }
    }
}
