using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //SET UP VARIABLES
    Animator animator;
    SpriteRenderer spriteRenderer;
    


    public GameObject Player;
    public float speed;
    private Vector2 changePosition;
    private Vector3 lastPosition;
    public bool slimeCanMove = true;
    public float distance;



    public float Health {
        set {
            health = value;
            
            if (health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }


    public float health = 1;


    // On start
    private void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
    }

    // Update function
    public void FixedUpdate() {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance < 1) {
            animator.SetBool("slimeCanMove", true);
            lastPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.fixedDeltaTime);
            changePosition = transform.position - lastPosition;
            if (changePosition.x < 0) {
                spriteRenderer.flipX = true;
            } else if (changePosition.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
    }



    // Make the slime stop moving after jump
    public void SlimeStop() {
        animator.SetBool("slimeCanMove", false);
    }


    // Defeated function
    public void Defeated() {
        animator.SetTrigger("Defeated");
    }



    //Remove the enemy sprite
    public void RemoveEnemy(){
        Destroy(gameObject);
    }
}
