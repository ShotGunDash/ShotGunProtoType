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
        Fire
    }
    public BulletType type;
    public string name;
    public int ID;
    public float power;
    public float recoil;
    public float angle;
    public float range;
    public float Levelpower; 
    public float Levelrecoil;
    public float Levelangle;
    public float Levelrange;




}
