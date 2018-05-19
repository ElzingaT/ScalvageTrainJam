using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    public float width = 20.0f;
    public float height = 20.0f;

    public GameObject[] asteroidPrefabs;
    public int startingCount = 10;

    public float rotationSpeed = 10.0f;
    public float spawnDistance = 100.0f;

	void Start ()
    {
        SpawnAsteroids(startingCount);
	}

    public void SpawnAsteroids(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = new Vector3();
            bool locationFound = false;
            while (!locationFound)
            {
                float xPos = Random.Range(0, width) - (width / 2);
                float yPos = Random.Range(0, height) - (width / 2);

                spawnPosition = new Vector3(xPos, yPos, 0);

                if (Vector3.Distance(spawnPosition, transform.position) > spawnDistance)
                    locationFound = true;
            }

            int rand = Random.Range(0, asteroidPrefabs.Length);
            GameObject obj = Instantiate(asteroidPrefabs[rand], spawnPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            obj.transform.parent = gameObject.transform;
        }
    }
	
	void Update ()
    {
        transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
    }
}
