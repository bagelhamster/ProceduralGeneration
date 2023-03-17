using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFixes : MonoBehaviour
{
    public float interval;
    void Update()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;

        }

        else
        {

            gameObject.GetComponent<ConstantForce>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().mass = 0;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.GetComponent<TreeFixes>().enabled = false;
        }
    }
}
