using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 5000f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)){ StartThrusting(); }
        else { StopThrusting(); }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A)) { RotateLeft(); }
        else if (Input.GetKey(KeyCode.D)) { RotateRight(); }
        else { StopRotation(); }
    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) { audioSource.PlayOneShot(mainEngine); }
        if (!mainBoosterParticles.isPlaying) { mainBoosterParticles.Play(); }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!leftBoosterParticles.isPlaying) { leftBoosterParticles.Play(); }
    }
    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!rightBoosterParticles.isPlaying) { rightBoosterParticles.Play(); }
    }

    private void StopRotation()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation to avoid conflicts with objects
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
