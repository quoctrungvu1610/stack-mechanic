using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    [SerializeField] Transform itemHolderTransform;
    [SerializeField] GameObject item;


    bool isAlreadyCollected = false;
    int numberOfItemHolding = 0;
    Transform objectHolding;

    void Awake()
    {
        objectHolding = this.gameObject.transform.GetChild(0).GetChild(0);
    }
    void Update()
    {
       
    }
    private void AddNewItem(Transform itemToAdd){
        itemToAdd.DOJump(itemHolderTransform.position + new Vector3(0,0.4f * numberOfItemHolding, -4 ),2f,1,0.2f).OnComplete(()=>{
            itemToAdd.SetParent(objectHolding);
            itemToAdd.localPosition = new Vector3(0,0,0);
            itemToAdd.localRotation = Quaternion.identity;
            itemToAdd.localScale = new Vector3(itemToAdd.transform.localScale.x,1,itemToAdd.transform.localScale.z);
            numberOfItemHolding++;
            objectHolding = objectHolding.GetChild(0);
        });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            AddNewItem(other.transform);
        }
    }
}
