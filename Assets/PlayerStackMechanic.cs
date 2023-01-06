using UnityEngine;
using DG.Tweening;

public class PlayerStackMechanic : MonoBehaviour
{
    [SerializeField] Transform itemHolderTransform;
    [SerializeField] float jumpPosY;

    WeaponPickup weapon;
    
    int numberOfItemHolding = 0;

    Transform objectHolding;
    
    void Awake()
    {
        objectHolding = this.gameObject.transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        FindEmptyPosition();
        SetUpTopItem(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemPickup item;
            item = other.GetComponent<ItemPickup>();
            if(item.IsAlreadyCollected == false){
                AddNewItem(other.transform);
                item.ToggleStatus();
            }
        }
        else if(other.CompareTag("Weapon")){
            WeaponPickup weaponPickup;
            weaponPickup = other.GetComponent<WeaponPickup>();
            this.weapon = weaponPickup;
            this.weapon.transform.DOJump(itemHolderTransform.position + new Vector3(0, (jumpPosY + 0.1f) * numberOfItemHolding, -4), 2f, 1, 0.2f).OnComplete(()=>{
                weaponPickup.ToggleStatus();
            });
        }
    }

    private void AddNewItem(Transform itemToAdd)
    {
        objectHolding.GetChild(1).gameObject.SetActive(true);

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
    }

    private void SetUpTopItem()
    {
        if(weapon!=null){
            if (weapon.IsAlreadyCollected)
            {
                weapon.transform.SetParent(objectHolding.GetChild(0));
                weapon.transform.localPosition = new Vector3(0, 0, 0);
                weapon.transform.localRotation = Quaternion.identity;
                weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y, weapon.transform.localScale.z);
            }
        }
    }

    public void FindEmptyPosition()
    {
        Transform check1;
        Transform check2;

        check1 = this.transform;
        for(int i = 0; i < 20; i++)
        {  
            if (check1.childCount <= 2 && check1.gameObject.name != "Head")
            {
                objectHolding = check1;
                check2 = check1;
                for(int y = 0; y < 20 - numberOfItemHolding;y++)
                {
                    if(check2.childCount > 2)
                    {
                        check2.GetChild(2).SetParent(null);
                        check2.GetChild(1).gameObject.SetActive(false);
                    }

                    check2 = check2.GetChild(0);    
                }

                break;
            }
            else
            {
                check1 = check1.GetChild(0);
            }
            numberOfItemHolding = i - 1;  
        }
    }
}
