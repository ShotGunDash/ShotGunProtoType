using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetParcButton : MonoBehaviour
{
    [SerializeField] private int ID;
    private Image image;
    private ParcController parcController => ParcController.instance;

    private void OnEnable()
    {
        ID = Random.Range(0, parcController.parcDatas.ParcData.Count);
        image = GetComponent<Image>();
        image.sprite = parcController.parcDatas.ParcData[ID].Image;
    }

    public void Click()
    {
        parcController.SetParc(ID);
        parcController.FinishParcSet();
    }
} 
