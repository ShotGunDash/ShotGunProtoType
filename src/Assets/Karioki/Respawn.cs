using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Enemy;
    [SerializeField] float RespawnIntarval = 1f;
    [SerializeField] private float SpawnRange = 1.8f;
    private Transform[] Points;
    private float RespawnTime = 0;

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
    }
    private static IEnumerable<Transform> EnumChildren(Transform parent)
    {
        return parent.OfType<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RespawnTime += Time.deltaTime;
        if(RespawnTime > RespawnIntarval)
        {
            RespawnTime = 0;

            Vector3 RespawnPoint = Points.First().position;
            float distance = -1;

            for (int i = 0;  i < Points.Length; i++)
            {
                float Nextdis = Vector3.Distance(Player.transform.position, Points[i].position);
                if(distance < Nextdis)
                {
                    distance = Nextdis;
                    RespawnPoint = Points[i].position;
                }    
            }
            float randomValue_X = Random.Range(-SpawnRange, SpawnRange);
            float randomValue_Z = Random.Range(-SpawnRange, SpawnRange);
            GameObject.Instantiate(Enemy, RespawnPoint + new Vector3(randomValue_X, 0f, randomValue_Z), Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
