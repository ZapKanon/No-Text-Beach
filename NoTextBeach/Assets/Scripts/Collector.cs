using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Collects all Collectable objects located within a set radius
//Author: Kyle Weekley
public class Collector : MonoBehaviour
{
    public float collectRange;
    public Collectable testTrash;

    // Start is called before the first frame update
    void Start()
    {
        collectRange = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (testTrash != null)
        {
            //Set Collectable object's parentCollector to this object if close enough
            if ((this.transform.position - testTrash.transform.position).magnitude < collectRange)
            {
                if (testTrash.collected == false)
                {
                    Debug.Log("Collected!");
                    testTrash.Collected(gameObject);
                }
            }
        }
    }
}
