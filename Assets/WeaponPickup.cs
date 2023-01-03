using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickable,IRotateable
{
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
}
