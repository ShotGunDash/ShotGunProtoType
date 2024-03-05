using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   [SerializeField] private GameObject Player;
   [SerializeField] private float DestroyIntarval = 10f;
   [SerializeField] private float BulletPower = 2000;
    private float DestroyTime = 0;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
       
        Vector3 Forward = Player.transform.position - transform.position;
        Forward.Normalize();
        Quaternion look = Quaternion.LookRotation(Forward);
        transform.rotation = look * Quaternion.Euler(90, 0, 0);
        
        rb.AddForce(Forward * BulletPower);
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DestroyTime += Time.deltaTime;

        if (DestroyTime > DestroyIntarval)
        {
            DestroyTime = 0f;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
