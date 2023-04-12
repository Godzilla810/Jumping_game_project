using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 1000;
    private int maxJumpTimes = 2;
    private int jumpCounter = 0;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier; 
    }

    // Update is called once per frame
    void Update()
    {
        //跳躍功能
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter!=maxJumpTimes && !gameOver){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            jumpCounter += 1;
            jumpForce = 500;
        }
        if (Input.GetKey("d")){
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_f", 2.0f);
        }
        else if (doubleSpeed){
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_f", 1.0f);
        }
        
    }
    private void OnCollisionEnter(Collision collision){
        //偵測落地
        if (collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
            dirtParticle.Play();
            jumpCounter = 0;
            jumpForce = 1000;
        }
        //偵測碰撞
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver){
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        }
    }
}
