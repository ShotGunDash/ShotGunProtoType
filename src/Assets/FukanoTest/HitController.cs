using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HitController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private ParcController parc => ParcController.instance;
    private PlayerHPController HPController;
    private FukanoPlayerController PlayerController;
    [SerializeField] private GameObject Fire;
    private enum Gun
    {
        L,
        R
    };
    [SerializeField] private Gun gun;

    private void Awake()
    {
        PlayerController = Player.GetComponent<FukanoPlayerController>();
        HPController = Player.GetComponent<PlayerHPController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var HitTarget = other.GetComponent<EnemyInterface>();
        
        if (HitTarget == null)
            return;

        Vector3 TargetPos = other.gameObject.transform.position;
      
        if (gun == Gun.L)
        {
            BulletClass bullet = PlayerController.LeftBullet[PlayerController.LeftShotNum];
            LBullettShot(bullet, HitTarget,TargetPos);
          
        }
        else if (gun == Gun.R)
        {
            BulletClass bullet = PlayerController.LeftBullet[PlayerController.RightShotNum];
            RBullettShot(bullet, HitTarget, TargetPos);
        }

    }

    private void LBullettShot(BulletClass bullet,EnemyInterface Target,Vector3 pos)
    {
        switch(bullet.GetBullet().type) 
        {
            case BulletData.BulletType.Normal:Target.AddDamage(Player, bullet.GetPower()+parc.mParc.LTotalPower, bullet.GetRecoil() + parc.mParc.Recoil); break;
            case BulletData.BulletType.OffShield: Target.ShieldPenetrationDamage(Player, bullet.GetPower() + parc.mParc.LTotalPower, bullet.GetRecoil() + parc.mParc.Recoil); break;
            case BulletData.BulletType.Heal: Target.AddDamage(Player, bullet.GetPower() + parc.mParc.LTotalPower, bullet.GetRecoil() + parc.mParc.Recoil);HPController.Heal(bullet.GetPower()/2 + +parc.mParc.Recovery); break;
            case BulletData.BulletType.Fire: Target.AddDamage(Player, bullet.GetPower() + parc.mParc.LTotalPower, bullet.GetRecoil() + parc.mParc.Recoil); GenerationFire((bullet.GetPower() + parc.mParc.LTotalPower)/5, pos); ; break;
        }
    }
    private void RBullettShot(BulletClass bullet, EnemyInterface Target, Vector3 pos)
    {
        switch (bullet.GetBullet().type)
        {
            case BulletData.BulletType.Normal: Target.AddDamage(Player, bullet.GetPower() + parc.mParc.RTotalPower, bullet.GetRecoil() + parc.mParc.Recoil); break;
            case BulletData.BulletType.OffShield: Target.ShieldPenetrationDamage(Player, bullet.GetPower() + parc.mParc.RTotalPower, bullet.GetRecoil() + parc.mParc.Recoil); break;
            case BulletData.BulletType.Heal: Target.AddDamage(Player, bullet.GetPower() + parc.mParc.RTotalPower, bullet.GetRecoil() + parc.mParc.Recoil); HPController.Heal(bullet.GetPower() / 2 + +parc.mParc.Recovery); break;
            case BulletData.BulletType.Fire: Target.AddDamage(Player, bullet.GetPower() + parc.mParc.RTotalPower, bullet.GetRecoil() + parc.mParc.Recoil); GenerationFire((bullet.GetPower() + parc.mParc.RTotalPower) / 5, pos); ; break;
        }
    }

    private void GenerationFire(int damage,Vector3 pos)
    {
       GameObject fire = Instantiate(Fire, new Vector3(pos.x, pos.y/2, pos.z), Quaternion.identity);
       FireController fireController = fire.GetComponent<FireController>();
        fireController.damage = damage;
    }
}
