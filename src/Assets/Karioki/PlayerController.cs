using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerController : MonoBehaviour
{
    public float BulletPower = 2000;
    Rigidbody rb;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float H2 = 0, V2 = 0;

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

        if (Input.GetMouseButtonDown(0) && time > 0.2)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * BulletPower);
            time = 0;
            //source.Play();
        }
        time += Time.deltaTime;
    }
}
