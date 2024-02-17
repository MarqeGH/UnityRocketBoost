using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    GameObject player; 
    AudioSource audioSource;
    AudioSource playerAudio;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip victorySound;
    [SerializeField] float timeToNextLevel = 2f;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    int currentSceneIndex;
    int sceneCount;
    
    bool isCrashed = false;
    bool collisionsDisabled = false;
    
    void Start()
    {
        Debug.Log("Starting timer");
        sceneCount = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        player = GameObject.Find("Player");
        playerAudio = player.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        
        // StartCoroutine(WaitAndPrint()); Start Coroutine w/ IEnumerator WaitAndPrint
    }

    void Update()
    {
        CheatCodes();
        // if (!Input.GetKey(KeyCode.Space) & audioSource.clip != victorySound)
        // { 
        //     audioSource.Pause();
        // }
    }

    void CheatCodes(){
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            HandleCollisions();
        }
    }

    // IEnumerator WaitAndPrint() Return WaitForSeconds value of 5
    // {
    //     yield return new WaitForSeconds(5);
    //     Debug.Log("WaitAndPrint " + Time.time);
    // }

    void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag)
        {
            case "Fuel":
                Destroy(other.gameObject);
                break;
            case "powerUp":
                break;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isCrashed || collisionsDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "LevelEnd":
                VictorySequence();
                break;
            case "LevelStart":
                break;
            default:
                CrashSequence();
                break;
        } 
    } 
    void NextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == sceneCount)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
        // if (!(currentSceneIndex+1 == sceneCount))
        //     SceneManager.LoadScene(currentSceneIndex+1);
        // else {
        //     SceneManager.LoadScene(0);
        // }
    }
    void ResetLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
    void HandleCollisions()
    {
        collisionsDisabled = !collisionsDisabled;
    }

    void VictorySequence()
    {
        isCrashed = true;
        successParticles.Play();
        player.GetComponent<Rocket>().enabled = false;
        playerAudio.Stop();
        audioSource.PlayOneShot(victorySound);
        Invoke("NextLevel", timeToNextLevel);
    }
    void CrashSequence()
    {
        crashParticles.Play();      
        isCrashed = true;
        player.GetComponent<Rocket>().enabled = false;
        playerAudio.Stop();
        audioSource.PlayOneShot(crashSound);
        Invoke("ResetLevel", timeToNextLevel);
    }
}