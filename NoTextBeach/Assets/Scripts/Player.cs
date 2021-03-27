using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Governs player's parameters and movement
//Author: Kyle Weekley
public class Player : MonoBehaviour
{
    private Vector3 cursorPosition;
    
    public Collector playerCollector;
    
    //Parameters that are affected by upgrades
    public float moveSpeed;       //Player's movement speed
    public float collectRange;    //Distance from which player can collect trash
    public int collectCapacity;   //Number of trash pieces the player can carry at once

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 4.0f; 
        collectRange = 1.0f;
        collectCapacity = 5;
    }

    // Update is called once per frame
    void Update()
    {

        FollowCursor();
        UpdateCollectorValues();
    }

    //Player follows the cursor at a set speed
    public void FollowCursor()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = transform.position.z;
        //transform.position = Vector3.Lerp(transform.position, cursorPosition, moveSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, cursorPosition, moveSpeed * Time.deltaTime);
    }

    //Set the player's collector to reflect their upgradable parameter values
    //This should be called after the player buys an upgrade
    public void UpdateCollectorValues()
    {
        playerCollector.range = collectRange;
        playerCollector.capacity = collectCapacity;
    }
}
