using UnityEngine;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    [SerializeField] Transform itemHolderTransform;
    [SerializeField] float jumpPosY;
    [SerializeField] GameObject weapon;
    [SerializeField] Transform header;
    
    bool isAlreadyCollected = false;
    int numberOfItemHolding = 0;

    Transform objectHolding;
    
    void Awake()
    {
        //Set up the first object holding
        objectHolding = this.gameObject.transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        
        SetUpSphereWeapon();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Item"))
        {
            
            
            ItemControl item;
            item = other.GetComponent<ItemControl>();
            if(item.GetItemCollectedStatus() == false){
                AddNewItem(other.transform);
                item.ToggleStatus();
            }
           // other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void AddNewItem(Transform itemToAdd)
    {
        //Jump the Collide Object to the main Object
        itemToAdd.DOJump(itemHolderTransform.position + new Vector3(0, jumpPosY * numberOfItemHolding, -4 ),2f,1,0.2f).OnComplete(()=>{ 
            SetUpCollideItem(itemToAdd);  
        });
    }
   
    private void SetUpCollideItem(Transform item)
    {
        
        //Set Parent for Collide Object
        item.SetParent(objectHolding);
        //Set up the Position
        item.localPosition = new Vector3(0,0,0);
        item.localRotation = Quaternion.identity;
        item.localScale = new Vector3(item.transform.localScale.x, 1, item.transform.localScale.z);
        //Increase the index
        numberOfItemHolding++;
        //Chage the object Holding
        objectHolding = objectHolding.GetChild(0);
    }

    private void SetUpSphereWeapon()
    {
        FindEmptyPosition();
        weapon.transform.SetParent(objectHolding);

        weapon.transform.localPosition = new Vector3(0, 2, 0);
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y, weapon.transform.localScale.z);
    }

    private void ChangeCurrentObjectHolding(Transform objct)
    {
        objct = objct.GetChild(0);
    }

    public void FindEmptyPosition()
    {
        Transform check1;
        Transform check2;
        check1 = transform;
        
        for(int i = 0; i < numberOfItemHolding;i++){

            if(check1.childCount > 1)
            {
                check1 = check1.GetChild(0);
            }

            else if(check1.childCount == 1)
            {
                 
                objectHolding = check1; 
                numberOfItemHolding = i-1;
                check2 = check1;

                for(int y = 0; y < 15 - numberOfItemHolding; y++)
                {

                    if(check2.childCount > 1)
                    {
                        if(check2.GetChild(1).gameObject.name != "Weapon"){
                            check2.GetChild(1).SetParent(null);
                        }      
                    }
                    check2 = check2.GetChild(0);
                }       

                break;
            }
        }
    }
}
