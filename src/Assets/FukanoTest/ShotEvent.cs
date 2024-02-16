using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEvent : MonoBehaviour
{
    [SerializeField] private GameObject LeftShot;
    [SerializeField] private GameObject RightShot;
    [SerializeField] private ConeCollider LeftCollider;
    [SerializeField] private ConeCollider RightCollider;

   
    public void NormalShot(float power,float recoil,float angle,float range,string LR)
    {
       if(LR == "L")
        {
            Debug.Log("ç∂îÑÇ¡ÇΩÇÊ");
            LeftShot.SetActive(true);
            StartCoroutine("LeftFinishShot");
          
        }
       else if(LR == "R") 
        {
            Debug.Log("âEîÑÇ¡ÇΩÇÊ");
            RightShot.SetActive(true);
            StartCoroutine("RightFinishShot");
        }
      //  Debug.Log(power+"ÇÃà–óÕÇæ"+recoil+"ÇÃîΩìÆÇæ");
    
    }
    public void FireShot(float power, float recoil, float angle, float range, string LR)
    {
        Debug.Log(power + "ÇÃà–óÕÇæ" + recoil + "ÇÃîΩìÆÇæ ínñ Ç™îRÇ¶ÇƒÇÈÇÊ");
    }

    public void ChangeBullet(float angle, float range, string LR)
    {
        if (LR == "L")
        {
            LeftCollider.SetCone(angle, range);
        }
        else if (LR == "R")
        {
            RightCollider.SetCone(angle, range);
        }
    }

    IEnumerator LeftFinishShot()
    {
        yield return null;
        LeftShot.SetActive(false);
    }

    IEnumerator RightFinishShot()
    {
        yield return null;
        RightShot.SetActive(false);
    }
}
