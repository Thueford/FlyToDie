using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillController : MonoBehaviour
{
    [ReadOnly]
    public Rigidbody rb;
    public GameObject DeadPrefab;
    public Vector3 respawnPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (KeyHandler.ReadKillInput())
            Die();
        Debug.DrawLine(respawnPos-new Vector3(0.1f, 0.1f,0), respawnPos + new Vector3(0.1f, 0.1f, 0));
        Debug.DrawLine(respawnPos - new Vector3(0, 0.1f, 0.1f), respawnPos + new Vector3(0, 0.1f, 0.1f));
    }

    public void SetRespawnPos() => respawnPos = transform.position;

    public void Die(bool corpse = true)
    {
        // if fly
        SoundHandler.PlayClip("die");
        FlyController fly = gameObject.GetComponent<FlyController>();
        if (fly) corpse = corpse && fly.handleDeath();

        Debug.Log("DIE MTFK DIEEE!!");
        if (corpse)
        {
            GameObject dead = Instantiate(DeadPrefab, transform.position, transform.rotation);
            dead.GetComponent<Rigidbody>().AddForce(200 * Vector3.up);
            dead.GetComponent<Rigidbody>().rotation = Random.rotation;
        }
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        KeyHandler.enableMovement = false;

        yield return new WaitForSeconds(3f);

        SoundHandler.PlayClip("changeToFly");
        transform.position = respawnPos;
        transform.GetChild(0).gameObject.SetActive(true);
        KeyHandler.enableMovement = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillLevel"))
        {
            Debug.Log("Drowned");
            Die();
        }
    }
}
