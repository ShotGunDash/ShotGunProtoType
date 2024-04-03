using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnDatabase")]
public class SpawnClass : ScriptableObject
{
    public SpawnClass Clone()
    {
        return Instantiate(this);
    }

    public SpawnData[] _spawnData;
  
    [System.Serializable]
    public class SpawnData
    {
        public GameObject[] _enemyObject;
        public SpawnLateData[] _spawnLateData;

        [System.Serializable]
        public class SpawnLateData
        {
            public float IntarvalTime;
            public int[] SpawnLate;

            public int GetLate()
            {
                int count = 0;
                for(int i = 0; i < SpawnLate.Length; i++)
                {
                    count += SpawnLate[i];
                }
                return count;
            }
        }
    }
}


