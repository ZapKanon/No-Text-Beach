using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detects Collectable objects within a set radius and converts them into money for the player
//Author: Kyle Weekley
public class TrashCan : MonoBehaviour
{
    public Collectable testTrash;
    public float trashRange;

    // Start is called before the first frame update
    void Start()
    {
        trashRange = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (testTrash != null)
        {
            //Destroy Collectable object if it's close enough to this trash can
            if ((this.transform.position - testTrash.transform.position).magnitude < trashRange)
            {
                testTrash.Trashed();
                Debug.Log("Item sent to trash.");
            }
        }
    }
}
