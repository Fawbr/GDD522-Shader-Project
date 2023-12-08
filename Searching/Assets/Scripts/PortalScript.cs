using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform portal;
    float teleportOffset;
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {
            if (gameObject.name == "Portal")
            {
                teleportOffset = -1f;
            }
            else
            {
                teleportOffset = +1f;
            }
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = new Vector3(portal.transform.position.x + teleportOffset, player.transform.position.y, player.transform.position.z);
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}
