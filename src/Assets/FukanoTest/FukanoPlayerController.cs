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
        BulletClass NormalBullet = new BulletClass();
        BulletClass FireBullet = new BulletClass();
        NormalBullet.Initialsetting(datas,shotEvent,0,0);
        FireBullet.Initialsetting(datas, shotEvent, 1, 0);
        PlayerBullet.Add(NormalBullet);
        PlayerBullet.Add(FireBullet);
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayerBullet[shotNum].Shot();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("íeÇïœÇ¶ÇÈÇ∫");
            shotNum++;
            if(shotNum == PlayerBullet.Count) 
            { 
                shotNum = 0;
            }
        }
        if(Input.GetMouseButtonDown(2))
        {
            Debug.Log("ç°ÇÃíeÇÃÉåÉxÉãÇè„Ç∞ÇÈÇ∫");
            PlayerBullet[shotNum].LevelUp();
        }
    }
}
