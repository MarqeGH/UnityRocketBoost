using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class FuelManagement : MonoBehaviour
{

    public Image UIfuelFill;
    public Image UIfuelEmpty;

    GameObject focusObject;

    GameObject player;

    float fuelAmount;
    float isFull;

    void Start()
    {
        FindItems();
        focusObject = GameObject.Find("Main Camera");
        fuelAmount = player.GetComponent<Rocket>().fuel;
        isFull = fuelAmount;
    }
    void Update()
    {
        FuelManager();
    }

    private void FuelManager()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - focusObject.transform.position);
        fuelAmount = player.GetComponent<Rocket>().fuel;

        if (fuelAmount == isFull)
        {
            UIfuelEmpty.enabled = false;
            UIfuelFill.enabled = false;
        }
        else 
        {
            UIfuelEmpty.enabled = true;
            UIfuelFill.enabled = true;
        }
        UIfuelEmpty.fillAmount = fuelAmount / isFull;
        UIfuelFill.fillAmount = UIfuelEmpty.fillAmount - 0.05f;
    }

    void FindItems()
    {
        player = GameObject.Find("Player");
    }
}
