using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashPFX;
    [SerializeField] ParticleSystem successPFX;

    AudioSource audioSource;

    bool isControllable;
    bool isCollidable;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        isControllable = true;
        isCollidable = true;
    }

    void OnCollisionEnter(Collision other) 
    {
        if (!isCollidable || !isControllable) return;

        switch (other.gameObject.tag)
        {
            case "Respawn":
                // N/A
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        successPFX.Play();
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        GetComponent<Movement>().TryStopBoostersPFX();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        crashPFX.Play();
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().TryStopBoostersPFX();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

}
