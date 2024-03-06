using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FieldItem : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float MoveDistance = 8f;
    [SerializeField] private float MoveSpeed= 2f;
    [SerializeField] private float GetDistance = 0.1f;
    private Rigidbody rb;
    private Collider co;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        co = GetComponent<Collider>();
    }
    void FromCamera()
    {
        Vector3 dir = Camera.main.transform.position - this.transform.position;
        dir.x = 0; dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180, 0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        FromCamera();
        float IsDistance = Vector3.Distance(Player.transform.position, this.transform.position);
      
        if (IsDistance < GetDistance)
        {
            IsGet();
        }
        else if (IsDistance < MoveDistance) 
        {
            float disSpeed = (MoveDistance - IsDistance+ MoveSpeed) * MoveSpeed;
            rb.AddForce((Player.transform.position - this.transform.position + Vector3.up).normalized * disSpeed);
        }
        else
        {
            rb.AddForce(Vector3.down);
        }
    }
    void IsGet()
    {
        Destroy(this.gameObject);
    }
}
