using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public int damage;
    private void OnTriggerStay(Collider other)
    {
        var HitTarget = other.GetComponent<EnemyInterface>();
        if (HitTarget == null)
            return;

        HitTarget.DotDamage(damage);
    }
}
