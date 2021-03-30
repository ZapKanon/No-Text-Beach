using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object can be collected by a Collector
//Author: Kyle Weekley
public class Collectable : MonoBehaviour
{
    public Collector parentCollector;
    private PlayerAnimation playerAnim;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //This object will follow its collector if it has one
        if (parentCollector != null)
        {
            transform.position = new Vector3(parentCollector.transform.position.x, parentCollector.transform. position.y, startPos.z);

            if (playerAnim)
            {
                if (playerAnim.DirState != PlayerAnimation.DirectionState.Up)
                    transform.position = new Vector3(parentCollector.transform.position.x, parentCollector.transform.position.y, parentCollector.transform.position.z - 1);

            }
        }
    }

    //Set this object's collector
    public void Collected(Collector collector)
    {
        parentCollector = collector;
        tag = "CollectedTrash";

        if (parentCollector)
            playerAnim = parentCollector.transform.Find("PlayerSprite").GetComponent<PlayerAnimation>();
    }

    //Destroy this object when it enters a trash can
    //also decrement collector's carrying count
    public void Trashed()
    {
        parentCollector.carrying--;
        Destroy(gameObject);
        //Debug.Log("Money++ goes here");
    }
}
