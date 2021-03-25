using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Collects all Collectable objects located within a set radius
//Author: Kyle Weekley
public class Collector : MonoBehaviour
{
    public float range;
    public float capacity;
    public float carrying;
    public Collectable testTrash;

    // Start is called before the first frame update
    void Start()
    {
        range = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (testTrash != null && carrying < capacity)
        {
            //Set Collectable object's parentCollector to this object if close enough
            if ((this.transform.position - testTrash.transform.position).magnitude < range)
            {
                if (testTrash.collected == false)
                {
                    Debug.Log("Collected!");
                    testTrash.Collected(this);
                    //Increment count for currently carried objects
                    carrying++;
                }
            }
        }
    }
}
