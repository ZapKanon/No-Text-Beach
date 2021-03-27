using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum SpawnCondition {
        Time,
        Collection
    }

    [SerializeField] private int amountToSpawn = 10; //Amount of trash to spawn per wave
    [SerializeField] private int maxTrash = 100; //Max amount of trash spawned at one time
    [SerializeField] private SpawnCondition spawnCondition = SpawnCondition.Time; //What should cause the trash to respawn
    [SerializeField] private float timeUntilRespawn = 10.0f; //Seconds until the next wave is spawned
    [SerializeField] private int collectsUntilRespawn = 10; //Amount of trash to collectcollect until the next wave is spawned
    [SerializeField] private Vector2 minSpawnPos; //Min position trash can be spawned at
    [SerializeField] private Vector2 maxSpawnPos; //Max position trash can be spawned at
    [SerializeField] private List<GameObject> trashPrefabs; //Prefabs for spawned trash

    [SerializeField] private float waveDuration = 2; //Number of seconds wave moves across the screen
    [SerializeField] private Vector2 waveStartPos; //Position wave starts at and returns to
    [SerializeField] private Vector2 waveEndPos; //Position wave moves to before returning
    [SerializeField] private GameObject wavePrefab; //Prefab for moving wave

    private bool spawning; //Whether trash is currently being spawned
    private List<GameObject> trash; //List of spawned trash
    private float elapsedTime; //Time since wave was spawned
    private int collectedTrash; //Amount of trash collected since wave was spawned

    // Start is called before the first frame update
    void Start()
    {
        trash = new List<GameObject>();
        SpawnTrash(); //Spawns garbage to start
    }

    // Update is called once per frame
    void Update()
    {
        ValidateInputs();

        elapsedTime += Time.deltaTime; //Updates elapsed time

        for (int i = trash.Count - 1; i >= 0; i--)
        {
            if (trash[i] == null) //Item was deposited and deleted
            {
                trash.RemoveAt(i); //Removes item from list
                collectedTrash++; //Updates collected trash
            }
        }

        if (((spawnCondition == SpawnCondition.Time && elapsedTime >= timeUntilRespawn) ||
            (spawnCondition == SpawnCondition.Collection && collectedTrash >= collectsUntilRespawn)) && !spawning) //Current respawn condition has been met
        {
            if (trash.Count < maxTrash) //Won't spawn trash past max value
            {
                spawning = true;
                MoveWave(); //Show the wave moving
            }
            else //Resets values to check again soon
            {
                elapsedTime = 0; //Resets time
                collectedTrash = 0; // Resets cllected trash
            }
        }
    }

    private void ValidateInputs()
    {
        if (amountToSpawn < 0) //Limits minumum amount of trash to spawn
            amountToSpawn = 0;

        if (timeUntilRespawn < 0) //Limits minimum respawn time
            timeUntilRespawn = 0;

        if (collectsUntilRespawn < 0) //Limits minimum collection amount
            collectsUntilRespawn = 0;
        else if (collectsUntilRespawn > amountToSpawn) //Limits maximum collection amount
            collectsUntilRespawn = amountToSpawn;

        if (minSpawnPos.x > maxSpawnPos.x) //Ensures max and min x are set correctly
        {
            float temp = minSpawnPos.x;
            maxSpawnPos.x = minSpawnPos.x;
            minSpawnPos.x = temp;
        }

        if (minSpawnPos.y > maxSpawnPos.y) //Ensures max and min y are set correctly
        {
            float temp = minSpawnPos.y;
            maxSpawnPos.y = minSpawnPos.y;
            minSpawnPos.y = temp;
        }
    }

    private void MoveWave() //Moves a wave across the screen that recedes and leaves trash behind
    {
        StartCoroutine(WaveEbbAndFlow(Instantiate(wavePrefab, new Vector3(waveStartPos.x, waveStartPos.y, 4), Quaternion.identity, transform)));
    }

    IEnumerator WaveEbbAndFlow(GameObject wave)
    {

        float waveElapsed = 0;

        while (waveElapsed < waveDuration / 2) { //Wave flows over time
            yield return new WaitForSeconds(Time.deltaTime);
            wave.transform.position = Vector2.Lerp(waveStartPos, waveEndPos, waveElapsed / (waveDuration / 2));
            waveElapsed += Time.deltaTime;
        }

        SpawnTrash(); //Spawns trash when wave is covering beach

        while (waveElapsed < waveDuration) //Wave ebbs over time
        {
            yield return new WaitForSeconds(Time.deltaTime);
            wave.transform.position = Vector2.Lerp(waveEndPos, waveStartPos, (waveElapsed - waveDuration / 2) / (waveDuration / 2));
            waveElapsed += Time.deltaTime;
        }

        spawning = false;
        Destroy(wave);
    }


    private void SpawnTrash() //Spawns a new wave of trash
    {
        elapsedTime = 0; //Resets time
        collectedTrash = 0; // Resets cllected trash

        for (int i = 0; i < amountToSpawn; i++)
        {
            Debug.Log("Trash.Count: " + trash.Count);
            if (trash.Count < maxTrash) //Prevents spawning past max value
            {
                int garbageIndex = Random.Range(0, trashPrefabs.Count); //Which prefab to instantiate
                Vector3 position = new Vector3(Random.Range(minSpawnPos.x, maxSpawnPos.x), Random.Range(minSpawnPos.y, maxSpawnPos.y), 5); //Gets random position
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360.0f))); //Gets random rotation
                trash.Add(Instantiate(trashPrefabs[garbageIndex], position, rotation, transform));

                if (Random.Range(0, 2) == 0) //Randomly flips sprite horizontally for variation
                {
                    SpriteRenderer renderer = trash[trash.Count - 1].GetComponent<SpriteRenderer>();
                    if (renderer)
                        renderer.flipX = true;
                }
            }
        }
    }
}