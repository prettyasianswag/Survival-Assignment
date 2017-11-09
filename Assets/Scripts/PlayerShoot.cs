using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class PlayerShoot : NetworkBehaviour
    {
        private const string PLAYER_TAG = "Player";

        public PlayerWeapon weapon;

        [SerializeField]
        private Camera cam;

        [SerializeField]
        private LayerMask mask;

        void Start()
        {
            // IF there is no camera
            if (cam == null)
            {
                // Disable camera
                Debug.LogError("No Camera");
                this.enabled = false;
            }
        }

        void Update()
        {
            // IF player uses left click
            if (Input.GetButtonDown("Fire1"))
            {
                // Fire
                Shoot();
            }
        }

        // Only called on the client, not server side
        [Client]
        void Shoot()
        {
            // Defining the variable of our raycast
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
            {
                // IF we hit an player
                if (hit.collider.tag == PLAYER_TAG)
                {
                    // Show name of player
                    CmdPlayerShot(hit.collider.name);
                }
                // IF we hit an enemy
                if (hit.collider.tag == "Enemy")
                {
                    // Destroy enemy
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        [Command]
        void CmdPlayerShot (string ID)
        {
            // Showing which player has been shot
            Debug.Log(ID + " has been shot.");
        }
    }

}
