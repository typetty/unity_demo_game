using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    //SET UP VARIABLES
    public Collider2D swordCollider;
    public float damage = 3;
    public enum AttackDirection {
        left, 
        right, 
        up, 
        down
    }
    public AttackDirection attackDirection;
    
    Vector2 rightAttackOffset;


    // On start
    private void Start() {
        // Set the offset of the sword collider box equal to what it is when the game loads
        rightAttackOffset = transform.position;
    }

    

    //Function to determine the direction of attack
    public void Attack() {
        
        switch(attackDirection) {
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
            case AttackDirection.up:
                AttackUp();
                break;
            case AttackDirection.down:
                AttackDown();
                break;
        //print(transform.localPosition);
        }
    }


    // Attack right
    private void AttackRight() {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }
    // Attack left
    private void AttackLeft() {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }
    // Attack right
    private void AttackDown() {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(0.02f, -0.14f);
    }
    // Attack right
    private void AttackUp() {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(0f, -0.08f);
    }
    // Attack stop the attack
    public void AttackStop() {
        swordCollider.enabled = false;
    }

    // Deal damage to enemy
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            // Deal damage
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.Health -= damage;
            }
        }
    }
}
