using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    private int HaveItems = 0;

    public int haveItem { get => HaveItems; }

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void GetItem(int Num)
    {
        HaveItems += Num;
    }

    public void UseItem(int Num)
    {
        HaveItems -= Num;
    }

}
