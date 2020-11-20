using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class finishScript : MonoBehaviour
{
    GameObject[] deliveries;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
    }
    bool IsTheDeliveryFinish()
    {
        bool finished = true;
        foreach (GameObject delivery in deliveries)
        {
            //print("for the delivery : " + delivery.transform.childCount);
            if(delivery.transform.childCount < 1)
            {
                finished = false;
                return false;
            }
        }

        return finished;
    }
    // Update is called once per frame
    void Update()
    {
        deliveries = GameObject.FindGameObjectsWithTag("Delivery");
        if(IsTheDeliveryFinish() && gameObject.GetComponent<TextMeshProUGUI>() != null)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = " Bravo, vous avez réussi à livrer les cadeaux dans les temps ! ";
            button.SetActive(true);


        }

    }
}
