using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukanoPlayerController : MonoBehaviour
{
    [SerializeField]private List<BulletClass> PlayerBullet = new List<BulletClass>();
    [SerializeField] private BulletDatas datas;
    [SerializeField] private ShotEvent shotEvent;
    private int shotNum = 0; 

    private void Start()
    {
        SetBullet(0);
        SetBullet(1);
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayerBullet[shotNum].Shot();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("�e��ς��邺");
            shotNum++;
            if(shotNum == PlayerBullet.Count) 
            { 
                shotNum = 0;
            }
        }
        if(Input.GetMouseButtonDown(2))
        {
            Debug.Log("���̒e�̃��x�����グ�邺");
            PlayerBullet[shotNum].LevelUp();
        }
    }
    public void SetBullet(int ID)
    {
        BulletClass Bullet = new BulletClass();
        Bullet.Initialsetting(datas, shotEvent, ID, 0);
        PlayerBullet.Add(Bullet);
    }
}
