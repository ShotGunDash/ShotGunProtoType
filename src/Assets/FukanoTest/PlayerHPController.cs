using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPController : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private int HP;
    private int MaxHP;
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

    public void Dead()
    {
        transform.position = RePos;
        HP = MaxHP;
    }
}
