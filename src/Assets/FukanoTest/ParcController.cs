using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Playables;
using UnityEngine.Rendering;

[System.Serializable]
public class mParc 
{
    public int Power;
    public float Recoil;
    public float Speed;
    public float CoolTime;
    public int HP;
    public float CriticalProbability;
    public int CriticalPower;
    public float GetPartsProbability;
    public float AvoidanceProbability;
    public float Brake;
    public FairyParc mFairyParc = new FairyParc();
    public BloodWetParc mBloodWetParc = new BloodWetParc();
    public DevilWatchParc mDevilWatchParc = new DevilWatchParc();
    public ToolBoxParc mToolBoxParc = new ToolBoxParc();
    public AngelWatchParc mAngelWatchParc = new AngelWatchParc();
    public MetronomeParc mMetronomeParc = new MetronomeParc();
    public BloodsheadParc mBloodsheadParc = new BloodsheadParc();
    public int ContactDamage;
    public int Recovery;
   
    public int LTotalPower;
    public int RTotalPower;
    public float TotalAvoiddance;
    public float TotalCoolTime;
    public float TotalSpeed;
}

public class ParcController:MonoBehaviour
{
    public static ParcController instance;
    public mParc mParc = new mParc();
    public ParcDatas parcDatas;
    [SerializeField] private GameObject ParcWindow;
    [SerializeField] private GameObject Player;
    private PlayerHPController HPcontroller;

    private ItemManager itemManager => ItemManager.instance;

    private void Awake()
    {
        ParcWindow.SetActive(false);
        if (instance == null)
            instance = this;

        else
            Destroy(gameObject);

        HPcontroller = Player.GetComponent<PlayerHPController>();

       

    }

    public void StartParcSet()
    {
        Time.timeScale = 0;
        ParcWindow.SetActive(true);
    }

    public void FinishParcSet()
    {
        Time.timeScale = 1;
        ParcWindow.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            StartParcSet();
        }
    }

    private void FixedUpdate()
    {
        TimeParc();
        mParc.LTotalPower = (int)((mParc.Power*mParc.mMetronomeParc.PowerParsent) + mParc.mFairyParc.Power + mParc.mBloodWetParc.Power+mParc.mDevilWatchParc.LPower);
        mParc.RTotalPower = (int)((mParc.Power * mParc.mMetronomeParc.PowerParsent) + mParc.mFairyParc.Power + mParc.mBloodWetParc.Power + mParc.mDevilWatchParc.RPower);
        mParc.TotalCoolTime = mParc.CoolTime + mParc.mFairyParc.coolTime + mParc.mBloodWetParc.coolTime+mParc.mToolBoxParc.GetcoolTime;
        mParc.TotalAvoiddance = mParc.AvoidanceProbability + mParc.mFairyParc.Avoidance;
        mParc.TotalSpeed = mParc.Speed + mParc.mBloodWetParc.speed;
      
    }
    public void SetParc(int ID)
    {
        mParc.Power += parcDatas.ParcData[ID].Power;
        mParc.Recoil += parcDatas.ParcData[ID].Recoil;
        mParc.Speed += parcDatas.ParcData[ID].Speed;
        mParc.CoolTime += parcDatas.ParcData[ID].CoolTime;
        mParc.HP += parcDatas.ParcData[ID].HP;
        mParc.CriticalProbability += parcDatas.ParcData[ID].GetPartsProbability;
        mParc.CriticalPower += parcDatas.ParcData[ID].CriticalPower;
        mParc.GetPartsProbability += parcDatas.ParcData[ID].GetPartsProbability;
        mParc.AvoidanceProbability += parcDatas.ParcData[ID].AvoidanceProbability;
        mParc.Brake += parcDatas.ParcData[ID].Brake;
        mParc.ContactDamage += parcDatas.ParcData[ID].ContactDamage;
        mParc.Recovery += parcDatas.ParcData[ID].Recovery;
        mParc.mFairyParc.SetFailyParc(parcDatas.ParcData[ID].GetFairyParc);
        mParc.mBloodWetParc.SetBloodWetParc(parcDatas.ParcData[ID].GetBloodWetParc);
        mParc.mDevilWatchParc.SetDevilWatchParc(parcDatas.ParcData[ID].GetDevilWatchParc);
        mParc.mToolBoxParc.SetToolBoxParc(parcDatas.ParcData[ID].GetToolBoxParc);
        mParc.mAngelWatchParc.SetAngelWatchParc(parcDatas.ParcData[ID].GetAngelWatchParc);
        mParc.mMetronomeParc.SetMetronomeParc(parcDatas.ParcData[ID].GetMetronomeParc);
        mParc.mBloodsheadParc.SetBloodsheadParc(parcDatas.ParcData[ID].GetBloodsheadParc);

        itemManager.GetItem(parcDatas.ParcData[ID].GetBloodWetParc.GetPart);
    }

    public void RemoveParc(int ID)
    {
        mParc.Power -= parcDatas.ParcData[ID].Power;
        mParc.Recoil -= parcDatas.ParcData[ID].Recoil;
        mParc.Speed -= parcDatas.ParcData[ID].Speed;
        mParc.CoolTime -= parcDatas.ParcData[ID].CoolTime;
        mParc.HP -= parcDatas.ParcData[ID].HP;
        mParc.CriticalProbability -= parcDatas.ParcData[ID].GetPartsProbability;
        mParc.CriticalPower -= parcDatas.ParcData[ID].CriticalPower;
        mParc.GetPartsProbability -= parcDatas.ParcData[ID].GetPartsProbability;
        mParc.AvoidanceProbability -= parcDatas.ParcData[ID].AvoidanceProbability;
        mParc.ContactDamage -= parcDatas.ParcData[ID].ContactDamage;
        mParc.Recovery -= parcDatas.ParcData[ID].Recovery;
        mParc.mFairyParc.RemoveFailyParc(parcDatas.ParcData[ID].GetFairyParc);
        mParc.mBloodWetParc.RemoveBloodWetParc(parcDatas.ParcData[ID].GetBloodWetParc);
        mParc.mDevilWatchParc.RemoveDevilWatchParc(parcDatas.ParcData[ID].GetDevilWatchParc);
        mParc.mToolBoxParc.RemoveToolBoxParc(parcDatas.ParcData[ID].GetToolBoxParc);
        mParc.mAngelWatchParc.RemoveAngelWatchParc(parcDatas.ParcData[ID].GetAngelWatchParc);
        mParc.mMetronomeParc.RemoveMetronomeParc(parcDatas.ParcData[ID].GetMetronomeParc);
        mParc.mBloodsheadParc.RemoveBloodsheadParc(parcDatas.ParcData[ID].GetBloodsheadParc);
    }

    private void FailyParc()
    {
       
            if (mParc.mFairyParc.Power < mParc.mFairyParc.MaxPower)
                mParc.mFairyParc.Power += (mParc.mFairyParc.MaxPower / mParc.mFairyParc.MaxTime) * 0.02f;
            else
                mParc.mFairyParc.Power = mParc.mFairyParc.MaxPower;

            if (mParc.mFairyParc.Avoidance < mParc.mFairyParc.MaxAvoidance)
                mParc.mFairyParc.Avoidance += (mParc.mFairyParc.MaxAvoidance / mParc.mFairyParc.MaxTime) * 0.02f;
            else
                mParc.mFairyParc.Avoidance = mParc.mFairyParc.MaxAvoidance;

            if (mParc.mFairyParc.coolTime < mParc.mFairyParc.MaxCoolTime)
                mParc.mFairyParc.coolTime += (mParc.mFairyParc.MaxCoolTime / mParc.mFairyParc.MaxTime) * 0.02f;
            else
                mParc.mFairyParc.coolTime = mParc.mFairyParc.MaxCoolTime;

        
        
       
        
       
    }
    private void DevilWatchParc()
    {
        if (mParc.mDevilWatchParc.LPower >= mParc.mDevilWatchParc.MaxPower)
            mParc.mDevilWatchParc.LPower = mParc.mDevilWatchParc.MaxPower;
        else
            mParc.mDevilWatchParc.LPower += (mParc.mDevilWatchParc.MaxPower / mParc.mDevilWatchParc.MaxTime) * 0.02f;

        if (mParc.mDevilWatchParc.RPower >= mParc.mDevilWatchParc.MaxPower)
            mParc.mDevilWatchParc.RPower = mParc.mDevilWatchParc.MaxPower;
        else
            mParc.mDevilWatchParc.RPower += (mParc.mDevilWatchParc.MaxPower / mParc.mDevilWatchParc.MaxTime) * 0.02f;
    }
    private void BloodWetParcBuff()
    {
        if(HPcontroller.HP <= HPcontroller.MaxHP* (mParc.mBloodWetParc.RestHPPercent/100))
        {
            mParc.mBloodWetParc.Power = mParc.mBloodWetParc.MaxPower;
            mParc.mBloodWetParc.coolTime = mParc.mBloodWetParc.MaxCoolTime;
            mParc.mBloodWetParc.speed = mParc.mBloodWetParc.MaxSpeed;
        }
        else
        {
            mParc.mBloodWetParc.Power = 0;
            mParc.mBloodWetParc.coolTime = 0;
            mParc.mBloodWetParc.speed = 0;
        }
    }

    private void BloodWetParcDamage()
    {
        if (HPcontroller.HP >= HPcontroller.MaxHP * (mParc.mBloodWetParc.MaxHPPercent / 100))
        {
            HPcontroller.Damage(mParc.mBloodWetParc.Damage);
        }
       
    }

    private void AngelWatchParc()
    {
        if (mParc.mAngelWatchParc.Power >= mParc.mAngelWatchParc.MaxPower)
            mParc.mAngelWatchParc.Power = mParc.mAngelWatchParc.MaxPower;
        else
            mParc.mAngelWatchParc.Power += (mParc.mAngelWatchParc.MaxPower / mParc.mAngelWatchParc.MaxTime) * 0.02f;
    }

    private void MetronomeParcTime()
    {
        mParc.mMetronomeParc.Time += 0.02f;
        if (mParc.mMetronomeParc.Time >= mParc.mMetronomeParc.MaxTime)
        {
            mParc.mMetronomeParc.Time = 0;
            mParc.mMetronomeParc.PowerParsent = 0;
        }
    }
    private void MetronomeParcAttack()
    {
        mParc.mMetronomeParc.Time = 0;
        if (mParc.mMetronomeParc.PowerParsent >= mParc.mMetronomeParc.MaxPowerParsent)
            mParc.mMetronomeParc.PowerParsent = mParc.mMetronomeParc.MaxPowerParsent;
        else
            mParc.mMetronomeParc.PowerParsent += mParc.mMetronomeParc.ParcentUp;



    }
    private void BloodshedParcAttack()
    {
        float random = Random.Range(0,101);
        if(random <= mParc.mBloodsheadParc.HealParsent)
            HPcontroller.Heal( mParc.mBloodsheadParc.AttackHeal+mParc.Recovery);
        
    }
    private void BloodsheadParcCritical()
    {
        HPcontroller.Heal(mParc.mBloodsheadParc.CriticalHeal + mParc.Recovery);
    }
    public void ShotParc()
    {
        mParc.mFairyParc.Power = 0;
        mParc.mFairyParc.Avoidance = 0;
        mParc.mFairyParc.coolTime = 0;

        mParc.mDevilWatchParc.LPower = 0;
        mParc.mDevilWatchParc.RPower = 0;
    }

    public void DamageParc()
    {
        mParc.mAngelWatchParc.Power = 0;
    }

    public void AttackParc()
    {
        MetronomeParcAttack();
        BloodshedParcAttack();

        BloodWetParcDamage();
    }
    public void CriticalParc()
    {
        BloodsheadParcCritical();
    }
    public void TimeParc()
    {
        FailyParc();
        BloodWetParcBuff();
        DevilWatchParc();
        AngelWatchParc();
        MetronomeParcTime();
    }
}
