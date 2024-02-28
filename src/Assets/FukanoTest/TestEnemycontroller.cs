using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemycontroller : MonoBehaviour,EnemyInterface
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void AddDamage(GameObject player, float damage)
    {
        Debug.Log("‘Å‚½‚ê‚½");
        Vector3 addForce = this.transform.position - player.transform.position;
        rb.AddForce(addForce*damage*1000);
    }
}
