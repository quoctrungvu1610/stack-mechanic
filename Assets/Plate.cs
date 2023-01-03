using UnityEngine;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    [SerializeField] Transform itemHolderTransform;
    [SerializeField] float jumpPosY;
    [SerializeField] GameObject weapon;
    [SerializeField] Transform header;
    
    int numberOfItemHolding = 0;

    Transform objectHolding;
    
    void Awake()
    {
        objectHolding = this.gameObject.transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        FindEmptyPosition();
        SetUpSphereWeapon(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemControl item;
            item = other.GetComponent<ItemControl>();
            if(item.IsAlreadyCollected == false){
                AddNewItem(other.transform);
                item.ToggleStatus();
            }
        }
    }

    private void AddNewItem(Transform itemToAdd)
    {
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

    private void SetUpSphereWeapon()
    {
        weapon.transform.SetParent(objectHolding.GetChild(0));
        weapon.transform.localPosition = new Vector3(0, 1, 0);
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y, weapon.transform.localScale.z);
    }

    public void FindEmptyPosition()
    {
        Transform check1;
        Transform check2;

        check1 = this.transform;
        for(int i = 0; i < 15; i++){  
            if (check1.childCount <= 1)
            {
                objectHolding = check1;
                check2 = check1;
                for(int y = 0; y < 15 - numberOfItemHolding;y++)
                {
                    if(check2.childCount > 1 && check2.GetChild(1).name != "Weapon"){
                        check2.GetChild(1).SetParent(null);
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
