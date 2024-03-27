using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyInterface
{
    void AddDamage(GameObject player,int damage,float recoil);

    void ShieldPenetrationDamage(GameObject player, int damage, float recoil);

    void DotDamage(int damage);
    void Dead();
}
