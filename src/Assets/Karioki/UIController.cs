using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject UIobject;
    private bool Rightbutton;
    private void CroseUIWindows()
    {
        UIobject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void OnRightbutton(InputAction.CallbackContext context)
    {
        var isTrigger = context.ReadValueAsButton();
        Rightbutton = isTrigger;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Rightbutton);
        if(Rightbutton) { CroseUIWindows(); }
    }

    
}
