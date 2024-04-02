using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    //[SerializeField] private GameObject Enemy;
    [SerializeField] float RespawnIntarval = 1f;
    [SerializeField] private float SpawnRange = 1.8f;
    [SerializeField] private SpawnClass _SpawnClass;
    [SerializeField]
    private int stageNumber = 0;
    private Transform[] Points;
    private float RespawnTime = 0, TimeCount = 0;
    private int EnemyMember, LateSum ,LateCount = 0; 
    // Start is called before the first frame update
    void Start()
    {
        var children = EnumChildren(this.transform);
        var childIndex = 0;
        Points = new Transform[this.transform.childCount];

        foreach (var child in children)
        {
            Points[childIndex++] = child;
        }

        EnemyMember = _SpawnClass._spawnData[stageNumber]._enemyObject.Length;
        LateSum = _SpawnClass._spawnData[stageNumber]._spawnLateData[LateCount].GetLate();
    }
    private static IEnumerable<Transform> EnumChildren(Transform parent)
    {
        return parent.OfType<Transform>();
    }


    private GameObject SpawnEnemy()
    {
        int Randam_Value = Random.Range(0, LateSum);
        for(int i = 0; i < EnemyMember; i++)
        {
            if(Randam_Value < _SpawnClass._spawnData[stageNumber]._spawnLateData[LateCount].SpawnLate[i])
            {
                return _SpawnClass._spawnData[stageNumber]._enemyObject[i];
            }
        }
        return _SpawnClass._spawnData[stageNumber]._enemyObject[EnemyMember - 1];
    }
    private Vector3 SpawnPoint()
    {
        Vector3 RespawnPoint = Points.First().position;
        float distance = -1;

        for (int i = 0; i < Points.Length; i++)
        {
            float Nextdis = Vector3.Distance(Player.transform.position, Points[i].position);
            if (distance < Nextdis)
            {
                distance = Nextdis;
                RespawnPoint = Points[i].position;
            }
        }

        float randomValue_X = Random.Range(-SpawnRange, SpawnRange);
        float randomValue_Z = Random.Range(-SpawnRange, SpawnRange);
        return RespawnPoint + new Vector3(randomValue_X, 1f, randomValue_Z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        TimeCount += Time.deltaTime;
        if(_SpawnClass._spawnData[stageNumber]._spawnLateData[LateCount].IntarvalTime < TimeCount 
            && LateCount < _SpawnClass._spawnData[stageNumber]._spawnLateData.Length)
        {
            LateCount++;
            LateSum = _SpawnClass._spawnData[stageNumber]._spawnLateData[LateCount].GetLate();
        }

        RespawnTime += Time.deltaTime;
        if(RespawnTime > RespawnIntarval)
        {
            RespawnTime = 0;

            
            GameObject.Instantiate(SpawnEnemy(), SpawnPoint(), Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
