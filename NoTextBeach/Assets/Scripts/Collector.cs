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

    public CircleCollider2D collectorCollider;
    public GameObject rangeIndicator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateRange();
    }

    //Update collection range and visual indicator to match value
    public void UpdateRange()
    {
        collectorCollider.radius = range;
        rangeIndicator.transform.localScale = new Vector3(range * 2, range * 2, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Pick up trash from within range
        if (collision.gameObject.tag == "Trash" && carrying < capacity)
        {
            collision.gameObject.GetComponent<Collectable>().Collected(this);
            Debug.Log("Collected!");
            carrying++;
        }

        //Deposit all collected trash when touching trash can
        else if (collision.gameObject.tag == "TrashCan")
        {
            GameObject[] collectedTrash = GameObject.FindGameObjectsWithTag("CollectedTrash");
            foreach (GameObject trash in collectedTrash)
            {
                trash.GetComponent<Collectable>().Trashed();
            }
        }
    }
}
