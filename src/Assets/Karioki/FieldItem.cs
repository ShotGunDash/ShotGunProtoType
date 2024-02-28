using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float MoveDistance = 8f;
    [SerializeField] private float MoveBrake= 0.4f;
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

    // Update is called once per frame
    void Update()
    {
        float IsDistance = Vector3.Distance(Player.transform.position, this.transform.position);

        if (IsDistance < GetDistance)
        {
            IsGet();
        }
        else if (IsDistance < MoveDistance) 
        {
            float disSpeed = (MoveDistance - IsDistance) * MoveBrake;
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
