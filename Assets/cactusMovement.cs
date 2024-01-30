using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactusMovement : MonoBehaviour
{
    public Transform target; // Drag the target GameObject here in the Inspector
    public float rotationSpeed = 5f; // Adjust this value to control the rotation speed
    public float movementSpeed = 5f; // Adjust this value to control the movement speed
    public float jumpForce = 10f; // Adjust this value to control the jump force
    public float jumpTimer = 1f; // Time in seconds before the next jump
    private float currentJumpTime;
    
    [Tooltip("VFX prefab to spawn upon impact")]
    public GameObject ImpactVfx;

    [Tooltip("LifeTime of the VFX before being destroyed")]
    public float ImpactVfxLifetime = 5f;
    private Animator animator;
    
    void Start()
    {
        currentJumpTime = jumpTimer;
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool dancing = false;
        // Activate the animation
        if (animator != null)
        {
            // Get the current value of the boolean parameter
            dancing = animator.GetBool("cactus_dance");
        }

        if (target != null && !dancing) //if dancing shouldn't translate or follow the player
        {
            // Calculate the direction towards the target
            Vector3 directionToTarget = target.position - transform.position;

            // Normalize the direction to get a unit vector
            directionToTarget.Normalize();

            // Calculate the rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate the GameObject towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the GameObject forward
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);


            currentJumpTime -= Time.deltaTime;
            if (currentJumpTime <= 0)
            {
                Jump();
            }
        }

    }

    void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        currentJumpTime = jumpTimer;
    }
    
    void OnCollisionEnter(Collision collision)
    {
       
        // Check if the GameObject was hit by a projectile with the name "Projectile_Blaster"
        if (collision.gameObject.CompareTag("bullet"))
        {
            // impact vfx
            if (ImpactVfx)
            {
                GameObject impactVfxInstance = Instantiate(ImpactVfx, transform.position, transform.rotation);
                if (ImpactVfxLifetime > 0)
                {
                    Destroy(impactVfxInstance.gameObject, ImpactVfxLifetime);
                }
            }
            Destroy(this.gameObject);
        }
    }
    
}
