using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private float SpawnIntarval = 5f;
    [SerializeField] private float DestroyIntarval = 30f;
    [SerializeField] private int SpawnMenberNum = 3;
    [SerializeField] private float SpawnRange = 1.5f;

    [SerializeField] private float OnGroundDistanse = 1.0f;
    [SerializeField] private float OnGroundSphereScale = 0.3f;
    private float SpawnTime = 0;
    private float DestroyTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private bool OnGround()
    {
        RaycastHit hit;
        LayerMask mask = 3 << LayerMask.NameToLayer("Stage");
        bool Trigger = Physics.SphereCast(transform.position, OnGroundSphereScale, Vector3.down, out hit, OnGroundDistanse, mask);
        if (Trigger) Debug.Log(hit.collider.gameObject.name);
        return Trigger;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (OnGround())
        {
            SpawnTime += Time.deltaTime;
            DestroyTime += Time.deltaTime;
        }
        if (SpawnTime > SpawnIntarval)
        {
            SpawnTime = 0f;
            for(int i = 0; i < SpawnMenberNum; i++)
            {
                float randomValue_X = Random.Range(-SpawnRange, SpawnRange);
                float randomValue_Z = Random.Range(-SpawnRange, SpawnRange);
                Vector3 SpawnPos = transform.position + new Vector3(randomValue_X, 0f, randomValue_Z);
                GameObject.Instantiate(Enemy, SpawnPos, Quaternion.Euler(0f, 0f, 0f));
            }
        }
        if (DestroyTime > DestroyIntarval)
        {
            DestroyTime = 0f;
            Destroy(this.gameObject);
        }
    }
}
