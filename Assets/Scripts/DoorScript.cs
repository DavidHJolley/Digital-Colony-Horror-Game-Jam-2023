using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float smooth = 2.0f; // Determines the speed at which the door opens and closes
    public float openAngle = 90.0f; // Determines the angle at which the door opens
    public float closeAngle = 0.0f; // Determines the angle at which the door closes
    public float interactDistance = 2.0f; // Determines the distance at which the player can interact with the door
    public float x ;
    public float z;

    private bool isOpen = false; // Keeps track of whether the door is currently open or closed

    void Update()
    {
        // Check if the player is within range of the door
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (distanceToPlayer <= interactDistance)
        {
            // Check if the player presses the interact key (default is E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Toggle the door open or closed
                isOpen = !isOpen;
            }
        }

        // Determine the target angle for the door based on whether it is open or closed
        float targetAngle = isOpen ? openAngle : closeAngle;

        // Smoothly rotate the door to the target angle using Lerp
        Quaternion targetRotation = Quaternion.Euler(x, targetAngle, z);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
