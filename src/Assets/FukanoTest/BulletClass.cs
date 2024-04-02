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
    private int mPower;
    private float mSpeed;
    private float mRecoil;
    private float mAngle;
    private float mRange;
    private float mCoolTime;

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
            mSpeed = mBullet.speed + (bulletDatabase.bulletDatas[ID].LevelSpeed * level);
            mRecoil = mBullet.recoil + (bulletDatabase.bulletDatas[ID].Levelrecoil * level);
            mAngle = mBullet.angle + (bulletDatabase.bulletDatas[ID].Levelangle * level);
            mRange = mBullet.range + (bulletDatabase.bulletDatas[ID].Levelrange * level);
            mCoolTime = mBullet.coolTime - (bulletDatabase.bulletDatas[ID].LevelcoolTime * level);
        }
       
    }
    public void LevelUp()
    {
        level++;
        mPower = mBullet.power + (bulletDatabase.bulletDatas[ID].Levelpower * level);
        mSpeed = mBullet.speed + (bulletDatabase.bulletDatas[ID].LevelSpeed * level);
        mRecoil = mBullet.recoil + (bulletDatabase.bulletDatas[ID].Levelrecoil * level);
        mAngle = mBullet.angle + (bulletDatabase.bulletDatas[ID].Levelangle * level);
        mRange = mBullet.range + (bulletDatabase.bulletDatas[ID].Levelrange * level);
        mCoolTime = mBullet.coolTime - (bulletDatabase.bulletDatas[ID].LevelcoolTime * level);
    }

    public float  GetAngle()
    {
        return mAngle;
    }

    public float GetRange()
    {
        return mRange;
    }

    public int GetPower()
    {
        return mPower;
    }
    public float GetSpeed()
    {
        return mSpeed;
    }
    public float GetRecoil()
    {
        return mRecoil;
    }

    public float GetCoolTime()
    {
        return mCoolTime;
    }

    public BulletData GetBullet()
    {
        return mBullet;
    }
    public void Shot(string LR)
    {
        shotEvent.Shot(LR);
    }
}
