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
    Vector3 tilt;
    Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        FindObjects();
        SpawnPlayer();
    }

    

    // Update is called once per frame
    void Update()
    {
        tilt = new Vector3(0, 0, -Input.GetAxisRaw("Horizontal"));
        PlayerEffects();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }


    void PlayerEffects()
    {
        HorizontalBoost();
        if (Input.GetKey(KeyCode.Space))
        {
            ThrusterEngage();
            isBoosting = true;
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            ThrusterDisable();
            isBoosting = false;
        }
    }
    void MovePlayer()
    {
        tilt = tilt * Time.deltaTime;
        //Quaternion newTilt = Quaternion.Euler(tilt);
        if(isBoosting)
        {
            player.AddForce(transform.up * rocketSpeed);
        }
        // player.MoveRotation(player.rotation*newTilt);
        player.AddTorque(tilt * angleSpeed, ForceMode.Acceleration);
    }
    void HorizontalBoost()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rightBoost.Emit(1000);
        }
        else
        {
            rightBoost.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            leftBoost.Emit(100);
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
            item.Emit(100);
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

