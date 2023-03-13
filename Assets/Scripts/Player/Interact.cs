using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteract
{
    public void Interact()
    {

    }
}
public class Interact : MonoBehaviour
{
    public Transform interactor;
    public float interactRange;

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Ray ray = new Ray(interactor.position, interactor.forward);
            if(Physics.Raycast(ray,out RaycastHit hitInfo, interactRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteract objectInteractable))
                {
                    objectInteractable.Interact();
                }
             }
        }
    }
}
