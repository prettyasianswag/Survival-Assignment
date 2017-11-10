using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class PlayerShootOffline : MonoBehaviour
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

            if (Input.GetButtonDown("Fire2"))
            {
                StartCoroutine(BurstShoot());
            }
        }

        // Only called on the client, not server side
        void Shoot()
        {
            // Defining the variable of our raycast
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
            {
                // IF we hit an enemy
                if (hit.collider.tag == "Enemy")
                {
                    // Destroy enemy
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        IEnumerator BurstShoot()
        {
            // Defining the variable of our raycast
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
            {
                // IF we hit an enemy
                if (hit.collider.tag == "Enemy")
                {
                    // Destroy enemy
                    Destroy(hit.collider.gameObject);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
