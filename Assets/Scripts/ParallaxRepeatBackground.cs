using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxRepeatBackground : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 startPos;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // Establish the default starting position 
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }


        if (transform.position.x < -startPos.x)
        {
            transform.position = startPos;
        }
    }
}
