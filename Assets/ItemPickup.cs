using UnityEngine;

public class ItemPickup : MonoBehaviour,IPickable
{
    Rigidbody rb;
    public bool isAlreadyCollected = false;

    public bool IsAlreadyCollected => isAlreadyCollected;

    private void Awake() {
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
                gameObject.transform.parent = null;

            }
        }
    }

}
