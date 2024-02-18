using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelManagement : MonoBehaviour
{

    public Image UIfuelFill;
    public Image UIfuelEmpty;

    GameObject player;

    public float fuelAmount;
    float isFull;

    void Start()
    {
        FindItems();
        fuelAmount = player.GetComponent<Rocket>().fuel;
        isFull = fuelAmount;
    }
    void Update()
    {
        fuelAmount = player.GetComponent<Rocket>().fuel;
        Debug.Log("FuelMax: " + isFull + " Fuel Total " + fuelAmount);

        if (fuelAmount != isFull )
        {
            UIfuelFill.fillAmount = UIfuelEmpty.fillAmount-0.1f;
        }
        else 
        {
            UIfuelFill.fillAmount = UIfuelEmpty.fillAmount;
        }
        UIfuelEmpty.fillAmount = fuelAmount/100;
    }

    void FindItems()
    {
        player = GameObject.Find("Player");
    }
}
