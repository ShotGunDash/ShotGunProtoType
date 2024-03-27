using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPController : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP;
    public int MaxHP;
    private Vector3 RePos;

    private void Start()
    {
        RePos = transform.position;
        MaxHP = HP;
    }

    public void Damage(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            HP = 0;
            Dead();
        }
    }

    public void Heal(int Recovery)
    {
        HP += Recovery;
        if(HP> MaxHP) 
        {
            HP = MaxHP;
        }
    }

    public void Dead()
    {
        transform.position = RePos;
        HP = MaxHP;
    }
}
