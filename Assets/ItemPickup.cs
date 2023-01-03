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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            gameObject.transform.parent = null;

        }
    }
}
