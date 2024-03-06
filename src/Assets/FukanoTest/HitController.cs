using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        Debug.Log(other.name);
        var HitTarget = other.GetComponent<EnemyInterface>();
        if (HitTarget == null)
            return;

        if (gun == Gun.L)
        {
            BulletClass bullet = PlayerController.LeftBullet[PlayerController.LeftShotNum];
            HitTarget.AddDamage(Player,bullet.GetPower(),bullet.GetRecoil());
        }
        else if (gun == Gun.R)
        {
            BulletClass bullet = PlayerController.RightBullet[PlayerController.RightShotNum];
            HitTarget.AddDamage(Player,bullet.GetPower(),bullet.GetRecoil());
        }

    }
}
