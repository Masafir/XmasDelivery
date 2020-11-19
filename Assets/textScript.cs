using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textScript : MonoBehaviour
{
    GameObject bag;
    public string gameObjectTag;
    GameObject car;
    TextMeshProUGUI text;
    int Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        print(gameObjectTag);
        bag = GameObject.FindGameObjectWithTag(gameObjectTag);
        print(bag);
        //car = GameObject.FindGameObjectWithTag("Car");
        //text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bag.GetComponentsInChildren<Transform>().Length != Count && gameObject.GetComponent<TextMeshProUGUI>() != null)
        {
            print("change text");
            gameObject.GetComponent<TextMeshProUGUI>().text = "" + (bag.GetComponentsInChildren<Transform>().Length - 1);
            Count = bag.GetComponentsInChildren<Transform>().Length;
        }
            
    }
}
