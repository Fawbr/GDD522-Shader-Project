using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{
    [SerializeField] List<GameObject> baseSpawners = new List<GameObject>();
    [SerializeField] List<GameObject> chosenSpawners = new List<GameObject>();
    [SerializeField] List<Transform> subSpawners = new List<Transform>();
    [SerializeField] List<GameObject> bodySpawners = new List<GameObject>();
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject objectToBeSpawned;
    [SerializeField] GameObject batterySpawn;
    [SerializeField] Transform objectTransform;
    [SerializeField] public int batteriesInArea;
    int randomSpawn;
    [SerializeField] int bodiesToBeSpawned = 5;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gameObject in baseSpawners)
        {
            bodySpawners.Add(gameObject);
        }

        for (int chooseSpawn = 0; chooseSpawn <= (bodiesToBeSpawned -1); chooseSpawn++)
        {
            randomSpawn = Random.Range(0, bodySpawners.Count);
            chosenSpawners.Add(bodySpawners[randomSpawn]);
            bodySpawners.Remove(bodySpawners[randomSpawn]);
        }

        randomSpawn = Random.Range(0, baseSpawners.Count);
        foreach (GameObject gameObject in chosenSpawners)
        {
            spawner = gameObject;
            
            foreach (Transform transform in spawner.GetComponentsInChildren<Transform>())
            {

                if (transform.gameObject.tag == "SpawnPoints" && transform.gameObject != spawner)
                {
                    subSpawners.Add(transform);
                }
            }
            randomSpawn = Random.Range(0, subSpawners.Count);
            GameObject spawnedObject = Instantiate(objectToBeSpawned, subSpawners[randomSpawn]);
            spawnedObject.transform.position = subSpawners[randomSpawn].transform.position;
            subSpawners.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (batteriesInArea < 10)
        {
            foreach (GameObject gameObject in baseSpawners)
            {
                spawner = gameObject;
                foreach (Transform transform in spawner.GetComponentsInChildren<Transform>())
                {
                    if (transform.gameObject.tag == "SpawnPoints" && transform.gameObject != spawner)
                    {
                        if (transform.childCount == 0)
                        {
                            subSpawners.Add(transform);
                        }
                    }
                }
            }
            randomSpawn = Random.Range(0, subSpawners.Count);
            GameObject spawnedObject = Instantiate(batterySpawn, subSpawners[randomSpawn]);
            spawnedObject.transform.position = subSpawners[randomSpawn].transform.position;
            spawnedObject.transform.rotation = new Quaternion(300, 290, 0, 0);
            subSpawners.Clear();
            batteriesInArea++;
        }
    }
}
