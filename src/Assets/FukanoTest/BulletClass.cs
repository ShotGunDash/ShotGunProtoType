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
    private float mAngle;
    private float mRange;

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
           mAngle = mBullet.angle + (bulletDatabase.bulletDatas[ID].Levelangle * level);
           mRange = mBullet.range + (bulletDatabase.bulletDatas[ID].Levelrange * level);
        }
       
    }
    public void LevelUp()
    {
        level++;
        mPower = mBullet.power + (bulletDatabase.bulletDatas[ID].Levelpower * level);
        mRecoil = mBullet.recoil + (bulletDatabase.bulletDatas[ID].Levelrecoil * level);
        mAngle = mBullet.angle + (bulletDatabase.bulletDatas[ID].Levelangle * level);
        mRange = mBullet.range + (bulletDatabase.bulletDatas[ID].Levelrange * level);
    }

    public float  GetAngle()
    {
        return mAngle;
    }

    public float GetRange()
    {
        return mRange;
    }
    public void Shot(string LR)
    {
       switch (mBullet.type) 
        {
            case BulletData.BulletType.Normal: { shotEvent.NormalShot(mPower,mRecoil,mAngle,mRange,LR); break; }
            case BulletData.BulletType.Fire: { shotEvent.FireShot(mPower, mRecoil, mAngle, mRange,LR); break; }
            default: { Debug.Log("ƒWƒƒƒ€‚Á‚½‚º"); break; }
        }
    }
}
