using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float dashForce = 4f;
    public float movementSpeed = 27.88f;
    public float jumpForce = 135.7f;
    public bool isGrounded = false;
    private bool isA = false;
    private bool isB = false;
    private bool isX = false;
    private bool isY = false;
    private Rigidbody2D rb;
    private float horizontal = 0;
    private int doubleJumpsLeft;
    public int maxDoubleJumps = 3;
    public float lastRunImpulse;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

		// resetRigidBody();
        resetDoubleJumps();
    }

    // Update is called 50 times per second (stable)
    void FixedUpdate()
    {

        // Ground Control
        if (isGrounded) {
            //if (isA && doubleJumpsLeft > 0) jump();
        } else {
			// In Air
            move(lastRunImpulse * 0.1f);
			// Debug.Log(lastRunImpulse + " : " + horizontal + " : " + (lastRunImpulse - horizontal));
            //if (Math.Abs((lastRunImpulse - horizontal)) > 0) Debug.Log("=== STOP ===");
			
			//move(horizontal * 0.4f);
        }

        // Enable Dash
        if (isX) {
			Vector3 v3 = transform.position;
			v3.x += pointingDirection() * dashForce;
			transform.position = v3;
		}

        // Flip Sprite on Direction change
        if (horizontal > 0) {
            // Looking right
            sprite.flipX = false;
        } else if (horizontal < 0) {
            // Looking left
            sprite.flipX = true;
        }
    }

    // Trigger on any Collision with a 2D Collider
    void OnCollisionEnter2D(Collision2D col) 
    {
        if ("Player" == col.otherRigidbody.tag && "Ground" == col.gameObject.tag) {
            isGrounded = true;
            resetDoubleJumps();
        }
    }

    // Trigger on any exit of previous collision
    void OnCollisionExit2D(Collision2D col) 
    {
        if ("Player" == col.otherRigidbody.tag && "Ground" == col.gameObject.tag) isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch controller axis
        horizontal = Input.GetAxis("Horizontal");

        isY = Input.GetButtonDown("Jump");
        isB = Input.GetButtonDown("Fire2");
        isX = Input.GetButtonDown("Fire3");
        isA = Input.GetButtonDown("Fire1");

        if (isA && !isGrounded && doubleJumpsLeft > 0) jump();
        if (isGrounded) {
			lastRunImpulse = horizontal;

			if (isA) jump();
		}

		move(horizontal);
    }

    // Reset amount of available double jumps to initial/maximal state
    private void resetDoubleJumps() {
        doubleJumpsLeft = maxDoubleJumps;
    }

    // Helper function to get distance between to object
    private float calculateDeltaY(GameObject a, GameObject b) {
        return Math.Abs(a.transform.position.y - b.transform.position.y);
    }
    private float calculateDeltaY(Rigidbody2D a, GameObject b) {
        return Math.Abs(a.transform.position.y - b.transform.position.y);
    }

    // Move player character
    private void move(float axis) {
        if (
            axis == 0f
        ) return;

        transform.position += new Vector3(axis, 0) * Time.fixedDeltaTime * movementSpeed * 1;
    }

    // Jump and remove one double jump token
    private void jump() {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        doubleJumpsLeft -= 1;
    }

    // Direction player is looking at
    private int pointingDirection() {
        return sprite.flipX ? -1 : 1;
    }

	private void resetRigidBody() 
	{
		rb.mass = 3.13f;
		rb.gravityScale = 9.56f;
	}
}
