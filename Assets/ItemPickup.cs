using UnityEngine;

public class ItemPickup : MonoBehaviour,IPickable
{
    PlayerStackMechanic playerStackMechanic;
    Rigidbody rb;
    public bool isAlreadyCollected = false;

    public bool IsAlreadyCollected => isAlreadyCollected;

    public bool isCollided = false;

    private void Awake() {
        playerStackMechanic = FindObjectOfType<PlayerStackMechanic>().gameObject.GetComponent<PlayerStackMechanic>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void ToggleStatus(){
        isAlreadyCollected = !isAlreadyCollected;
    }

    public void HandlePickItem()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(IsAlreadyCollected){
            if (other.gameObject.CompareTag("Obstacle"))
            {
                RotateObstacle obstacle;

                obstacle = other.gameObject.GetComponent<RotateObstacle>();

                if(obstacle.IsCollided == false){
                    playerStackMechanic.ToogleIsCollideItem();
                    obstacle.ToogleCollideStatus();
                }
            }
        }
    }
}
