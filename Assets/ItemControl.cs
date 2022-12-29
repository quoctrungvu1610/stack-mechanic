using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public bool isAlreadyCollected = false;

    public void ToggleStatus(){
        isAlreadyCollected = !isAlreadyCollected;
    }
    public bool GetItemCollectedStatus(){
        return isAlreadyCollected;
    }
}
