using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    MeshRenderer renderer;
    Rigidbody rigidBody;
    [SerializeField] float timeToWait = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToWait)
        {
            rigidBody.useGravity = true;
            renderer.enabled = true;
        }
    }
}
