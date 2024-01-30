using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform target; // Drag the target GameObject here in the Inspector
    public float rotationSpeed = 5f; // Adjust this value to control the rotation speed

 // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Calculate the direction towards the target
            Vector3 directionToTarget = target.position - transform.position;

            // Normalize the direction to get a unit vector
            directionToTarget.Normalize();

            // Calculate the rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate the GameObject towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);




        }

    }
}
