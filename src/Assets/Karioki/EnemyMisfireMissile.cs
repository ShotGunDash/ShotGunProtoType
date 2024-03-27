using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMisfireMissile : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float DestroyIntarval = 15f;
    [SerializeField] private float lockOnIntarval = 5f;
    [SerializeField] private float MissilePower = 25f;
    [SerializeField] private float MissileSpeed = 3f;
    [SerializeField] private float RotateSpeed = 0.001f;
    [SerializeField] private float RandomRange = 3f;
    [SerializeField] private float Missile_UpForse = 3f;
    [SerializeField] private float MisfireMissilePower = 50f;
    private Rigidbody rb;
    private float DestroyTime = 0;
    private bool LockOn;
    private Vector3 LockOnPos;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        LockOn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Stage"))
        {
            rb.isKinematic = true;
        }
    }

    private Vector3 PosSearch()
    {
        float randomValue_X = Random.Range(-RandomRange, RandomRange) + Player.transform.position.x - transform.position.x;
        float randomValue_Z = Random.Range(-RandomRange, RandomRange) + Player.transform.position.z - transform.position.z;
        return new Vector3(randomValue_X, Player.transform.position.y - transform.position.y, randomValue_Z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        DestroyTime += Time.deltaTime;
        if (DestroyTime > DestroyIntarval)
        {
            Destroy(this.gameObject);
        }
        else if (DestroyTime > lockOnIntarval)
        {
            if (LockOn)
            {
                rb.velocity = Vector3.zero;
                
                LockOn = !LockOn;
                LockOnPos = PosSearch();
                rb.AddForce(LockOnPos.normalized * MisfireMissilePower,ForceMode.Impulse);
                transform.rotation = Quaternion.LookRotation(LockOnPos, Vector3.up) * Quaternion.AngleAxis(90, Vector3.right);
            }
            else
            {
                if(transform.position.y < -5f)
                    Destroy(this.gameObject);
                //rb.AddForce(LockOnPos * MisfireMissilePower);
                //Quaternion rotation = Quaternion.LookRotation(LockOnPos, Vector3.up) * Quaternion.AngleAxis(90, Vector3.right);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotateSpeed * 2);
            }
        }
        else if (DestroyTime < lockOnIntarval)
        {
            Vector3 look_Player = new Vector3(Player.transform.position.x - transform.position.x,
               0f, Player.transform.position.z - transform.position.z).normalized;

            Quaternion rotation = Quaternion.LookRotation(look_Player, Vector3.up) * Quaternion.AngleAxis(90, Vector3.right);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotateSpeed);

            look_Player = new Vector3(look_Player.x, Missile_UpForse, look_Player.z);
            rb.AddForce(look_Player * MissilePower);

            //transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}
