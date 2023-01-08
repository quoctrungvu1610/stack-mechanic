using UnityEngine;
using DG.Tweening;

public class ItemPickup : MonoBehaviour,IPickable
{
    PlayerStackMechanic playerStackMechanic;
    Rigidbody rb;
    Transform itemTransform;
    public bool isAlreadyCollected = false;
    public bool IsAlreadyCollected => isAlreadyCollected;
    public bool isCollided = false;

    private void Awake() 
    {
        playerStackMechanic = FindObjectOfType<PlayerStackMechanic>().gameObject.GetComponent<PlayerStackMechanic>();
        rb = gameObject.GetComponent<Rigidbody>();
        itemTransform = transform;
    }

    private void ToggleStatus()
    {
        isAlreadyCollected = !isAlreadyCollected;
    }

    public void HandlePickItem(Transform itemHolderTransform, float jumpYPosition)
    {
        JumpToObject(itemHolderTransform,jumpYPosition);
        ToggleStatus();
    }

    private void JumpToObject(Transform itemHolderTransform, float jumpYPosition)
    {
        Vector3 endVal = Vector3.zero;
        itemTransform.DOScale(endVal,2);
        itemTransform.DOJump(itemHolderTransform.position + new Vector3(0, jumpYPosition, -4 ),2f,1,0.2f).OnComplete(()=>{
            itemTransform.gameObject.SetActive(false);
        });
    }
}
