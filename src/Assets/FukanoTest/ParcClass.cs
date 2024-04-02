using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ParcDatabase")]
public class ParcDatas : ScriptableObject
{
    public List<Parc> ParcData;
}

[System.Serializable]
public class FairyParc
{
    [NonSerialized] public float Power;
    [NonSerialized] public float Avoidance;
    [NonSerialized] public float coolTime;

    public float   MaxPower;
    public float MaxAvoidance;
    public float MaxCoolTime;
    public float MaxTime;

    public void SetFailyParc(FairyParc mFailyParc)
    {
        MaxPower += mFailyParc.MaxPower;
        MaxAvoidance += mFailyParc.MaxAvoidance;
        MaxCoolTime += mFailyParc.MaxCoolTime;
        if(MaxTime <=0||MaxTime > mFailyParc.MaxTime)
            MaxTime = mFailyParc.MaxTime;
    }

    public void RemoveFailyParc(FairyParc mFailyParc)
    {
        MaxPower -= mFailyParc.MaxPower;
        MaxAvoidance -= mFailyParc.MaxAvoidance;
        MaxCoolTime -= mFailyParc.MaxCoolTime;
        
    }

}
[System.Serializable]
public class BloodWetParc
{
    public float RestHPPercent;
    [NonSerialized] public float Power;
    [NonSerialized] public float coolTime;
    [NonSerialized] public float speed;
    public int MaxPower;
    public float MaxCoolTime;
    public float MaxSpeed;

    public void SetBloodWetParc(BloodWetParc mBloodWetParc)
    {
        MaxPower += mBloodWetParc.MaxPower;
        MaxCoolTime += mBloodWetParc.MaxCoolTime;
        MaxSpeed += mBloodWetParc.MaxSpeed;
        if (RestHPPercent <= 0 || RestHPPercent < mBloodWetParc.RestHPPercent)
            RestHPPercent = mBloodWetParc.RestHPPercent;
    }

    public void RemoveBloodWetParc(BloodWetParc mBloodWetParc)
    {
        MaxPower -= mBloodWetParc.MaxPower;
        MaxCoolTime -= mBloodWetParc.MaxCoolTime;
        MaxSpeed -= mBloodWetParc.MaxSpeed;
    }

}

[System.Serializable]
public class DevilWatchParc
{
    public float MaxPower;
    public float MaxTime;
    [NonSerialized] public float LPower;
    [NonSerialized] public float RPower;
    public void SetDevilWatchParc(DevilWatchParc mDevilWatchParc)
    {
        MaxPower += mDevilWatchParc.MaxPower;
        if (MaxTime <= 0 || MaxTime > mDevilWatchParc.MaxTime)
            MaxTime = mDevilWatchParc.MaxTime;
    }
    public void RemoveDevilWatchParc(DevilWatchParc mDevilWatchParc)
    {
        MaxPower -= mDevilWatchParc.MaxPower;
      
    }

}

[System.Serializable]
public class Parc
{
    [SerializeField] private string name;
    [SerializeField] private Sprite image;
    [SerializeField] private string explanation;
    [SerializeField] private int id;
    [SerializeField] private int power;
    [SerializeField] private float recoil;
    [SerializeField] private float speed;
    [SerializeField] private float coolTime;
    [SerializeField] private int hp;
    [SerializeField] private float criticalProbability;
    [SerializeField] private int criticalPower;
    [SerializeField] private float getPartsProbability;
    [SerializeField] private float avoidanceProbability;
    [SerializeField] private float brake;
    [SerializeField] private int contactDamage;
    [SerializeField] private int recovery;
    [SerializeField] private FairyParc fairyParc;
    [SerializeField] private BloodWetParc bloodWetParc;
    [SerializeField] private DevilWatchParc devilWatch;

    public string Name  { get => name; }

    public Sprite Image { get => image; }
    public string Explanation { get => explanation; }
    public int ID { get => id;  }
    public int Power { get => power; }
    public float Recoil { get => recoil; }
    public float Speed { get => speed;  }
    public float CoolTime { get => coolTime;  }
    public int HP { get => hp;}
    public float CriticalProbability { get => criticalProbability;  }
    public int CriticalPower { get => criticalPower;}
    public float GetPartsProbability { get => getPartsProbability; }
    public float AvoidanceProbability { get => avoidanceProbability; }
    public float Brake { get => brake;}

    public int ContactDamage { get=> contactDamage; }

    public int Recovery { get => recovery; }  

    public FairyParc GetFairyParc { get=> fairyParc; }

    public BloodWetParc GetBloodWetParc { get=> bloodWetParc;}

    public DevilWatchParc GetDevilWatchParc { get => devilWatch; }


}

