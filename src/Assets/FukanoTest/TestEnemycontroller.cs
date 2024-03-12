using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemycontroller : MonoBehaviour,EnemyInterface
{
    private Rigidbody rb;
    [SerializeField]private int HP;
    [SerializeField] private List<GameObject> DropItems = new List<GameObject>();
    private NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void AddDamage(GameObject player, int damage,float recoil)
    {
        rb.isKinematic = false;
        agent.enabled = false;
        HP -= damage;
        if (HP <= 0)
            Dead();
        Vector3 angle = this.transform.position - player.transform.position;
        float S = Mathf.Sqrt(angle.x+angle.y+angle.z);
        rb.AddForce(new Vector3(angle.x/S,0,angle.z/S)*recoil);
        StartCoroutine("GetUp");
    }
    public void Dead()
    {
        foreach (GameObject item in DropItems)
        {
            Instantiate(item,transform.position,Quaternion.identity);
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
