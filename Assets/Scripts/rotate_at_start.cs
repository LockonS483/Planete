using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rotate_at_start : MonoBehaviour
{
    float rotationsPerMinute = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -6.0f * rotationsPerMinute * Time.deltaTime);

        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(sceneName: "SampleScene");
        }
    }
}
