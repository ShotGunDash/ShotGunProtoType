using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class EnemyMediumMissile : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float DestroyIntarval = 15f;
    [SerializeField] private float lockOnIntarval = 5f;
    [SerializeField] private float LockOnMissilePower = 25f;
    [SerializeField] private float HomingMissilePower = 40f;
    //[SerializeField] private float MissileSpeed = 3f;
    [SerializeField] private float RotateSpeed = 0.001f;
    [SerializeField] private float LimitSpeed = 10.0f;
    private Rigidbody rb;
    private float DestroyTime = 0;
    private bool Brake;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        Brake = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           Destroy(this.gameObject);
        }
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
            if (Brake)
            {
                rb.velocity = Vector3.zero;
                Brake = !Brake;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                Vector3 velocity = Player.transform.position - transform.position;
                Vector3 forward = velocity.normalized;
                forward.y *= 10f;
                rb.AddForce(forward * HomingMissilePower);
                Quaternion rotation = Quaternion.LookRotation(velocity, Vector3.up) * Quaternion.AngleAxis(90, Vector3.right);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotateSpeed * 2);
                if (rb.velocity.magnitude > LimitSpeed)
                {
                    rb.velocity = new Vector3(rb.velocity.x / 1.1f, rb.velocity.y, rb.velocity.z / 1.1f);
                }
                if(transform.position.y < 1.8f && rb.velocity.y < 0)
                { 
                    rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z); 
                }
            }
            /*
            if (Agent.enabled == false)
            {
                rb.isKinematic = true;
                Agent.updateRotation = false;
                Agent.enabled = true;
            }
            else
            {
                Vector3 look_Player = new Vector3(Player.transform.position.x - transform.position.x,
                0f, Player.transform.position.z - transform.position.z);

                transform.rotation = Quaternion.LookRotation(look_Player, Vector3.up) * Quaternion.Euler(90, 0, 90);

                Agent.destination = Player.transform.position;   
            }
            */
        }
        else if(DestroyTime < lockOnIntarval)
        {

            Vector3 look_Player = new Vector3(Player.transform.position.x - transform.position.x,
               0f, Player.transform.position.z - transform.position.z);

            rb.AddForce(look_Player.normalized * LockOnMissilePower);

            Quaternion rotation = Quaternion.LookRotation(look_Player, Vector3.up) * Quaternion.AngleAxis(90, Vector3.right);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotateSpeed);
        }
    }
}
