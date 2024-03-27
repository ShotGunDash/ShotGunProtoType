using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeEnemyController : MonoBehaviour
{
    [SerializeField]private GameObject Player;
    [SerializeField] private float ChargeIntarval = 2f;
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float Distance = 12f;
    [SerializeField] private float ChargePower = 100f;
    [SerializeField] private float StopIntarval = 2f;
    private NavMeshAgent Agent;
    private float TimeCount = 0;
    private Rigidbody rb;
    private Vector3 ChargeForward;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Agent.speed = MoveSpeed;
    }

    float GetDistance()
    {
        Vector3 player = new Vector3(Player.transform.position.x, 0f, Player.transform.position.z);
        Vector3 thisEnemy = new Vector3(this.transform.position.x,0f, this.transform.position.z);
        return Vector3.Distance(player, thisEnemy);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Agent.enabled == true)
        {
            if (Agent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
               Destroy(this.gameObject);
            }

            else
            Agent.destination = Player.transform.position;
        }

        float PlaneDistance = GetDistance();
        //if (Agent.remainingDistance < Distance)
        if (PlaneDistance < Distance)
        {
            TimeCount += Time.deltaTime;
            Agent.speed = 0f;
           
            Vector3 direction = Player.transform.position - transform.position;
            

            // ƒ^[ƒQƒbƒg‚Ì•ûŒü‚Ö‚Ì‰ñ“]
            if (TimeCount < ChargeIntarval)
            {
                ChargeForward = direction.normalized;
                direction.y = 0.0f;
                Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.3f);
            }
            else if (TimeCount > ChargeIntarval + StopIntarval)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                Agent.enabled = true;

                TimeCount = 0f;
            }
            else if (TimeCount > ChargeIntarval)
            {
                Agent.enabled = false;
                rb.isKinematic = false;

                rb.AddForce(ChargeForward * ChargePower);
            }
           
        }
        else
        {
            TimeCount = 0f;
            Agent.speed = MoveSpeed;
            if(transform.position.y < -10f)
            {
                transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
                rb.isKinematic = true;
                Agent.enabled = true;
            }
        }
    }
}
