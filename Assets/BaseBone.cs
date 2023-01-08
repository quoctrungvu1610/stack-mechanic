using UnityEngine;

public class BaseBone : MonoBehaviour
{
    public PlayerStackMechanic playerStackMechanic;
    private bool isCollided = false;
    public bool IsCollided => isCollided;
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Obstacle"))
        {
            RotateObstacle obstacle;
            obstacle = other.gameObject.GetComponent<RotateObstacle>();
            if(obstacle.IsCollided == false)
            {
                playerStackMechanic.ToogleIsCollideItem();
                obstacle.ToogleCollideStatus();
            } 
        }
    }
}


