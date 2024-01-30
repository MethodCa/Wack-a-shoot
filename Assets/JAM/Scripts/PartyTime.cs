using System;
using UnityEngine;
using UnityEngine.Audio;

public class PartyTime : MonoBehaviour
{
    // Animator reference
    private Animator[] enemyAnimators;
    public Animator boosAnimator;

    // Boolean parameter name in the Animator
    private string booleanParameterName = "cactus_dance";

    // Private AudioSource variable
    private AudioSource musicAudioSource;

    // Timer variables
    private float musicTimer = 0.0f;
    private float musicDuration = 26.0f;
    public float intervalDuration = 120.0f; // 2 minutes
    public GameObject enemyGenerator;
    public AudioMixer mixer;

    private void Start()
    {
        // Find all game objects with the tag "enemy" and get their animators
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
        enemyAnimators = new Animator[enemyObjects.Length];

        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyAnimators[i] = enemyObjects[i].GetComponent<Animator>();
        }

        // Get the AudioSource component attached to the same GameObject
        musicAudioSource = GetComponent<AudioSource>();
        // Ensure AudioSource is present
        if (musicAudioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }

        // Start the music timer
        musicTimer = intervalDuration;
    }
    
    void Update()
    {
        // Update the music timer
        musicTimer -= Time.deltaTime;

        // Check if it's time to play music
        if (musicTimer <= 0.0f)
        {
            // Play the music for the specified duration
            if (musicAudioSource != null)
            {
                musicAudioSource.Play();
                Invoke("StopMusic", musicDuration); // Invoke a method to stop the music after the specified duration
                // Toggle the boolean variable for each enemy
                ToggleBooleanVariableForEnemies();
                
            }

            // Reset the music timer
            musicTimer = intervalDuration;
        }
    }

    // Method to stop the music
    void StopMusic()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
        // Toggle the boolean variable for each enemy
        ToggleBooleanVariableForEnemies();
    }
    
    
    void FindAllEnemies()
    {
        // Find all game objects with the tag "enemy" and get their animators
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("enemy");
        enemyAnimators = new Animator[enemyObjects.Length];

        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyAnimators[i] = enemyObjects[i].GetComponent<Animator>();
        }

        // Get the AudioSource component attached to the same GameObject
        musicAudioSource = GetComponent<AudioSource>();
        // Ensure AudioSource is present
        if (musicAudioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }

        // Start the music timer
        musicTimer = intervalDuration;
    }

    // Method to toggle the boolean variable for all enemy animators
    void ToggleBooleanVariableForEnemies()
    {
        FindAllEnemies();
        // Ensure that there are enemy animators
        if (enemyAnimators != null && enemyAnimators.Length > 0)
        {
            // Loop through each enemy animator
            foreach (Animator enemyAnimator in enemyAnimators)
            {
                // Ensure that the Animator component and the boolean parameter exist
                if (enemyAnimator != null && !string.IsNullOrEmpty(booleanParameterName))
                {
                    // Get the current value of the boolean parameter
                    bool currentValue = enemyAnimator.GetBool(booleanParameterName);

                    // Toggle the value
                    enemyAnimator.SetBool(booleanParameterName, !currentValue);
                }
            }
            if (boosAnimator != null && !string.IsNullOrEmpty("boss_dance"))
            {
                // Get the current value of the boolean parameter
                bool currentValue = boosAnimator.GetBool("boss_dance");

                // Toggle the value
                boosAnimator.SetBool("boss_dance", !currentValue);
                if (enemyGenerator.GetComponent<EnemyGenerator>().maxCactus > 0)
                {
                    enemyGenerator.GetComponent<EnemyGenerator>().maxCactus = 0;
                }
                else
                {
                    enemyGenerator.GetComponent<EnemyGenerator>().maxCactus = 500;
                }
            }
            
        }
    }
}
