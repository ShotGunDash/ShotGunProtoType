using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukanoPlayerController : MonoBehaviour
{
    [SerializeField] public List<BulletClass> LeftBullet = new List<BulletClass>();
    [SerializeField] public List<BulletClass> RightBullet = new List<BulletClass>();
    [SerializeField] private BulletDatas datas;
    [SerializeField] private ShotEvent shotEvent;
    public int LeftShotNum = 1;
    public int RightShotNum = 0;
    public mParc mParc = new mParc();
    private void Start()
    {
        SetBullet(0,"L");
        shotEvent.ChangeBullet(LeftBullet[LeftShotNum].GetAngle(), LeftBullet[LeftShotNum].GetRange(), "L");
        SetBullet(1, "L");
        SetBullet(0, "R");
        shotEvent.ChangeBullet(RightBullet[LeftShotNum].GetAngle(), RightBullet[LeftShotNum].GetRange(), "R");
        SetBullet(1, "R");
        Debug.Log(mParc.Brake);
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
        Debug.Log("’e‚ð•Ï‚¦‚é‚º");
        LeftShotNum++;
        if (LeftShotNum == LeftBullet.Count)
        {
            LeftShotNum = 0;
        }
        shotEvent.ChangeBullet(LeftBullet[LeftShotNum].GetAngle(), LeftBullet[LeftShotNum].GetRange(),"L");
    }
    public void RightChangeBullet()
    {
        Debug.Log("’e‚ð•Ï‚¦‚é‚º");
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
            Debug.Log("•¶Žš‚ªŠÔˆá‚¦‚Ä‚¢‚Ü‚·");
       
    }
}
