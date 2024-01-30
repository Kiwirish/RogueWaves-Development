using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraSwitch : MonoBehaviour
{
    
    public GameObject FollowCamera;
    public GameObject MainCamera;
    public GameObject Default;
    public bool ZoomView;

    public CinemachineVirtualCamera cam;

    void Update(){
        ManageCamera();
    }

    void ManageCamera(){

        if(Input.GetKeyDown(KeyCode.Z)){
            if(ZoomView){
                ActivateFollowCamera();
                ZoomView = false;
            }else{
                ActivateMainCamera();
                ZoomView = true;
            }
        }
    }

    void ActivateMainCamera(){
        FollowCamera.SetActive(false);
        MainCamera.SetActive(true);
    }

    void ActivateFollowCamera(){
        FollowCamera.SetActive(true);
        MainCamera.SetActive(false);
    }

    // Reset followTarget when the cannonball is destroyed
    public void Reset()
    {
        cam.Follow = Default.transform;
        ActivateFollowCamera();
        ZoomView = false;
    }
}
