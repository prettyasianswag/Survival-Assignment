using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Test : MonoBehaviour
{
    public float checkDelay = 1f;

    private float checkTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Simulate test first
        Simulate();

        // Check for the check function
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkDelay)
        {
            Check();
            checkTimer = 0;
        }

        // Perform debugging
        Debug();
    }

    public virtual void Debug() { } // For GizmosGL to perform debugging
    public virtual void Simulate() { } // Run once per frame
    public abstract void Check(); // Perform checks to see whether a test has succeeded or failed
}