using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class houseScript : MonoBehaviour
{
    GameObject[] deliveries;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deliveries = GameObject.FindGameObjectsWithTag("Delivery");
        int count = 0;
        foreach (GameObject delivery in deliveries)
        {
            //print("for the delivery : " + delivery.transform.childCount);
            if (delivery.transform.childCount > 0)
            {
                count++;
            }
        }
        if (gameObject.GetComponent<TextMeshProUGUI>() != null)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = count + " / " + deliveries.Length;
        }
    }
}
