using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RPGMovement : MonoBehaviour
{
    GameObject[] rocketClone = new GameObject[100];
    public GameObject Menu=new GameObject("menuFocus");
    public float ForwardSpeed;
    public float BackwardSpeed;
    public float StrafeSpeed;
    public float RotateSpeed;
    public GameObject[] pops;
    CharacterController m_CharacterController;
    Vector3 m_LastPosition;
    Animator m_Animator;
    PhotonView m_PhotonView;
    PhotonTransformView m_TransformView;

    float m_AnimatorSpeed;
    Vector3 m_CurrentMovement;
    float m_CurrentTurnSpeed;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        m_PhotonView = GetComponent<PhotonView>();
        m_TransformView = GetComponent<PhotonTransformView>();
    }

    void Update()
    {
        if (m_PhotonView.isMine == true)
        {
            ResetSpeedValues();

            UpdateRotateMovement();

            UpdateForwardMovement();
            UpdateBackwardMovement();
            UpdateStrafeMovement();
            UpdateTools();
            MoveCharacterController();
            ApplyGravityToCharacterController();

            ApplySynchronizedValues();
        }

        UpdateAnimation();
    }

    void UpdateTools()
    {

        if (Input.GetKey(KeyCode.G))
        {
            Vector3 rig;
            

            for (float i = .01f; i < 8; i += .5f)
            {
                Vector3 focal = Camera.main.transform.forward * 2;
                rig = focal + new Vector3(Mathf.Cos(i) * 1.5f, Mathf.Sin(i) * 1.5f);
                //m_LastPosition = rig;
                Menu.transform.position = focal;
                //Rigidbody rocketClone = (Rigidbody)Instantiate(pops[0], transform.position, transform.rotation);//for mimbus
                rocketClone[(int)(i*10)] = (GameObject)Instantiate(pops[0], Camera.main.transform.position+rig, transform.rotation);//for mimbus
                rocketClone[(int)(i * 10)].transform.parent = Menu.transform;
                Menu.transform.LookAt(Camera.main.transform.position);
            }



        }else{
            for (int i = 1; i < 100; i += 1)
            {
               Destroy(rocketClone[i]);
            }
        }
        if (Input.GetKey(KeyCode.F))
        {




            Rigidbody rocketClone = (Rigidbody)Instantiate(pops[0], transform.position, transform.rotation);//for mimbus



        }


    }
    void UpdateAnimation()
    {
        Vector3 movementVector = transform.position - m_LastPosition;

        float speed = Vector3.Dot(movementVector.normalized, transform.forward);
        float direction = Vector3.Dot(movementVector.normalized, transform.right);

        if (Mathf.Abs(speed) < 0.2f)
        {
            speed = 0f;
        }

        if (speed > 0.6f)
        {
            speed = 1f;
            direction = 0f;
        }

        if (speed >= 0f)
        {
            if (Mathf.Abs(direction) > 0.7f)
            {
                speed = 1f;
            }
        }

        m_AnimatorSpeed = Mathf.MoveTowards(m_AnimatorSpeed, speed, Time.deltaTime * 5f);

        m_Animator.SetFloat("Speed", m_AnimatorSpeed);
        m_Animator.SetFloat("Direction", direction);

        m_LastPosition = transform.position;
    }

    void ResetSpeedValues()
    {
        m_CurrentMovement = Vector3.zero;
        m_CurrentTurnSpeed = 0;
    }

    void ApplySynchronizedValues()
    {
        m_TransformView.SetSynchronizedValues(m_CurrentMovement, m_CurrentTurnSpeed);
    }

    void ApplyGravityToCharacterController()
    {
        m_CharacterController.Move(transform.up * Time.deltaTime * -9.81f);
    }

    void MoveCharacterController()
    {
        m_CharacterController.Move(m_CurrentMovement * Time.deltaTime);
    }

    void UpdateForwardMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0.1f)
        {
            m_CurrentMovement = transform.forward * ForwardSpeed;
        }
    }

    void UpdateBackwardMovement()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetAxisRaw("Vertical") < -0.1f)
        {
            m_CurrentMovement = -transform.forward * BackwardSpeed;
        }
    }

    void UpdateStrafeMovement()
    {
        if (Input.GetKey(KeyCode.Q) == true)
        {
            m_CurrentMovement = -transform.right * StrafeSpeed;
        }

        if (Input.GetKey(KeyCode.E) == true)
        {
            m_CurrentMovement = transform.right * StrafeSpeed;
        }
    }

    void UpdateRotateMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            m_CurrentTurnSpeed = -RotateSpeed;
            transform.Rotate(0.0f, -RotateSpeed * Time.deltaTime, 0.0f);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            m_CurrentTurnSpeed = RotateSpeed;
            transform.Rotate(0.0f, RotateSpeed * Time.deltaTime, 0.0f);
        }
    }
}
