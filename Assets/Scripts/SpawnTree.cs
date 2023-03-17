using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public GameObject tree;
    void Start()
    {

        for (int i = 0; i < 1000; i++)
        {
            Quaternion RandomRotate = new Quaternion(0,Random.Range(0, 360), 0,Random.Range(0, 360));
            Vector3 RandomTree = new Vector3(Random.Range(-750, 750), 300, Random.Range(-750, 750));
            GameObject newObject = Instantiate(tree, RandomTree, RandomRotate);
            newObject.transform.localScale = new Vector3(Random.Range(7,13),Random.Range(9, 13),Random.Range(7, 13));
        }

    }
}
