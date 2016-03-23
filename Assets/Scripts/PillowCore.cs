using UnityEngine;
using System.Collections;

public class PillowCore : MonoBehaviour {

    public Rigidbody parentsRb; // Reference to main pillow's rigidbody

    AudioSource[] pillowSounds; // Reference to array of pillow sound effects

    Collider previousCollider; // Collider from previous collision
    bool landed; // Whether the pillow has landed

    void Awake()
    {
        previousCollider = null;
        pillowSounds = GetComponents<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        // If the pillow hasn't landed
        if (!landed)
        {
            // If the pillow hasn't collided with the same object twice
            if (col != previousCollider)
            {
                // Play sound
                playPillowSound();

                // Set landed equal to true
                landed = true;

                // Set previous collider to this collider
                previousCollider = col;
            }
        }
    }

    void OnTriggerExit()
    {
        previousCollider = null;
        landed = false;
    }

    // Plays one of the available pillow sounds (chosen randomly)
    void playPillowSound()
    {
        int i = Random.Range(0, pillowSounds.Length);
        pillowSounds[i].Play();
    }
}
