using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEvent : MonoBehaviour
{
   public void NormalShot(float power,float recoil)
    {
        Debug.Log(power+"�̈З͂�"+recoil+"�̔�����");
    }
    public void FireShot(float power, float recoil)
    {
        Debug.Log(power + "�̈З͂�" + recoil + "�̔����� �n�ʂ��R���Ă��");
    }
}
