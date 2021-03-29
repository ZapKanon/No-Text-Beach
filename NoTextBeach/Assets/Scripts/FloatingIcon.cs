using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An icon rises from it's starting position, then fades away.
//Author: Kyle Weekley
public class FloatingIcon : MonoBehaviour
{
    public Sprite money;
    public Sprite fullCapacity;

    private float verticalSpeed;
    private float horizontalSway;
    private float horizontalSpeed;
    private float horizontalMin;
    private float horizontalMax;

    private float fadeTimer;
    private float fadeStartTime;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeBehavior();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, horizontalMax - horizontalMin) + horizontalMin, transform.position.y, transform.position.z);
        transform.position += new Vector3(0, verticalSpeed * Time.deltaTime, 0);

        if (fadeTimer >= fadeStartTime)
        {
            spriteRenderer.color += new Color(0f, 0f, 0f, -0.4f * Time.deltaTime);
        }
        
        if (spriteRenderer.color.a <= 0f)
        {
            Destroy(gameObject);
        }

        fadeTimer += Time.deltaTime;
    }

    private void RandomizeBehavior()
    {
        transform.position += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
        verticalSpeed = Random.Range(0.7f, 1.3f);
        horizontalSway = Random.Range(0.4f, 0.8f);
        horizontalSpeed = Random.Range(1.1f, 1.9f);
        fadeStartTime = Random.Range(0.7f, 1.5f);

        horizontalMin = transform.position.x - horizontalSway;
        horizontalMax = transform.position.x + horizontalSway;
    }
}
