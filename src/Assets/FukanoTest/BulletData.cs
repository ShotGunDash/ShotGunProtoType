using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "BulletDatabase")]
public class BulletDatas : ScriptableObject
{
   public List<BulletData> bulletDatas;
}
[System.Serializable]
public class BulletData
{
    public enum BulletType
    {
        Normal,
        OffShield,
        Heal,
        Fire
    }
    public enum Rarity
    {
        N,
        R,
        SR,
        SSR
    }
    public string name;
    public int ID;
    public BulletType type;
    public Rarity rarity;
    public string explanation;
    public int power;
    public float speed;
    public float recoil;
    public float angle;
    public float range;
    public float coolTime;
    public int Levelpower;
    public float LevelSpeed;
    public float Levelrecoil;
    public float Levelangle;
    public float Levelrange;
    public float LevelcoolTime;



}
