using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MIssileLauncherEnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject Player, Missile, LeftLauncher, RightLauncher;
    private NavMeshAgent Agent;
    [SerializeField] private float SpawnIntarval = 3f;
    [SerializeField] private float MoveSpeed = 3.5f;
    [SerializeField] private float Distance = 12f;

    private float TimeCount = 0;
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
        if (Agent.remainingDistance < Distance)
        {
            TimeCount += Time.deltaTime;
            Agent.speed = 0f;

            // ƒ^[ƒQƒbƒg‚Ì•ûŒü‚Ö‚Ì‰ñ“]
            Vector3 direction = Player.transform.position - transform.position;
            direction.y = 0.0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.3f);

            if (TimeCount > SpawnIntarval)
            {
                TimeCount = 0f;
                GameObject.Instantiate(Missile, LeftLauncher.transform.position, Quaternion.identity);
                GameObject.Instantiate(Missile, RightLauncher.transform.position, Quaternion.identity);
            }
        }
        else
        {
            TimeCount = 0f;
            Agent.speed = MoveSpeed;
        }
    }
}
