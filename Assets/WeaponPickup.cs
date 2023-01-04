using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickable,IRotateable
{
    [SerializeField] float weaponStrength = 2;
    [SerializeField] GameObject gameObjectHolder;

    public bool isAlreadyPickupWeapon = false;
    public bool IsAlreadyCollected => isAlreadyPickupWeapon;

    public void HandlePickItem()
    {
        throw new System.NotImplementedException();
    }

    public void RotateObstacleObject()
    {
        transform.Rotate(new Vector3(0, 5, 0));
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
