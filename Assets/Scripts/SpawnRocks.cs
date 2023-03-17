using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocks : MonoBehaviour
{
    public GameObject[] rocks;

    void Start()
    {

        for (int i = 0; i < 1000; i++)
        {
            Quaternion RandomRotate = new Quaternion(0, Random.Range(0, 360), 0, Random.Range(0, 360));
            Vector3 RandomRock = new Vector3(Random.Range(-750, 750), 300, Random.Range(-750, 750));
            GameObject Rock = rocks[Random.Range(0, rocks.Length)];
            GameObject newObject = Instantiate(Rock, RandomRock, RandomRotate);
            newObject.transform.localScale = new Vector3(Random.Range(3, 6), Random.Range(4, 6), Random.Range(3, 6));
        }

    }
}
