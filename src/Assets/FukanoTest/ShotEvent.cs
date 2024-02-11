using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEvent : MonoBehaviour
{
   public void NormalShot(float power,float recoil)
    {
        Debug.Log(power+"‚ÌˆÐ—Í‚¾"+recoil+"‚Ì”½“®‚¾");
    }
    public void FireShot(float power, float recoil)
    {
        Debug.Log(power + "‚ÌˆÐ—Í‚¾" + recoil + "‚Ì”½“®‚¾ ’n–Ê‚ª”R‚¦‚Ä‚é‚æ");
    }
}
