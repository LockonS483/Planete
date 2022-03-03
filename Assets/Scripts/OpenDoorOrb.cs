using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOrb : MonoBehaviour
{
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            door.SetActive(false);
            Destroy(door);
            Destroy(gameObject);
        }
    }

}
