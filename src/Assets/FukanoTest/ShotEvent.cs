using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEvent : MonoBehaviour
{
   public void NormalShot(float power,float recoil)
    {
        Debug.Log(power+"の威力だ"+recoil+"の反動だ");
    }
    public void FireShot(float power, float recoil)
    {
        Debug.Log(power + "の威力だ" + recoil + "の反動だ 地面が燃えてるよ");
    }
}
