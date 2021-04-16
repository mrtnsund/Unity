using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sceneDelay = 1f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    AudioSource audioSource;

    bool isTransitioning;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        PlaySound(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", sceneDelay);
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        PlaySound(successSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", sceneDelay);
    }
    void ReloadScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
    void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = activeSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void PlaySound(AudioClip sfx)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sfx);
    }
}
