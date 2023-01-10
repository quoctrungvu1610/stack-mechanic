using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCMCamController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CMFollowVCam;
    [SerializeField] PlayerStackMechanic playerStackMechanic;
    int keyCheck = 0;
    
    void Start()
    {
        keyCheck = playerStackMechanic.NumberOfItemHolding;
    }

    void Update()
    {
        StartCoroutine(UpdateCMFollowVCamPosition());
    }
    
    IEnumerator UpdateCMFollowVCamPosition()
    {  
        if(keyCheck != playerStackMechanic.NumberOfItemHolding){
            if(keyCheck < playerStackMechanic.NumberOfItemHolding)
            {
                for(int i = 0; i < 10; i++)
                {
                    CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance += 0.02f;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if(keyCheck > playerStackMechanic.NumberOfItemHolding)
            {
                for(int i = 0; i < 10; i++)
                {
                    CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= 0.01f;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            
            keyCheck = playerStackMechanic.NumberOfItemHolding;
        }
        yield return null;
    }
}
