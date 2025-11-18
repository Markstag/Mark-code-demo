using System;
using UnityEngine;
using Unity.Cinemachine; //You need to add this for Cinemachine

public class FirstPersonController : MonoBehaviour
{

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravityMultiplier = 1.0f;
    
    [Header("Look Parameters")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float upDownLookRange = 80f;
    
    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CinemachineCamera mainCamera; //you need to change this to CinemachineCamera
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private float interactdistance; 
    [SerializeField] private float attackdistance;
    private Vector3 currentMovement;
    private float verticalRotation;
    private float CurrentSpeed => walkSpeed * (playerInputHandler.SprintTriggered ? sprintMultiplier : 1);
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();

        if (playerInputHandler. InteractTriggered)
        {
             Debug.Log("hit");
            RaycastHit hit;
            if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactdistance))
            {
                if (hit.collider.CompareTag("Key"))
                {
                    hit.collider.gameObject.GetComponent<Key>().OpenDoor();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("hit");
            RaycastHit hit;
            if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, attackdistance))
            {
                Debug.Log("hit");
                if (hit.collider.CompareTag("Ground"))
                {
                    hit.collider.gameObject.GetComponent<Dies>().Destroy();
                }
            }
        }
    }
    
    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }
    
    private void HandleJumping()
    {
        if(characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if(playerInputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
    }
    
    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * CurrentSpeed;
        currentMovement.z = worldDirection.z * CurrentSpeed;

        HandleJumping();
        characterController.Move(currentMovement * Time.deltaTime);
    }
    
    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }
    
    private void ApplyVerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(verticalRotation - rotationAmount, -upDownLookRange, upDownLookRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
    
    private void HandleRotation()
    {
        float mouseXRotation = playerInputHandler.RotationInput.x * mouseSensitivity;
        float mouseYRotation = playerInputHandler.RotationInput.y * mouseSensitivity;
        
        ApplyHorizontalRotation(mouseXRotation);
        ApplyVerticalRotation(mouseYRotation);        
    }
    
}
