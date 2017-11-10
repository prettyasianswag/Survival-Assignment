using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

namespace Networking
{
    [RequireComponent(typeof(Player))]
    public class NetworkUser : NetworkBehaviour
    {
        public Camera cam;
        public AudioListener aListener;
        private Player player;
        private PlayerShoot PS;

        [SerializeField]
        string remoteLayerName = "RemotePlayer";

        // Use this for initialization
        void Start()
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;

            player = GetComponent<Player>();
            // If it is not the local player on the network
            if (!isLocalPlayer)
            {
                // Disable camera
                cam.enabled = false;
                // Disable audio listener
                aListener.enabled = false;
                // Assign the layer mask
                AssignRemoteLayer();
            }
            // Register the player into the server
            RegisterPlayer();
        }

        void RegisterPlayer()
        {
            // Creating a unique ID for player on the server
            string ID = "Player " + GetComponent<NetworkIdentity>().netId;
            transform.name = ID;
        }

        void Update()
        {
            // If it is the local player
            if (isLocalPlayer)
            {
                // IF player presses escape, unlock cursor
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
                // IF player presses w, lock cursor
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }

                // Allow the player to move and jump
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

        void AssignRemoteLayer()
        {
            // Setting other spawned player with the remotePlayer layer mask
            gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
        }
    }
}
