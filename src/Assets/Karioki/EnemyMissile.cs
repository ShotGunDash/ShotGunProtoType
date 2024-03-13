using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Target;
    [SerializeField] private float DestroyIntarval = 15f;
    [SerializeField] private float lockOnIntarval = 5f;
    [SerializeField] private float MissilePower = 200;
    [SerializeField] private float RandomRange = 3f;
    private Rigidbody rb;
    private float DestroyTime = 0;
    private Vector3 TargetPos = Vector3.up;
    private GameObject SetTarget;
    private bool TargetActive;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        TargetActive = false;
    }

    private Vector3 PosSearch()
    {
        float randomValue_X = Random.Range(-RandomRange, RandomRange) + Player.transform.position.x;
        float randomValue_Z = Random.Range(-RandomRange, RandomRange) + Player.transform.position.z;
        return new Vector3(randomValue_X, TargetPos.y, randomValue_Z);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DestroyTime += Time.deltaTime;
        if (DestroyTime < lockOnIntarval)
        {
            rb.AddForce(Vector3.up * MissilePower);
        }
        else if (DestroyTime > DestroyIntarval) 
        {
            SameDestroy();
        }
        else if(DestroyTime > lockOnIntarval) 
        {
            if (TargetActive == false)
            {
                TargetActive = true;
                TargetPos = PosSearch();
                SetTarget = GameObject.Instantiate(Target, TargetPos, Quaternion.Euler(90, 0, 0));
                transform.position = new Vector3(SetTarget.transform.position.x,
                    transform.position.y,SetTarget.transform.position.z);
            }

            else 
            {
                transform.rotation = Quaternion.LookRotation(SetTarget.transform.position);
                //Target.transform.position = TargetPos;
                rb.AddForce((SetTarget.transform.forward.normalized)* MissilePower); 
            }    
        }     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SameDestroy();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Stage"))
        {
            SameDestroy();
        }
    }

    private void SameDestroy()
    {
        if(SetTarget != null)
            Destroy(SetTarget);

        Destroy(this.gameObject);
    }
}
