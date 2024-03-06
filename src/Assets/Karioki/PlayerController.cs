using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static Unity.VisualScripting.Member;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{
    [Header("ヒエラルキーからの導入")]

    [SerializeField] private GameObject LeftArrow; 
    [SerializeField] private GameObject RightArrow;
    [SerializeField] private VisualEffect effect;
    [Header("プレハブ外部")]
    [SerializeField] private GameObject UIobject;
    [Space]
    [Header("基本パラメータ")]
    [SerializeField] private float Intarval = 0.4f;
    [SerializeField] private float BulletPower = 2000;
    [SerializeField] private float AirResistance = 4f;
    [SerializeField] private float JumpPower = 0.3f;
    [SerializeField] private float OnGroundDistanse = 1.0f;
    [SerializeField] private float OnGroundSphereScale = 0.3f;
    [SerializeField] private float BrakePower = 0.9f;
    
    private Rigidbody rb;
    private float time = 0;
    private Vector3 LeftMuzzle, RightMuzzle;
    private bool PouseButton,LeftShot, RightShot, LeftSteyTrigger, RightSteyTrigger;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = AirResistance;
    }

  //InputAcsions

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

    public void OnPouseButton(InputAction.CallbackContext context)
    {
        var isTrigger = context.ReadValueAsButton();
        PouseButton = isTrigger;
        OpenPouseUI();
    }

    // 関数

    private void OpenPouseUI()
    {
        UIobject.SetActive(true);
        Time.timeScale= 0f;
    }

    private bool OnGround()
    {
        RaycastHit hit; 
        LayerMask mask = 3 << LayerMask.NameToLayer("Stage");
        bool Trigger = Physics.SphereCast(transform.position, OnGroundSphereScale, Vector3.down, out hit, OnGroundDistanse, mask);
        if (Trigger)Debug.Log(hit.collider.gameObject.name);
        return Trigger;
    }

    //　ギズモ

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * OnGroundDistanse);
        Gizmos.DrawWireSphere(transform.position + Vector3.down * OnGroundDistanse, OnGroundSphereScale);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BulletLineControl(LeftArrow, LeftMuzzle);
        BulletLineControl(RightArrow, RightMuzzle);
        
        bool CheckTriggerBoth = RightSteyTrigger && LeftSteyTrigger;
        bool CheckTriggerOneside = RightSteyTrigger || LeftSteyTrigger;
        time += Time.deltaTime;
        bool CheckIntarval = time > Intarval;
        if (CheckIntarval)
        {
            if (LeftShot)
            {
                IsShot(LeftMuzzle, CheckTriggerBoth);
            }
            if (RightShot)
            {
                IsShot(RightMuzzle, CheckTriggerBoth);
            }
        }

        if (OnGround() && CheckTriggerOneside)
        {
            Vector3 rbForse = rb.velocity;
            rbForse.x *= BrakePower; 
            rbForse.z *= BrakePower;
            rb.velocity = rbForse;
        }
       /* if (Input.GetMouseButtonDown(0) && time > Intarval)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * BulletPower);
            time = 0;
            //source.Play();
        }*/
    }

    void BulletLineControl(GameObject Arrow, Vector3 Muzzle)
    {
        if (Muzzle != Vector3.zero)
        {
            if (!Arrow.activeSelf)
                Arrow.SetActive(true);
            Quaternion look = Muzzle == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(Muzzle);
            Arrow.transform.rotation = look * Quaternion.Euler(90, 0, 0);
        }
        else
        {
            Arrow.SetActive(false);
        }
    }

    void IsShot(Vector3 Muzzle, bool CheckTrigger)
    {
        Vector3  Forward = CheckTrigger ? Vector3.down * JumpPower : Muzzle;
        time = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(-Forward * BulletPower);

        Quaternion look = Muzzle == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(Muzzle);
        effect.transform.rotation = look * Quaternion.Euler(CheckTrigger ? 90 : 0, 0, 0);
        effect.SendEvent("OnPlay");
    }
}
