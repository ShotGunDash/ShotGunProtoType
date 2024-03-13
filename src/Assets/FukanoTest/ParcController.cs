using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

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
}

public class ParcController:MonoBehaviour
{
    public static ParcController instance;
    public mParc mParc = new mParc();
    public ParcDatas parcDatas;
    [SerializeField] private GameObject ParcWindow;

    private void Awake()
    {
        ParcWindow.SetActive(false);
        if (instance == null)
            instance = this;

        else
            Destroy(gameObject);

       

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
        mParc.Brake -= parcDatas.ParcData[ID].Brake;
    }
}
