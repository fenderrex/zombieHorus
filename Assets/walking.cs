
using UnityEngine;
using System.Collections;

public class walking : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Quaternion rot = transform.rotation; ;
        //if (controller.isGrounded)
        //{
            //float Angle = Quaternion.Angle(Quaternion.Euler(new Vector3(0, 0, 0)), transform.rotation);
            //rot = transform.rotation;
       
        if (Input.GetKey("w"))
            transform.Translate(transform.forward * speed*Time.deltaTime, Space.Self);
      
        if (Input.GetKey("s"))
            transform.Translate(transform.forward * -speed*Time.deltaTime, Space.Self);
        //transform.Translate(transform.up* -gravity*Time.deltaTime, Space.World);//todo get mass... gravity
        // if (Input.GetButton("jump"))
        //    transform.Translate(Vector3.up * Time.deltaTime,Space.World);

        //}
        //transform.position = transform.forward*1;

    }
}
