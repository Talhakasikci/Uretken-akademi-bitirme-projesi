using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public WheelColliders colliders;
    public WheelMesh meshes;
    public float gasInput;
    public float steerinInput;
    public float breakPoower;
    private float slipAngel;
    public float motorSpeed;
    private float speed;
    private float breakInput;
    private Rigidbody playerRb;
    public AnimationCurve SteeringCurve;
    public int lapControl = 0;
    private GameObject collidedObject;
    public GameStart gameStart;
    private Quaternion initialRotation;
    public AudioSource crash;
    




    private void Start()
    {
        initialRotation = transform.rotation;
        playerRb = gameObject.GetComponent<Rigidbody>();
        gameStart = FindObjectOfType<GameStart>(); // CarController bileþenini bulur
        if (gameStart == null)
        {
            Debug.LogError("CarController component not found!");
        }
    }
    private void FixedUpdate()
    {
        
        speed = playerRb.velocity.magnitude;
        bool isGameStart = gameStart.isGameStart;

        if (isGameStart)
        {
            //getInputs();
                carMove();
                ApplyWheels();
                ApplyBreak();
                ApplySteering();
        }
        if(isGameStart && Input.GetKey(KeyCode.R))
        {
            transform.rotation = initialRotation;
        }
      


        
    }
    private void OnTriggerEnter(Collider other)
    {
        string sahneAdi = SceneManager.GetActiveScene().name;
        if (other.gameObject.CompareTag("Finish"))
        {

            lapControl++;
            Debug.Log("Lap Count: " + lapControl);

            if (sahneAdi == "track1"||sahneAdi == "track2")
            {
                collidedObject = other.gameObject; // Çarpýþma anýndaki nesne referansýný sakla

                collidedObject.SetActive(false); 
                
                Invoke("ActivateFinishObject", 3f);
            }else
            {
                crash.Play();
            }
        }
        
    }

    
    private void ActivateFinishObject()
    {
        if (collidedObject != null)
        {
            collidedObject.SetActive(true); // Referanstaki nesneyi tekrar görünür yap
        }
    }

    void ApplySteering()
    {
        float steeringAngle = steerinInput * SteeringCurve.Evaluate(speed);
        colliders.FRwhell.steerAngle = steeringAngle;
        colliders.FLwhell.steerAngle = steeringAngle;
    }

    void getInputs()    
    {
        gasInput = Input.GetAxis("Vertical");
        steerinInput = Input.GetAxis("Horizontal");
        slipAngel = Vector3.Angle(transform.forward,playerRb.velocity-transform.forward);

        if (slipAngel < 120f)
        {
            if (gasInput < 0)
            {
                breakInput = Mathf.Abs(gasInput);
                gasInput = 0;
                //ApplyBreak();

            }
            else
            {
                breakInput = 0;
            }
        }
        else
        {
            breakInput = 0;
        }

       


    }
    void ApplyBreak()
    {
        colliders.FRwhell.brakeTorque = breakInput*breakPoower*0.7f;
        colliders.FLwhell.brakeTorque = breakInput * breakPoower * 0.7f;
        colliders.RRwhell.brakeTorque = breakInput * breakPoower * 0.3f;
        colliders.RLwhell.brakeTorque = breakInput * breakPoower * 0.3f;



    }
    void carMove()
    {
        getInputs();
        colliders.RLwhell.motorTorque = gasInput * motorSpeed;
        colliders.RRwhell.motorTorque = gasInput * motorSpeed;
    }

    void ApplyWheels()
    {
        UpdateWheels(colliders.FLwhell, meshes.FLwhell);
        UpdateWheels(colliders.FRwhell, meshes.FRwhell);
        UpdateWheels(colliders.RRwhell, meshes.RRwhell);
        UpdateWheels(colliders.RLwhell, meshes.RLwhell);
    }
    void UpdateWheels(WheelCollider coll, MeshRenderer WheelMesh)
    {
        Quaternion quat;
        Vector3 position;

        coll.GetWorldPose(out position, out quat);
        WheelMesh.transform.position = position;    
        WheelMesh.transform.rotation = quat;
    }
    
}
   


[System.Serializable]
    public class WheelColliders
    {
        public WheelCollider FLwhell;
        public WheelCollider FRwhell;
        public WheelCollider RLwhell;
        public WheelCollider RRwhell;
    }

    [System.Serializable]
    public class WheelMesh
    {
        public MeshRenderer FLwhell;
        public MeshRenderer FRwhell;
        public MeshRenderer RLwhell;
        public MeshRenderer RRwhell;
    }
