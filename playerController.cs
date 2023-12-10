using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    // SET UP VARIABLES
    // Public
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;


    // Vector2 is a variable that can hold 2 numbers like an x and y
    Vector2 movementInput;
    // Rigid Body, this is the 
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    // List of collisions
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // This function is to make it so that the sprite slides diagonally around a collision even if it hits it on one axis
    // Like the update function but will with a fixed interval of time
    private void FixedUpdate(){
        if (canMove) {
            // if input is not 0 then try to move
            if (movementInput != Vector2.zero) {
                bool success = TryMove(movementInput);
                // If it doesn't work then check for the one direction
                if (!success && movementInput.x != 0) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    
                }
                // Check for other direction
                if (!success && movementInput.y != 0) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                // Set the animation booleans
                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }

            // Change the direction of the sprite for left and right
            if (movementInput.x < 0) {
                spriteRenderer.flipX = true;
                swordAttack.attackDirection = SwordAttack.AttackDirection.left;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
                swordAttack.attackDirection = SwordAttack.AttackDirection.right;
            }
            // Change the direction of the sprite for up and down
            if (movementInput.y < 0 && movementInput.x == 0) {
                animator.SetBool("isMovingDown", true);
                swordAttack.attackDirection = SwordAttack.AttackDirection.down;
            } else if (movementInput.y > 0 && movementInput.x == 0) {
                animator.SetBool("isMovingUp", true);
                swordAttack.attackDirection = SwordAttack.AttackDirection.up;
            } else {
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
            }
        }
    }

    // Test if it can move the sprite or if there is a collision
    private bool TryMove(Vector2 direction){
        if (direction != Vector2.zero){
            // Check for collisions
            int count = rb.Cast(
                direction, // x and y values that represent a direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions after the cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset // The amount to cast equal to the movement plus the offset
            );
            // Move the sprite if there is no collision
            if (count == 0) {
                rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
    //On move script
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }

    // Sword attack
    void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    // Locking the movement while attacking
    public void LockMovement() {
        canMove = false;
        swordAttack.Attack();
    }

    public void UnlockMovement() {
        canMove = true;
        swordAttack.AttackStop();
    }



}
