using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    [RequireComponent(typeof(Player))]
    public class LocalUser : MonoBehaviour
    {
        private Player player;

        // Use this for initialization
        void Start()
        {
            player = GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            player.MouseRotateHorizontal();
            player.MouseRotateVertical();

            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            player.Move(h, v);

            if (Input.GetButtonDown("Jump"))
            {
                player.Jump();
            }
        }
    }
}