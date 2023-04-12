using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private float dashSpeed = 60;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //一個相對位移的概念
        if (playerControllerScript.gameOver == false){
            if (playerControllerScript.doubleSpeed){
                transform.Translate(Vector3.left * Time.deltaTime * dashSpeed);
            }
            else {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            
        }
        //刪除畫面外的障礙物
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle")){
            Destroy(gameObject);
        }        
    }
}
