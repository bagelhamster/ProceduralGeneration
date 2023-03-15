using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject Egg;
    public float interval;
    public GameObject cam;
    public GameObject TreeBreak;
    void Update()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;
            Time.timeScale = 100;
        }
        /*if(interval >= 0.1)
        {
            interval -= Time.deltaTime;

        }*/
        else
        {
            cam.gameObject.SetActive(false);
            Time.timeScale = 1;
            TreeBreak.gameObject.SetActive(true);


        }
    }
        void Start()
    {

        for (int i = 0; i < 10; i++)
        {
        Vector3 randomEgg = new Vector3(Random.Range(-600, 600), 300, Random.Range(-600, 600));
        Instantiate(Egg,randomEgg,Quaternion.identity);
            cam.gameObject.SetActive(true);
            TreeBreak.gameObject.SetActive(false);

        }

    }

}
