using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEvent : MonoBehaviour
{
    [SerializeField] private GameObject LeftShot;
    [SerializeField] private GameObject RightShot;
    [SerializeField] private ConeCollider LeftCollider;
    [SerializeField] private ConeCollider RightCollider;
    private ParcController parc => ParcController.instance;

   
    public void Shot(string LR)
    {
       if(LR == "L")
        {
            //Debug.Log("ç∂îÑÇ¡ÇΩÇÊ");
            LeftShot.SetActive(true);
            StartCoroutine("LeftFinishShot");
          
        }
       else if(LR == "R") 
        {
           // Debug.Log("âEîÑÇ¡ÇΩÇÊ");
            RightShot.SetActive(true);
            StartCoroutine("RightFinishShot");
        }
      //  Debug.Log(power+"ÇÃà–óÕÇæ"+recoil+"ÇÃîΩìÆÇæ");
    
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
        yield return new WaitForSeconds(0.04f);
        LeftShot.SetActive(false);
        
    }

    IEnumerator RightFinishShot()
    {
        yield return new WaitForSeconds(0.04f);
        RightShot.SetActive(false);
    }
}
