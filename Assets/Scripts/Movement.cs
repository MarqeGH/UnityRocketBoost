using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    
    private GameObject playerObj = null;
    AudioSource audioSource;
    Rigidbody player;

    GameObject levelStart = null;

    [SerializeField] float angleSpeed = 30f;
    
    [SerializeField] float rocketSpeed = 30f;


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
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer (){
        tilt = tilt*Time.deltaTime;
        //Quaternion newTilt = Quaternion.Euler(tilt);

         // player.MoveRotation(player.rotation*newTilt);
        player.AddTorque(tilt*angleSpeed, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.Space))
        {
            player.AddForce(transform.up*rocketSpeed);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (!Input.GetKey(KeyCode.Space))
        { 
            audioSource.Pause();
        }
    }

    
    private void FindObjects()
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

