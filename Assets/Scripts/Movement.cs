using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    
    private GameObject playerObj = null;
    AudioSource audioSource;
    Rigidbody player;
    GameObject levelStart = null;
    [SerializeField] ParticleSystem rightBoost;
    [SerializeField] ParticleSystem leftBoost;
    [SerializeField] ParticleSystem[] mainBoosters;

    [SerializeField] float angleSpeed = 30f;
    [SerializeField] float rocketSpeed = 30f;
    bool isBoosting;
    bool boostDisabled = false;
    Vector3 tilt;
    Vector3 startPosition;

    [SerializeField] float fuelCap = 100f;
    float fuelHalf;
    [SerializeField] float fuelFill = 40f;
    [SerializeField] float fuelCost = 25f;
    
    public float fuel;

    // Start is called before the first frame update
    void Start()
    {
        FindObjects();
        SpawnPlayer();
        fuel = fuelCap;
        fuelHalf = fuelCap/2;
    }

    

    // Update is called once per frame
    void Update()
    {
        tilt = new Vector3(0, 0, -Input.GetAxisRaw("Horizontal"));
        PlayerEffects();
        FuelLevels();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //Quaternion newTilt = Quaternion.Euler(tilt);
        if(isBoosting && !boostDisabled)
        {
            player.AddForce(transform.up * rocketSpeed);
        }
        // player.MoveRotation(player.rotation*newTilt);
        player.AddTorque(tilt*Time.deltaTime * angleSpeed, ForceMode.Acceleration);
    }
    void PlayerEffects()
    {
        HorizontalBoost();
        if (Input.GetKey(KeyCode.Space) && !boostDisabled)
        {
            ThrusterEngage();
            isBoosting = true;
        }
        if (!Input.GetKey(KeyCode.Space) || boostDisabled)
        {
            ThrusterDisable();
            isBoosting = false;
        }
    }

    void FuelLevels()
    {
        if (isBoosting && !boostDisabled)
        {
            fuel -= fuelCost * Time.deltaTime;
        }
        else
        {
            fuel += fuelFill * Time.deltaTime;
        }
        fuel = Mathf.Clamp(fuel, 0f, fuelCap);
        RefillEmpty();
    }

    private void RefillEmpty()
    {
        if (fuel <= 0)
        {
            boostDisabled = true;
        }
        else if (fuel >= fuelHalf)
        {
            boostDisabled = false;
        }
    }
    void HorizontalBoost()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rightBoost.Emit(1);
        }
        else
        {
            rightBoost.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            leftBoost.Emit(1);
        }
        else
        {
            leftBoost.Stop();
        }
    }
    void ThrusterEngage()
    {
        foreach (ParticleSystem item in mainBoosters)
        {
            item.Emit(1);
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void ThrusterDisable()
    {
        audioSource.Pause();
        foreach (ParticleSystem item in mainBoosters)
        {
            item.Stop();
        }
    }

    void FindObjects()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }
        player = playerObj.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (levelStart == null)
        {
            levelStart = GameObject.Find("LaunchPad");
            startPosition = levelStart.transform.position;
        }
    }

    void SpawnPlayer()
    {
        player.transform.position = startPosition + new Vector3(0, 10, 0);
    }


}

