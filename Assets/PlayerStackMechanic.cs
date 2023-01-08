using UnityEngine;
using DG.Tweening;
using System.Collections;
public class PlayerStackMechanic : MonoBehaviour
{
    [SerializeField] Transform itemHolderTransform;
    [SerializeField] float jumpPosY;
    [SerializeField] GameObject[] bones;
    WeaponPickup weapon;
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
                    item.HandlePickItem(itemHolderTransform,jumpPosY * numberOfItemHolding);
                    objectHolding.GetChild(0).gameObject.SetActive(true);
                    numberOfItemHolding += 1;
                }   
            }

            else if(other.CompareTag("Weapon"))
            {
                WeaponPickup weaponPickup;
                weaponPickup = other.GetComponent<WeaponPickup>();
                if(weaponPickup.isAlreadyPickupWeapon == false){
                    this.weapon = weaponPickup;
                    weapon.HandlePickItem(itemHolderTransform,(jumpPosY + 0.1f) * numberOfItemHolding);
                }
            }
        }     
    }

    private void SetUpTopItem()
    {
        if(weapon!=null)
        {
            if (weapon.IsAlreadyCollected)
            {
                weapon.transform.SetParent(bones[numberOfItemHolding + 1].transform);
                weapon.transform.localPosition = new Vector3(0, 0, 0);
                weapon.transform.localRotation = Quaternion.identity;
                weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y, weapon.transform.localScale.z);
            }
        }
    }

    IEnumerator RemoveAllItem()
    {
        isLoadingAnimation = true;
        weapon.transform.DOScale(new Vector3(0,0,0),0.5f);
        for(int i = numberOfItemHolding-1 ; i >= 0; i--){

            Animator boneAnimator;
            boneAnimator = bones[i].transform.GetChild(0).GetComponent<Animator>();;   
            boneAnimator.SetBool("Out",true);
            yield return new WaitForSeconds(1/numberOfItemHolding); 

            bones[i].transform.GetChild(0).gameObject.SetActive(false);   
            yield return new WaitForSeconds(1/numberOfItemHolding);    
        }
        yield return new WaitForSeconds(0.1f);
        DecreaseNumberOfItemHolding();   
        StartCoroutine(AddItemFromStart());
    }

    IEnumerator AddItemFromStart()
    { 
        for(int i = 0; i < numberOfItemHolding; i++)
        {
            bones[i].transform.GetChild(0).gameObject.SetActive(true); 
            yield return new WaitForSeconds(0.1f);
        }
        
        weapon.transform.DOScale(new Vector3(1.5f,0.5f,1.5f),0.5f);
        yield return new WaitForSeconds(0.1f);
        isLoadingAnimation = false;
    }
    
    void RebuildBehaviour()
    {
        if(isItemCollided)
        {
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
     public void IncreaseNumberOfItemHolding(){
        numberOfItemHolding++;
     }
     public void DecreaseNumberOfItemHolding(){
        numberOfItemHolding--;
     }
}
