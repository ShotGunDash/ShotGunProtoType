using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShieldEnemyHitController : MonoBehaviour, EnemyInterface
{
    private Rigidbody rb;
    [SerializeField] private int HP;
    [SerializeField] private GameObject Shield;
    [SerializeField] private List<GameObject> DropItems = new List<GameObject>();
    private NavMeshAgent agent;
    private ParcController parcController => ParcController.instance;
    private GameObject Player;

    private float DotTime = 1;
    private float DotMaxTime = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    public void AddDamage(GameObject player, int damage, float recoil)
    {
        float ShieldDist = Vector3.Distance(player.transform.position, Shield.transform.position);
        float EnemyDist = Vector3.Distance(player.transform.position, this.transform.position);

        if(EnemyDist < ShieldDist)
        {
            rb.isKinematic = false;
            agent.enabled = false;
            HP -= damage;
            if (HP <= 0)
                Dead();
            Vector3 angle = this.transform.position - player.transform.position;
            float S = Mathf.Sqrt(angle.x + angle.y + angle.z);
            rb.AddForce(new Vector3(angle.x / S, 0, angle.z / S) * recoil);
            StartCoroutine("GetUp");
        }
        else
        {
            Debug.Log("–h‚ª‚ê‚Ü‚µ‚½");
            rb.isKinematic = false;
            agent.enabled = false;
            Vector3 angle = this.transform.position - player.transform.position;
            float S = Mathf.Sqrt(angle.x + angle.y + angle.z);
            rb.AddForce(new Vector3(angle.x / S, 0, angle.z / S) * recoil/2);
            StartCoroutine("GetUp");
        }
       
    }

    public void ShieldPenetrationDamage(GameObject player, int damage, float recoil)
    {
        rb.isKinematic = false;
        agent.enabled = false;
        HP -= damage;
        Debug.Log(damage );
        if (HP <= 0)
            Dead();
        Vector3 angle = this.transform.position - player.transform.position;
        float S = Mathf.Sqrt(angle.x + angle.y + angle.z);
        rb.AddForce(new Vector3(angle.x / S, 0, angle.z / S) * recoil);
        StartCoroutine("GetUp");
    }

    public void DotDamage(int damage)
    {
        DotTime += Time.deltaTime;
        if(DotTime >= DotMaxTime)
        {
            Debug.Log("Dot");
            HP -= damage;
            if (HP <= 0)
                Dead();
            DotTime = 0;
        }
    }
    public void Dead()
    {
        foreach (GameObject item in DropItems)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;
        agent.enabled = true;

    }
}
