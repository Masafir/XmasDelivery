using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        print("a");
    }
    void OnCollisionEnter(Collision collision)
    {
        print("B");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
