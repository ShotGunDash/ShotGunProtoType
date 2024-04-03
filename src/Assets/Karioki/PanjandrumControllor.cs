using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;
public class PanjandrumControllor : MonoBehaviour
{
    enum Action
    {
        Rotate_around,
        Charge_dash,
        Rapid_missile,
        Medium_missile,
        Power_charge,
        Misfire_missile,
    }
    [SerializeField] private GameObject Player,LeftLauncher, RightLauncher;
    [SerializeField] private GameObject Missile_S, Missile_M, Missile_L;
    [SerializeField] private Action Enemyaction = Action.Rotate_around;

    [Space]
    [SerializeField] private float Speed = 5, RotateSpeed = 1.0f, ChargeDashSpeed = 1.0f,
        ChargeDashIntarval = 2f,StopChargeDashIntarval =  0.7f;
   
    [Space]
    [SerializeField]
    private float PowerChargeSpeed = 1.0f,
        PowerChargeIntarval = 2f, StopPowerChargeIntarval =  0.7f;

    [Space]
    [SerializeField]
    private float RapidMissileIntarval = 1.0f, RapidMissileDistance = 13f;

    [Space]
    [SerializeField]
    private float MediumMissileIntarval = 5.0f, MediumMissileDistance = 13f, MediumMissilePower = 20f;

    [Space]
    [SerializeField]
    private float MisfireMissileIntarval = 4.0f, MisfireMissileDistance = 13f, MisfireMissilePower = 20f;

    [SerializeField] private Vector3 StartPos;
    [SerializeField] private float MoveChangeIntarval = 30f;
    private NavMeshAgent Agent;
    private Rigidbody rb;
    private float TimeCount,MoveChangeCount;
    private bool ActionSet;
    private Vector3 forward;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        TimeCount = 0; ActionSet = true;
        MoveChangeCount = 0;
        //StartPos = transform.position;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 1.5f, transform.localScale * 0.5f);
    }
    private void NavChanged(bool change)
    {
        rb.isKinematic = change;
        Agent.enabled = change;
    }

    bool BoxLayCheck()
    {
        bool check; RaycastHit hit;
        LayerMask mask = 3 << LayerMask.NameToLayer("Stage");
        check = Physics.BoxCast(transform.position, transform.localScale * 0.5f, Vector3.down, out hit, Quaternion.identity, 1.5f, mask);
        if (check) Debug.Log(hit.collider.gameObject.name);
        return check;
    }
    float GetDistance()
    {
        Vector3 player = new Vector3(Player.transform.position.x, 0f, Player.transform.position.z);
        Vector3 thisEnemy = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        return Vector3.Distance(player, thisEnemy);
    }
    void LookPlayer()
    {
        Vector3 lookforward = new Vector3(Player.transform.position.x - transform.position.x,
            0, Player.transform.position.z - transform.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookforward, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, RotateSpeed);
    }
    private void ActionChange()
    {
        int random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(Action)).Length);
        if(Enemyaction == Action.Rapid_missile || Enemyaction == Action.Medium_missile || Enemyaction == Action.Misfire_missile)
        {
            if(random == 2 || random == 3 || random == 5)
                ActionChange();
            else
            {
                MoveChangeCount = 0f;
                Enemyaction = (Action)random;
            }
        }
        else if (random == (int)Enemyaction)
            ActionChange();

        else
        {
            MoveChangeCount = 0f;
            Enemyaction = (Action)random;
        }       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, RotateSpeed);
        // Quaternion.AngleAxis(RotateSpeed, Vector3.up);
        TimeCount += Time.deltaTime;
        
        if(MoveChangeCount >= MoveChangeIntarval)
            ActionChange();
        switch (Enemyaction)
        {
            case Action.Rotate_around:
                {
                    Rotate_around_Action();
                    break;
                }
            case Action.Charge_dash:
                {
                    Charge_dash_Action();
                    break;
                }
            case Action.Rapid_missile:
                {
                    Rapid_missile_Action();
                    break;
                }
            case Action.Medium_missile:
                {
                    Medium_missile_Action();
                    break;
                }
            case Action.Power_charge:
                {
                    Power_charge_Action();
                    break;
                }
            case Action.Misfire_missile:
                {
                    Misfire_missile_Action();
                    break;
                }
        }


        if (Agent.enabled == true)
        {
            //Agent.destination = Player.transform.position;
            //transform.RotateAround(new Vector3(0,0,100), Vector3.up, 1);
            Debug.Log("");
        }
    }

    void Rotate_around_Action()
    {
        if (ActionSet)
        {
            NavChanged(false);
            TimeCount = 0;
            Vector3 direction = StartPos;
            direction.y = 0.0f;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            //Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = Quaternion.Euler(0, 90, 0) * lookRotation;
            ActionSet = false;
        }
        MoveChangeCount += Time.deltaTime * 3f;
        float RotateDistance = Vector3.Distance(new Vector3(Player.transform.position.x,0,Player.transform.position.z), StartPos);
        float EnemyDistance = Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), StartPos);
        if (RotateDistance < 15f && EnemyDistance < 18f)
            transform.RotateAround(Player.transform.position, Vector3.up, RotateSpeed);
       else
            transform.RotateAround(StartPos, Vector3.up, RotateSpeed);
    }
    private void ResetPos()
    {
        rb.velocity = Vector3.zero;
        NavChanged(true);
        Agent.destination = Player.transform.position;
        ActionSet = true;
        MoveChangeCount += 10f;
    }
    void Charge_dash_Action()
    {
        if (ActionSet)
        {
            NavChanged(false);
            TimeCount = 0;
            ActionSet = false;
        }
        if (TimeCount < ChargeDashIntarval)
        {
            //forward = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            forward = Player.transform.position - transform.position;
            forward.y = 0.0f;
            LookPlayer();
        }
        else if (TimeCount > ChargeDashIntarval + StopChargeDashIntarval)
        {
            ResetPos();
        }
        else if (TimeCount > ChargeDashIntarval)
        {
            //Vector3 forward = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            rb.AddForce(forward.normalized * ChargeDashSpeed);
            if (!BoxLayCheck())
            {
                ResetPos();
            }
        }
    }
    void Rapid_missile_Action()
    {
        if (ActionSet)
        {
            rb.velocity = Vector3.zero;
            NavChanged(true);
            TimeCount = 0;
            ActionSet = false;
        }
       
        if (Agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            Agent.destination = Player.transform.position;
        }
        else
        {
            if (Agent.enabled == false)
            {
                NavChanged(true);
            }
            //transform.position = StartPos;
        }
        
        float PlaneDistance = GetDistance();
      
        if (PlaneDistance < RapidMissileDistance)
        {
            Agent.speed = 0.001f; 
            if(TimeCount > RapidMissileIntarval) {
                TimeCount = 0f;
                GameObject Missile_left =Instantiate(Missile_S, LeftLauncher.transform.position, Quaternion.identity);
                GameObject Missile_right = Instantiate(Missile_S, RightLauncher.transform.position, Quaternion.identity);
                MoveChangeCount += 2f;
            }
        }
        else
        {
            Agent.speed = Speed;
        }
        
    }
    void Medium_missile_Action()
    {
        if (ActionSet)
        {
            rb.velocity = Vector3.zero;
            NavChanged(true);
            TimeCount = 0;
            ActionSet = false;
        }
        if (Agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            Agent.destination = Player.transform.position;
        }
        else
        {
            if (Agent.enabled == false)
            {
                NavChanged(true);
            }
            //transform.position = StartPos;
        }
        float PlaneDistance = GetDistance();

        if (PlaneDistance < MediumMissileDistance)
        {
            LookPlayer();
            Agent.speed = 0.001f;
            if (TimeCount > MediumMissileIntarval)
            {
                TimeCount = 0f;


                Vector3 look_left = new Vector3(LeftLauncher.transform.position.x - transform.position.x,
            0, LeftLauncher.transform.position.z - transform.position.z);
                Quaternion Rote_left = Quaternion.LookRotation(-look_left.normalized, Vector3.up) * Quaternion.Euler(90, 0, 0);
                GameObject Missile_left = Instantiate(Missile_M, LeftLauncher.transform.position, Rote_left);
                Rigidbody rigidbody_left = Missile_left.GetComponent<Rigidbody>();
                rigidbody_left.AddForce((transform.position - LeftLauncher.transform.position).normalized * MediumMissilePower, ForceMode.Impulse);
                
                Vector3 look_right = new Vector3(RightLauncher.transform.position.x - transform.position.x,
            0, RightLauncher.transform.position.z - transform.position.z);
                Quaternion Rote_right = Quaternion.LookRotation(-look_right.normalized, Vector3.up) * Quaternion.Euler(90, 0, 0);
                GameObject Missile_right = Instantiate(Missile_M, RightLauncher.transform.position, Rote_right);
                Rigidbody rigidbody_right = Missile_right.GetComponent<Rigidbody>();
                rigidbody_right.AddForce((transform.position - RightLauncher.transform.position).normalized * MediumMissilePower, ForceMode.Impulse);

                MoveChangeCount += 3f;
            }
        }
        else
        {
            Agent.speed = Speed;
        }
        // GameObject.Instantiate(Missile_M,new Vector3(), Quaternion.identity);
    }
    void Power_charge_Action()
    {
        if (ActionSet)
        {
            NavChanged(false);
            TimeCount = 0;
            ActionSet = false;
        }
        if (TimeCount < PowerChargeIntarval)
        {
            //forward = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            forward = Player.transform.position - transform.position;
            forward.y = 0.0f;
            LookPlayer();
        }
        else if (TimeCount > PowerChargeIntarval + StopPowerChargeIntarval)
        {
            ResetPos();
        }
        else if (TimeCount > PowerChargeIntarval)
        {
            //Vector3 forward = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            rb.AddForce(forward.normalized * PowerChargeSpeed);
            if (!BoxLayCheck())
            {
                ResetPos();
            }
        }
    }
    void Misfire_missile_Action()
    {
        if (ActionSet)
        {
            rb.velocity = Vector3.zero;
            NavChanged(true);
            TimeCount = 0;
            ActionSet = false;
        }
        if (Agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            Agent.destination = Player.transform.position;
        }
        else
        {
            if (Agent.enabled == false)
            {
                NavChanged(true);
            }
            //transform.position = StartPos;
        }
        float PlaneDistance = GetDistance();

        if (PlaneDistance < MisfireMissileDistance)
        {
            Agent.speed = 0.001f;
            if (TimeCount > MisfireMissileIntarval)
            {
                TimeCount = 0f;
                Vector3 look_left = new Vector3(LeftLauncher.transform.position.x - transform.position.x,
                0, LeftLauncher.transform.position.z - transform.position.z);
                Quaternion Rote_left = Quaternion.LookRotation(-look_left.normalized, Vector3.up) * Quaternion.Euler(90, 0, 0);
                GameObject Missile_left = Instantiate(Missile_L, LeftLauncher.transform.position, Rote_left);
                Rigidbody rigidbody_left = Missile_left.GetComponent<Rigidbody>();
                rigidbody_left.AddForce((transform.position - LeftLauncher.transform.position).normalized * MisfireMissilePower, ForceMode.Impulse);

                Vector3 look_right = new Vector3(RightLauncher.transform.position.x - transform.position.x,
                0, RightLauncher.transform.position.z - transform.position.z);
                Quaternion Rote_right = Quaternion.LookRotation(-look_right.normalized, Vector3.up) * Quaternion.Euler(90, 0, 0);
                GameObject Missile_right = Instantiate(Missile_L, RightLauncher.transform.position, Rote_right);
                Rigidbody rigidbody_right = Missile_right.GetComponent<Rigidbody>();
                rigidbody_right.AddForce((transform.position - RightLauncher.transform.position).normalized * MisfireMissilePower, ForceMode.Impulse);

                MoveChangeCount += 2.5f;
            }
        }
        else
        {
            Agent.speed = Speed;
        }
        // GameObject.Instantiate(Missile_L,new Vector3(), Quaternion.identity);

    }
}
 
