using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object can be collected by a Collector
//Author: Kyle Weekley
public class Collectable : MonoBehaviour
{
    public Collector parentCollector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //This object will follow its collector if it has one
        if (parentCollector != null)
        {
            transform.position = new Vector3(parentCollector.transform.position.x, parentCollector.transform. position.y, transform.position.z);
        }
    }

    //Set this object's collector
    public void Collected(Collector collector)
    {
        parentCollector = collector;
        tag = "CollectedTrash";
    }

    //Destroy this object when it enters a trash can
    //also decrement collector's carrying count
    public void Trashed()
    {
        parentCollector.carrying--;
        Destroy(gameObject);
        Debug.Log("Money++ goes here");
    }
}
