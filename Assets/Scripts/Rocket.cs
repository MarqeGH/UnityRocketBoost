using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    
    private GameObject playerObj = null;
    Rigidbody player;

    public GameObject levelStart = null;
    GameObject levelEnd = null;

    [SerializeField] float angleSpeed = 30f;
    
    [SerializeField] float rocketSpeed = 30f;
    int bugCounter = 0;


    Vector3 tilt;
    Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }
        player = playerObj.GetComponent<Rigidbody>();

        if (levelStart == null)
        {
            levelStart = GameObject.Find("LaunchPad");
        }
        if (levelEnd == null)
        {
            levelEnd = GameObject.Find("LandingPad");
        }

        startPosition = levelStart.transform.position;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        tilt = new Vector3(0, 0, -Input.GetAxisRaw("Horizontal"));
        if (Input.GetKeyDown("r"))
        {
            ResetLevel();
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer (){
        tilt = tilt*Time.deltaTime;
        Quaternion newTilt = Quaternion.Euler(tilt);

         // player.MoveRotation(player.rotation*newTilt);
        player.AddTorque(tilt*angleSpeed, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.Space))
        {
            player.AddForce(transform.up*rocketSpeed);
            Debug.Log("You are creating space");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Hazard")
        {
            ResetLevel();
        }
        if (other.gameObject.name == "LandingPad")
        {
            NextLevel();
        }
    } 
    

    void ResetLevel()
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

    public void NextLevel()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void SpawnPlayer()
    {
        player.transform.position = startPosition + new Vector3(0, 10, 0);
    }


}

