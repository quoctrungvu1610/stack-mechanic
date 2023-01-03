using UnityEngine;

public class RotateObstacle : Obstacle,IRotateable,IDamageable
{
    public void HandleDamage()
    {
        
    }

    public void RotateObstacleObject()
    {
        transform.Rotate(new Vector3(0, 15, 0));
    }

    void Update()
    {
        RotateObstacleObject();
    }
}
