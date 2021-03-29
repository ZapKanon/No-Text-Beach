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

    [SerializeField] private GameObject floatingIcon;
    [SerializeField] private GameObject capacityIcon;

    [SerializeField] private GameManager gameManager;

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
        if (collision.gameObject.tag == "Trash")
        {
            if (carrying < capacity)
            {
                collision.gameObject.GetComponent<Collectable>().Collected(this);
                Debug.Log("Collected!");
                carrying++;
            }
            //If cpaacity has been reached, spawn an icon
            else if (capacityIcon == null)
            {
                capacityIcon = Instantiate(floatingIcon, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, null);
                FloatingIcon iconScript = capacityIcon.GetComponent<FloatingIcon>();
                iconScript.GetComponent<SpriteRenderer>().color = Color.red;
                iconScript.GetComponent<SpriteRenderer>().sprite = iconScript.fullCapacity;
            }
        }

        //Deposit all collected trash when touching trash can
        else if (collision.gameObject.tag == "TrashCan")
        {
            GameObject[] collectedTrash = GameObject.FindGameObjectsWithTag("CollectedTrash");
            foreach (GameObject trash in collectedTrash)
            {
                trash.GetComponent<Collectable>().Trashed();

                //Increment the player's score for each piece of trash deposited
                GameManager.gm.addToScore(1);

                //Spawn a money icon for each piece of trash deposited
                Instantiate(floatingIcon, new Vector3(collision.transform.position.x, collision.transform.position.y, 0), Quaternion.identity, null);
                FloatingIcon iconScript = floatingIcon.GetComponent<FloatingIcon>();
                iconScript.GetComponent<SpriteRenderer>().color = Color.green;
                iconScript.GetComponent<SpriteRenderer>().sprite = iconScript.money;
            }
        }
    }
}
