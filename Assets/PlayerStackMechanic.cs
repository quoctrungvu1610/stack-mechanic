using UnityEngine;
using DG.Tweening;
using System.Collections;
public class PlayerStackMechanic : MonoBehaviour
{
    [SerializeField] Transform itemHolderTransform;
    [SerializeField] float jumpPosY;
    [SerializeField] GameObject[] bones;
    WeaponPickup weapon;
    
    string[] animationBools = {"Left","Right"};
    int numberOfItemHolding = 0;

    Transform objectHolding;

    public bool isItemCollided = false;

    private bool isLoadingAnimation = false;
    
    void Awake()
    {
        objectHolding = bones[numberOfItemHolding].transform;
    }

    void Update()
    {
        SetUpTopItem(); 
        Debug.Log(numberOfItemHolding);
        objectHolding = bones[numberOfItemHolding].transform;
        RebuildBehaviour();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isLoadingAnimation == false)
        {
            if (other.CompareTag("Item"))
            {
                ItemPickup item;
                item = other.GetComponent<ItemPickup>();
                if(item.IsAlreadyCollected == false){
                    JumpToObject(other.transform);
                    item.ToggleStatus();
                }
            }

            else if(other.CompareTag("Weapon"))
            {
                WeaponPickup weaponPickup;
                weaponPickup = other.GetComponent<WeaponPickup>();
                this.weapon = weaponPickup;
                this.weapon.transform.DOJump(itemHolderTransform.position + new Vector3(0, (jumpPosY + 0.1f) * numberOfItemHolding, -4), 2f, 1, 0.2f).OnComplete(()=>{
                    weaponPickup.ToggleStatus();
                });
            }
        }     
    }

    private void JumpToObject(Transform itemToAdd)
    {
        objectHolding.GetChild(0).gameObject.SetActive(true);

        itemToAdd.DOJump(itemHolderTransform.position + new Vector3(0, jumpPosY * numberOfItemHolding, -4 ),2f,1,0.2f).OnComplete(()=>{ 
            SetUpCollideItem(itemToAdd);  
        });
        
    }
   
    private void SetUpCollideItem(Transform item)
    {
        item.SetParent(objectHolding);

        item.localPosition = new Vector3(0,0,0);
        item.localRotation = Quaternion.identity;
        item.localScale = new Vector3(item.transform.localScale.x, 1, item.transform.localScale.z);   

        item.transform.GetComponent<MeshRenderer>().enabled = false;

        numberOfItemHolding ++;
    }

    private void SetUpTopItem()
    {
        if(weapon!=null){
            if (weapon.IsAlreadyCollected)
            {
                weapon.transform.SetParent(bones[numberOfItemHolding + 1].transform);

                weapon.transform.localPosition = new Vector3(0, 0, 0);
                weapon.transform.localRotation = Quaternion.identity;
                weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y, weapon.transform.localScale.z);
            }
        }
    }

    IEnumerator RemoveAllItem(){
        isLoadingAnimation = true;
        weapon.transform.DOScale(new Vector3(0,0,0),0.5f);
        for(int i = numberOfItemHolding - 1 ; i >= 0; i--){

            Animator boneAnimator;
            boneAnimator = bones[i].transform.GetChild(0).GetComponent<Animator>();
            bones[i].transform.GetChild(1).gameObject.SetActive(false);   
            
            boneAnimator.SetBool("Out",true);
            
            yield return new WaitForSeconds(1/numberOfItemHolding); 
            bones[i].transform.GetChild(0).gameObject.SetActive(false);   
            
            yield return new WaitForSeconds(1/numberOfItemHolding);    
        }
        yield return new WaitForSeconds(0.1f);   
        StartCoroutine(AddItemFromStart());
    }

    IEnumerator AddItemFromStart(){
        
        for(int i = 0; i < numberOfItemHolding; i++){
            bones[i].transform.GetChild(0).gameObject.SetActive(true); 
            bones[i].transform.GetChild(1).gameObject.SetActive(true);  
            yield return new WaitForSeconds(0.1f);
        }
        
        weapon.transform.DOScale(new Vector3(1.5f,0.5f,1.5f),0.5f);
        yield return new WaitForSeconds(0.1f);
        isLoadingAnimation = false;
    }
    
    void RebuildBehaviour(){
        if(isItemCollided){
            StartCoroutine(RemoveAllItem());
            isItemCollided = false;
        }
    }

     public void ToogleIsCollideItem()
     {
        if(isItemCollided == true)
        {
            isItemCollided = false;
        }
        else if(isItemCollided == false) 
        {
            isItemCollided = true;
        }
     }
}
