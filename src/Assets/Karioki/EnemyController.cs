using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private NavMeshAgent Agent;

    public float MoveSpeed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = MoveSpeed;
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
    }
}
