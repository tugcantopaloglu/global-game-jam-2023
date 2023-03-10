using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 3.0f;
    protected float xSpeed = 2.0f;
    protected SpriteRenderer spriteRenderer;
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        
        //Swap sprite direction, wether you're going to right or left
        if (moveDelta.x > 0) { 
            transform.localScale = new Vector3(6, 6, 6);
        }
        else if (moveDelta.x < 0) { 
            transform.localScale = new Vector3(-6, 6, 6);
        }

        //Add push vector, if any
        moveDelta += pushDirection;

        //Reduce the push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //move y
        //Make sure we can move in this direction, by casting a box there first, if the box null, we're free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //Move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        //move x
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //Move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);

        }

    }
}
