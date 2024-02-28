using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukanoPlayerController : MonoBehaviour
{
    [SerializeField] private List<BulletClass> LeftBullet = new List<BulletClass>();
    [SerializeField] private List<BulletClass> RightBullet = new List<BulletClass>();
    [SerializeField] private BulletDatas datas;
    [SerializeField] private ShotEvent shotEvent;
    private int LeftShotNum = 0;
    private int RightShotNum = 0;

    private void Start()
    {
        SetBullet(0,"L");
        shotEvent.ChangeBullet(LeftBullet[LeftShotNum].GetAngle(), LeftBullet[LeftShotNum].GetRange(), "L");
        SetBullet(1, "L");
        SetBullet(0, "R");
        shotEvent.ChangeBullet(RightBullet[LeftShotNum].GetAngle(), RightBullet[LeftShotNum].GetRange(), "R");
        SetBullet(1, "R");
    }
   
    public void Leftshot()
    {
        LeftBullet[LeftShotNum].Shot("L");
    }
    public void RightShot()
    {
        RightBullet[LeftShotNum].Shot("R");
    }

    public void LeftChangeBullet()
    {
        Debug.Log("�e��ς��邺");
        LeftShotNum++;
        if (LeftShotNum == LeftBullet.Count)
        {
            LeftShotNum = 0;
        }
        shotEvent.ChangeBullet(LeftBullet[LeftShotNum].GetAngle(), LeftBullet[LeftShotNum].GetRange(),"L");
    }
    public void RightChangeBullet()
    {
        Debug.Log("�e��ς��邺");
        RightShotNum++;
        if (RightShotNum == RightBullet.Count)
        {
            RightShotNum = 0;
        }
        shotEvent.ChangeBullet(RightBullet[LeftShotNum].GetAngle(), RightBullet[LeftShotNum].GetRange(), "R");
    }

    public void SetBullet(int ID,string LR)
    {
        if (LR == "L")
        {
            BulletClass Bullet = new BulletClass();
            Bullet.Initialsetting(datas, shotEvent, ID, 0);
            LeftBullet.Add(Bullet);
        }
        else if (LR == "R")
        {
            BulletClass Bullet = new BulletClass();
            Bullet.Initialsetting(datas, shotEvent, ID, 0);
            RightBullet.Add(Bullet);
        }
        else
            Debug.Log("�������ԈႦ�Ă��܂�");
       
    }
}