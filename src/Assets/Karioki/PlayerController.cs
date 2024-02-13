using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("ヒエラルキーからの導入")]

    [SerializeField] private GameObject LeftArrow; 
    [SerializeField] private GameObject RightArrow;
    [Space]
    [Header("基本パラメータ")]
    [SerializeField] private float Intarval = 0.4f;
    [SerializeField] private float BulletPower = 2000;
    [SerializeField] private float AirResistance = 4f;
    [SerializeField] private float JumpPower = 0.3f;

    private Rigidbody rb;
    private float time = 0;
    private Vector3 LeftMuzzle, RightMuzzle;
    private bool LeftShot, RightShot, LeftSteyTrigger, RightSteyTrigger;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = AirResistance;
    }

    public void OnLeftStick(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        LeftMuzzle = new Vector3(axis.x, 0, axis.y);
    }
    public void OnRightStick(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        RightMuzzle = new Vector3(axis.x, 0, axis.y);
    }

    public void OnLeftShot(InputAction.CallbackContext context)
    {
        var isTrigger = context.ReadValueAsButton();
        LeftShot = isTrigger;
    }
    public void OnRightShot(InputAction.CallbackContext context)
    {
        var isTrigger = context.ReadValueAsButton();
        RightShot = isTrigger;
    }

    public void OnLeftSteyTrigger(InputAction.CallbackContext context)
    {
        var isTrigger = context.ReadValueAsButton();
        LeftSteyTrigger = isTrigger;
    }
    public void OnRightSteyTrigger(InputAction.CallbackContext context)
    {
        var isTrigger = context.ReadValueAsButton();
        RightSteyTrigger = isTrigger;
    }









    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            V2 = 1;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            V2 = -1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            H2 = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            H2 = 1;
        }
          if (H2 != 0 || V2 != 0)
        {
            Vector3 direction = new Vector3(H2, 0, V2);
            transform.localRotation = Quaternion.LookRotation(direction);
        }
        */
       
        if (LeftMuzzle != Vector3.zero) 
        {
            if (!LeftArrow.activeSelf)
                LeftArrow.SetActive(true);
            LeftArrow.transform.rotation = Quaternion.LookRotation(LeftMuzzle) * Quaternion.Euler(90, 0, 0);
        }
        else
        {
            LeftArrow.SetActive(false);
        }

        if (RightMuzzle != Vector3.zero)
        {
            if (!RightArrow.activeSelf)
                RightArrow.SetActive(true);
            RightArrow.transform.rotation = Quaternion.LookRotation(RightMuzzle) * Quaternion.Euler(90, 0, 0);
        }
        else
        {
            RightArrow.SetActive(false);
        }
        
        bool CheckTrigger = RightSteyTrigger && LeftSteyTrigger;
        
        time += Time.deltaTime;
        bool CheckIntarval = time > Intarval;

        if (CheckIntarval)
        {
            if (LeftShot)
            {
                IsShot(LeftMuzzle, CheckTrigger);
            }
            if (RightShot)
            {
                IsShot(RightMuzzle, CheckTrigger);
            }
        }
       /* if (Input.GetMouseButtonDown(0) && time > Intarval)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * BulletPower);
            time = 0;
            //source.Play();
        }*/
    }

    void IsShot(Vector3 Muzzle, bool CheckTrigger)
    {
        Vector3  Forward = CheckTrigger ? Vector3.down * JumpPower : Muzzle;
        time = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(-Forward * BulletPower);
    }
}
