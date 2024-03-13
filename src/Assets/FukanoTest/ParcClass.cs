using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ParcDatabase")]
public class ParcDatas : ScriptableObject
{
    public List<Parc> ParcData;
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
}

