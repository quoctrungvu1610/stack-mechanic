using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    Rigidbody rb;
    public bool isAlreadyCollected = false;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void ToggleStatus(){
        isAlreadyCollected = !isAlreadyCollected;
    }
    
    public bool GetItemCollectedStatus(){
        return isAlreadyCollected;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            gameObject.transform.parent = null;
            rb.useGravity = true;
        }
    }
}
