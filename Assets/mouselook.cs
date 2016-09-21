using UnityEngine;
using System.Collections;

public class mouselook : MonoBehaviour {



    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float lowLimit = 2;
    public float uperLimit = 10;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;
    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX) ;
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
            SpringJoint hinge = gameObject.GetComponent(typeof(SpringJoint)) as SpringJoint;
            hinge.anchor = hinge.anchor - new Vector3(0,(Input.GetAxis("Mouse Y") * Time.deltaTime),0);
            
            hinge.anchor = new Vector3(hinge.anchor.x, ((hinge.anchor.y<=0?0: hinge.anchor.y)>=uperLimit*.5f?uperLimit*.5f: (hinge.anchor.y <= 0 ? 0 : hinge.anchor.y)), ClampZoom(hinge.anchor.z));
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }
    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        originalRotation = transform.localRotation;
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
    public float ClampZoom(float Z)
    {

        float zoom=-Z;
        float wheel = Input.GetAxis("Mouse ScrollWheel") * 3;
        if (zoom + wheel > uperLimit){
            zoom= 10;
        }else if (zoom + wheel < lowLimit)
        {
            zoom = 2;
        }
        else
        {
            zoom=zoom+wheel;
        }
        print(wheel);
        print(zoom);
        return -zoom;
    }
}
