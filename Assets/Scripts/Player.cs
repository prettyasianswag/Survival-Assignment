using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class Player : MonoBehaviour
    {
        public float movementSpeed = 10.0f;
        public float rotationSpeed = 10.0f;
        public float jumpHeight = 6.0f;
        public bool isGrounded = false;
        private Rigidbody rigid;

        public Camera cam;
        private float mouseSensitivity = 3f;
        private float minimumY = -20f;
        private float maximumY = 15f;
        float rotationY = 0;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public void Move(float h, float v)
        {
            // Position is equal to the rigid body's position
            Vector3 position = rigid.position;

            // Position when player moves forward and backwards
            position += transform.forward * v * movementSpeed * Time.deltaTime;         

            // Position when player moves left and right
            position += transform.right * h * movementSpeed * Time.deltaTime;

            // Moving the rigid body with the position
            rigid.MovePosition(position);
        }

        // Moving of the mouse from a left to right view
        public void MouseRotateHorizontal()
        {
            // Setting the horizontal input of mouse as rotationX
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            // Setting the rotationX into action for the mouse
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
        }

        // Moving of the moouse from a up to down view
        public void MouseRotateVertical()
        {
            // Setting the input of vertical input of mouse as rotationY
            rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Clamping the minimum and maximum heights the mouse can move before it gets locked (so that player can't look up and backwards in 1 motion)
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            // Setting the rotationY into action for just the camera and not the prefab
            cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }

        // Jumping
        public void Jump()
        {
            // If player is grounded
            if (isGrounded)
            {
                // Add force to jump
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                // Is grounded is false
                isGrounded = false;
            }
        }

        // Colliders
        void OnCollisionEnter (Collision col)
        {
            // If player touches ground
            if (col.gameObject.tag == ("Ground"))
            {
                // Is grounded is true so the player can now jump again
                isGrounded = true;
            }

            // If player collides into an enemy
            if (col.gameObject.tag == ("Enemy"))
            {
                // Gameover
                SceneManager.LoadScene(3);
            }
        }
    }  
}

