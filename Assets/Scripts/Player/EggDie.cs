using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDie : MonoBehaviour,IInteract
    
{
    public void Interact()
    {
        Destroy(gameObject);
        Score.scoreValue += 1;
    }

}
