using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Upgrade that passively generates money once purchased
//Author: Kyle Weekley
public class Net : MonoBehaviour
{
    public float moneyTimer;
    public float moneyGenerateSpeed;

    public AudioSource cashSound;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject floatingIcon;

    // Start is called before the first frame update
    void Start()
    {
        moneyGenerateSpeed = 5.0f; //Placeholder value
    }

    // Update is called once per frame
    void Update()
    {
        //Generate money at a set interval
        if (moneyTimer >= moneyGenerateSpeed)
        {
            GenerateMoney();
            moneyTimer = 0f;
        }

        moneyTimer += Time.deltaTime;
    }

    //Increment player score and spawn a money icon
    public void GenerateMoney()
    {
        gameManager.addToScore(1);

        cashSound.volume *= 0.25f;
        cashSound.PlayOneShot(cashSound.clip);
        cashSound.volume *= 4.0f;

        Instantiate(floatingIcon, Camera.main.ScreenToWorldPoint(transform.position), Quaternion.identity, null);
        FloatingIcon iconScript = floatingIcon.GetComponent<FloatingIcon>();
        iconScript.GetComponent<SpriteRenderer>().color = Color.green;
        iconScript.GetComponent<SpriteRenderer>().sprite = iconScript.money;
    }
}
