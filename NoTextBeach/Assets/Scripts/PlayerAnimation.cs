using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public enum State
    {
        Idle,
        Walk,
        Carry
    }
    public enum DirectionState
    {
        Up,
        Down,
        Left,
        Right
    }

    private SpriteRenderer sprite;
    private Animator anim;
    private State state;
    private DirectionState dirState;

    public DirectionState DirState
    {
        get { return dirState; }
    }
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        state = State.Idle;
        dirState = DirectionState.Down;
    }

    // Update is called once per frame
    void Update()
    {
        if (dirState == DirectionState.Right) //Flip sprite when facing right
            sprite.flipX = true;
        else
            sprite.flipX = false;

        if (state == State.Idle) //Player is Idle
        {
            if (dirState == DirectionState.Down)
                anim.Play("Idle_Down");
            else if (dirState == DirectionState.Left || dirState == DirectionState.Right)
                anim.Play("Idle_Side");
            else if (dirState == DirectionState.Up)
                anim.Play("Idle_Up");
        }
        else if (state == State.Carry) //Player is Carrying Trash
        {
            if (dirState == DirectionState.Down)
                anim.Play("Carry_Down");
            else if (dirState == DirectionState.Left || dirState == DirectionState.Right)
                anim.Play("Carry_Side");
            else if (dirState == DirectionState.Up)
                anim.Play("Carry_Up");
        }

        else if (state == State.Walk) //Player is Walking
        {
            if (dirState == DirectionState.Down)
                anim.Play("Walk_Down");
            else if (dirState == DirectionState.Left || dirState == DirectionState.Right)
                anim.Play("Walk_Side");
            else if (dirState == DirectionState.Up)
                anim.Play("Walk_Up");
        }
    }

    public void UpdateState(Vector3 position, Vector3 target, float carrying) //Updates the player's Animation state based on movement
    {
        Vector3 direction = target - position;

        if (carrying > 0) //Player is carrying trash
            state = State.Carry;
        else if (direction == Vector3.zero) //Player is standing still
            state = State.Idle;
        else //Player is moving
            state = State.Walk;

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x)) //Player is facing up or down
        {
            if (direction.y < 0) //Player is facing down
                dirState = DirectionState.Down;
            else if (direction.y > 0) //Player is facing up
                dirState = DirectionState.Up;
        }
        else if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //Player is facing left or right
        {
            if (direction.x > 0) //Player is facing right
                dirState = DirectionState.Right;
            else if (direction.x < 0) //Player is facing left
                dirState = DirectionState.Left;
        }
    }
}
