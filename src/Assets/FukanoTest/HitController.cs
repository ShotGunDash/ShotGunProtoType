using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private FukanoPlayerController PlayerController;
    private enum Gun
    {
        L,
        R
    };
    [SerializeField] private Gun gun;

    private void Awake()
    {
        PlayerController = Player.GetComponent<FukanoPlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var HitTarget = other.GetComponent<EnemyInterface>();
        if (HitTarget == null)
            return;

        if (gun == Gun.L)
        {
            HitTarget.AddDamage(Player,PlayerController.LeftBullet[PlayerController.LeftShotNum].GetPower());
        }
        else if (gun == Gun.R)
        {
            HitTarget.AddDamage(Player,PlayerController.RightBullet[PlayerController.RightShotNum].GetPower());
        }

    }
}
