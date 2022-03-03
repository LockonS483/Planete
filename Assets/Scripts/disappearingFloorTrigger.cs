using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappearingFloorTrigger : MonoBehaviour
{
    public GameObject platform; //reference to the platform to be removed

    // Start is called before the first frame update
    void Start()
    {
        platform.SetActive(true);
        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            platform.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
