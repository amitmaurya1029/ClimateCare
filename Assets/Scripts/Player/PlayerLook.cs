using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    
    [SerializeField] Transform player;
    [SerializeField] Transform camera;
    [SerializeField] float senstivity = 10;

     PlayerInputActions _playerInputAction;

    float rotationY;
  
    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;

        _playerInputAction = new PlayerInputActions();
         _playerInputAction.Player.Enable();
        
    }

    
    void Update()
    {

         Vector2 rotation = _playerInputAction.Player.Rotation.ReadValue<Vector2>();
            // get an mouse x axis value
             float x = Input.GetAxis("Mouse X") * Time.deltaTime * senstivity;
             float Y = Input.GetAxis("Mouse Y") * Time.deltaTime * senstivity;
             
             Debug.Log("get the mouse axis val : " + x);

             Debug.Log("here is the rotation value new : " + rotation);
            
         
            rotationY -= rotation.y * Time.deltaTime * senstivity;
            rotationY = Mathf.Clamp(rotationY, -50, 50);

            camera.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
             //  Vector3.Lerp(,rotationY, Time.deltaTime * 10);
                camera.localRotation =  Quaternion.Euler(rotationY, 0f, 0f);

            player.Rotate(Vector3.up * rotation.x);

    }
}
