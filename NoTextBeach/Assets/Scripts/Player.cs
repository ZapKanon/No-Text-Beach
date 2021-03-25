using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Governs player's following of the mouse cursor
//Author: Kyle Weekley
public class Player : MonoBehaviour
{
    private Vector3 cursorPosition;
    private Vector2 followDirection;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
    }

    //Player follows the cursor at a set speed
    public void FollowCursor()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = transform.position.z;
        //transform.position = Vector3.Lerp(transform.position, cursorPosition, moveSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, cursorPosition, moveSpeed * Time.deltaTime);
    }
}
