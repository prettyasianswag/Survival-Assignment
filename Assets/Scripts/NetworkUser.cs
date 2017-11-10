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
            player = GetComponent<Player>();
            if (!isLocalPlayer)
            {
                cam.enabled = false;
                aListener.enabled = false;
                AssignRemoteLayer();
            }

            RegisterPlayer();
        }

        void RegisterPlayer()
        {
            string ID = "Player " + GetComponent<NetworkIdentity>().netId;
            transform.name = ID;
        }

        void Update()
        {
            // Check if current client has authority over this player
            if (isLocalPlayer)
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

        void AssignRemoteLayer()
        {
            // Setting other spawned player with the remotePlayer layer mask
            gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
        }
    }
}
