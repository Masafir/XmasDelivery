using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
     
public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float breakForce;
    private bool isBreaking = false;
    private float currentbreakForce;
    
    // finds the corresponding visual wheel
    // correctly applies the transform
    
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
        // Debug.Log("speed = " + motor);
        
    }
     
    public void Update()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        Debug.Log(Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            isBreaking = true;
        } else {
            isBreaking = false;
        }
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            if (isBreaking == true) {
                axleInfo.leftWheel.brakeTorque = breakForce;
                axleInfo.rightWheel.brakeTorque = breakForce;
            } else {
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
    }
}