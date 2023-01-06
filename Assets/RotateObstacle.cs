using UnityEngine;

public class RotateObstacle : Obstacle,IRotateable,IDamageable
{
    public bool isCollided = false;

    public bool IsCollided => isCollided;
    public void HandleDamage()
    {
        
    }

    public void RotateObstacleObject()
    {
        transform.Rotate(new Vector3(0, 5, 0));
    }

    void Update()
    {
        RotateObstacleObject();
    }
    public void ToogleCollideStatus()
    {
        if(isCollided==false)
        {
            isCollided = true;
        }
        else if(isCollided == true)
        {
            isCollided = false;
        }
    }
}
