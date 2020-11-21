using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("collectible start");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
