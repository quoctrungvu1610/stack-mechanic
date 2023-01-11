using UnityEngine;
using DG.Tweening;

public class WeaponPickup : MonoBehaviour, IPickable,IRotateable,IToggleStatus
{
    [SerializeField] float weaponStrength;
    [SerializeField] GameObject gameObjectHolder;
    Transform weaponTransform;
    public bool isAlreadyPickupWeapon = false;
    public bool IsAlreadyCollected => isAlreadyPickupWeapon;
    private void Awake() 
    {
        weaponTransform = transform;    
    }
    public void HandlePickItem(Transform itemHolderTransform, float jumpYPosition)
    {
        JumpToObject(itemHolderTransform,jumpYPosition);
    }

    public void RotateObstacleObject()
    {
        transform.Rotate(new Vector3(0, 5, 0));
    }

    private void JumpToObject(Transform itemHolderTransform, float jumpYPosition)
    {   
        Vector3 endVal = Vector3.zero;
        Vector3 scaleVal = new Vector3(1.5f,0.5f,1.5f);
        weaponTransform.DOScale(endVal,0.5f);
        weaponTransform.DOJump(itemHolderTransform.position + new Vector3(0, jumpYPosition, -4), 2f, 1, 0.2f).OnComplete(()=>{
            weaponTransform.DOScale(scaleVal, 0.5f);
            ToggleStatus();
        });
    }

    public void ToggleStatus()
    {
        isAlreadyPickupWeapon = !isAlreadyPickupWeapon;
    }

    private void Update() 
    {
        if(IsAlreadyCollected == false){
            RotateObstacleObject();
        } 
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(IsAlreadyCollected){
            if(other.gameObject.CompareTag("Wall")){
                BreakWall(other);
            }
        }
    }

    private void BreakWall(Collision objectToBreak)
    {
        Rigidbody wallRb = objectToBreak.gameObject.GetComponent<Rigidbody>();
        Vector3 awayDirectionFromPlayer = objectToBreak.gameObject.transform.position - gameObjectHolder.transform.position;
        wallRb.AddForce(awayDirectionFromPlayer * weaponStrength);
        Destroy(objectToBreak.gameObject, 1f);
    }
}
