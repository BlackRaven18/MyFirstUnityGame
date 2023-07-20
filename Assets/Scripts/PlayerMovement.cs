using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private Animator playerAnimator;

    private float dirX = 0f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running, jumping, falling}

    // Start is called before the first frame update
    private void Start(){
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update(){
        dirX = Input.GetAxisRaw("Horizontal");


        playerRigidbody.velocity = new Vector2(dirX * moveSpeed, playerRigidbody.velocity.y);


        if (Input.GetButtonDown("Jump")){
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
        }

        UpdateAnimation();
    } 

    private void UpdateAnimation()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            playerSpriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            playerSpriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
            playerAnimator.SetBool("running", false);
        }

        if(playerRigidbody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        } 
        else if (playerRigidbody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        playerAnimator.SetInteger("movementState", ((int)state));
    }
}