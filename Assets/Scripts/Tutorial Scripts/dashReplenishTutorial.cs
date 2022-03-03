using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Goes on dashReplenishTutorialTrigger object
 tutorial text for dash replenish will show once player collides with the dashReplenishTutorialTrigger object
 */

public class dashReplenishTutorial : MonoBehaviour
{
    public GameObject uiObject; //reference to the text to be turned on and off
    public Transform p; //reference to player position

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (p.position.x >= 22.1f & p.position.y >= 2.7f)
        {
            Destroy(uiObject);
            Destroy(gameObject);
        }
    }
}
