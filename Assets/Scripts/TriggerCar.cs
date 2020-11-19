using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class TriggerCar : MonoBehaviour
{
    
    private GameObject santaClaus;
    private GameObject car;
    public Camera camSantaClaus;
    public Camera camCar;
    private bool isInCar;
    private bool isNearCar;
    private Vector3 stoppedPosition;
    private CarAudio carAudio;
    private CarUserControl carScript;

    private void Start()
    {
        santaClaus = GameObject.Find("SantaClaus");
        car = GameObject.Find("Car");

        // disable car script
        carScript = car.GetComponent<CarUserControl>();
        carAudio = car.GetComponent<CarAudio>();
        carScript.enabled = false;
        carAudio.enabled = false;

        // cameras
        camCar.enabled = false;
        camSantaClaus.enabled = true;

        isInCar = false;
        isNearCar = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        isNearCar = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        isNearCar = false;
    }

    private void Update() {
        // pass from santa to car
        if (isInCar == false && isNearCar == true && Input.GetKeyUp(KeyCode.E)) {
            santaClaus.SetActive(false);
            camCar.enabled = true;
            camSantaClaus.enabled = false;
            carScript.enabled = true;
            isInCar = true;
            carAudio.enabled = true;
        } else

        //pass from car to santa
        if(isInCar == true && Input.GetKeyUp(KeyCode.E)) {
            // stop the car on place where pressed e
            car.SetActive(false);
            car.SetActive(true);

            // show santa
            santaClaus.SetActive(true);
            camCar.enabled = false;
            camSantaClaus.enabled = true;
            carScript.enabled = false;
            carAudio.enabled = false;

            isInCar = false;
            isNearCar = false;
            
            // move character to the car position
            Vector3 carPosition = car.transform.position;
            santaClaus.transform.position = new Vector3(carPosition.x - 1, 0, carPosition.z - 1);
            santaClaus.transform.Rotate(0, 0, 0);
        }
    }
}
