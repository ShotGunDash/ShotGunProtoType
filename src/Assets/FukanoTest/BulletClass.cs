using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BulletClass
{
    private BulletDatas bulletDatabase;
    private ShotEvent shotEvent;
    private int ID;
    private int level;
    private BulletData mBullet;
    private float mPower;
    private float mRecoil;

    public void Initialsetting(BulletDatas datas,ShotEvent events,int setID,int setLevel)
    { 
        bulletDatabase = datas;
        shotEvent = events;
        ID = setID;
        level = setLevel;
        if(bulletDatabase != null)
        {
           mBullet = bulletDatabase.bulletDatas[ID];
           mPower = mBullet.power+(bulletDatabase.bulletDatas[ID].Levelpower*level);
           mRecoil = mBullet.recoil + (bulletDatabase.bulletDatas[ID].Levelrecoil * level);
        }
       
    }
    public void LevelUp()
    {
        level++;
        mPower = mBullet.power + (bulletDatabase.bulletDatas[ID].Levelpower * level);
        mRecoil = mBullet.recoil + (bulletDatabase.bulletDatas[ID].Levelrecoil * level);
    }
    public void Shot()
    {
       switch (mBullet.type) 
        {
            case BulletData.BulletType.Normal: { shotEvent.NormalShot(mPower,mRecoil); break; }
            case BulletData.BulletType.Fire: { shotEvent.FireShot(mPower, mRecoil); break; }
            default: { Debug.Log("ƒWƒƒƒ€‚Á‚½‚º"); break; }
        }
    }
}
