using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3 position = rigid.position;

            position += transform.forward * v * movementSpeed * Time.deltaTime;         
            position += transform.right * h * movementSpeed * Time.deltaTime;

            rigid.MovePosition(position);
        }

        public void MouseRotateHorizontal()
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            transform.localEulerAngles = new Vector3(0, rotationX, 0);
        }

        public void MouseRotateVertical()
        {
            rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }

        public void Jump()
        {
            if (isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        void OnCollisionEnter (Collision col)
        {
            if (col.gameObject.tag == ("Ground"))
            {
                isGrounded = true;
            }
        }
    }  
}

