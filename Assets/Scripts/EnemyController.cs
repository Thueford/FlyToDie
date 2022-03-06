using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CapsuleCollider triggerCollider;
    private bool targetFound = false;

    public KillController targetPlayer;
    public bool moving = false;
    private Vector3 playerPos;
    public float moveMult = 20f;

    private Vector3 dplayerPos;

    public float pov_width = 20f;
    public float viewDistance = 20f;
    public bool enablePov = true;
    public bool eating = false;

    [HideInInspector] public Rigidbody rb;
    private const int angleSpeed = 130;

    public float fov = 90f;

    private Mesh mesh;

    private void Awake()
    {
        triggerCollider = transform.gameObject.GetComponentInChildren<CapsuleCollider>();
        triggerCollider.radius = viewDistance;
        mesh = new Mesh();
    }


    // Start is called before the first frame update
    void Start()
    {
        mesh = transform.Find("Pov").GetComponentInChildren<MeshFilter>().mesh = mesh;
        if (enablePov) drawPov();
    }

    private void FixedUpdate()
    {
        // bei eintritt in trigger bewegt sich enemy solange bis player tot ist auch wenn nicht mehr in trigger
        

        if (moving)
        {
            playerPos = targetPlayer.transform.position;
            dplayerPos = transform.position - playerPos;


            Transform testTransform = transform;

            float angl = Vector3.Angle(transform.forward, new Vector3(dplayerPos.x, 0, dplayerPos.z));
            // Debug.Log(angl);
            if (angl <= fov/2 || targetFound)
            {
                targetFound = true;
                Vector3 dVector = new Vector3(dplayerPos.x, 0, dplayerPos.z);
                Vector3 moveForce = dVector;

                float ang = transform.forward.x * moveForce.z - transform.forward.z * moveForce.x;
                if (Mathf.Abs(ang) > 0.5) transform.Rotate(0, -Mathf.Sign(ang) * Time.fixedDeltaTime * angleSpeed, 0);
                else transform.forward = moveForce;
            }

            if (angl < 2 && !eating)
            {
                eating = true;
                Transform toung = transform.Find("zungeCont");
                toung.GetComponent<Animator>().Play("ExtendToung");
                StartCoroutine(KillPlayer());
            }
        }
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(0.17f);
        if (targetPlayer) targetPlayer.Die(false);
        moving = false;
        targetFound = false;
    }

    private void OnTriggerEnter(Collider player)
    {
        Debug.Log(player.name);
        Debug.Log(player);
        KillController obj = player.gameObject.GetComponent<KillController>();
        if (obj)
        {
            targetPlayer = obj;
            moving = true;
            eating = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player left");
        //moving = false;
    }

    public Vector3 getPov() => transform.rotation.eulerAngles;

    void drawPov()
    {
        Vector3 vzero = Vector3.zero;

        int rayCount = 10;
        float angle = -45f;
        float angleIncrease = fov / rayCount;


        Vector3[] vectors = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vectors.Length];

        int[] triangles = new int[rayCount * 3];

        vectors[0] = vzero;

        int vindex = 1;
        int tindex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            float angleRad = angle * (Mathf.PI / 180f);

            Vector3 vert = vzero + new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad)) * viewDistance;
            vectors[vindex] = vert;


            if (i > 0)
            {
                triangles[tindex + 0] = 0;
                triangles[tindex + 1] = vindex - 1;
                triangles[tindex + 2] = vindex;

                tindex += 3;
            }

            vindex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vectors;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
