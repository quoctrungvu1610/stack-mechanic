using UnityEngine;

public class ItemControl : MonoBehaviour,IPickable
{
    Rigidbody rb;
    public bool isAlreadyCollected = false;
    Plate plate;

    public bool IsAlreadyCollected => isAlreadyCollected;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        plate = FindObjectOfType<Plate>();
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
