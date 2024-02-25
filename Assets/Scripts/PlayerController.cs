using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public bool isMouseRotation;
    private bool isCrouching;
    
    //Input System Variables
    private PlayerInputActions _playerControls;
    private InputAction _move;
    private InputAction _crouch;

    //Smoothing variables
    private Vector2 _currentMoveDir;
    private Vector2 _smoothVelocity;
    [SerializeField] private float smoothSpeed;

    private void Awake()
    {
        _playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();

        _crouch = _playerControls.Player.Crouch;
        _crouch.Enable();
        _crouch.performed += Crouch;
    }

    private void OnDisable()
    {
        _move.Disable();
        _crouch.Disable();
    }

    protected override void MakeDecisions()
    {
        //get input from InputActions, smooth it, pass it to a vector3 and move
        Vector2 moveDirection = _move.ReadValue<Vector2>();
        _currentMoveDir = Vector2.SmoothDamp(_currentMoveDir, moveDirection, ref _smoothVelocity, smoothSpeed);
        Vector3 direction = new Vector3(_currentMoveDir.x, 0, _currentMoveDir.y);
        //direction = Vector3.ClampMagnitude(direction, 1);
        
        controlledPawn.Move(direction);
        
        //Rotate controlled pawn
        RotatePawn(isMouseRotation);
    }

    private void RotatePawn(bool isMouseRotation)
    {
        if (isMouseRotation)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane footPlane = new Plane(Vector3.up, controlledPawn.transform.position);

            float distanceToIntersect;
            if (footPlane.Raycast(mouseRay, out distanceToIntersect))
            {
                Vector3 intersectPoint = mouseRay.GetPoint(distanceToIntersect);

                controlledPawn.RotateToLookAt(intersectPoint);
            }
            else
            {
                Debug.Log("Camera is not looking at ground - no intersectPoint between footPlane and mouseRay");
            }
        }
        else // Keyboard/Controller Rotation on input axis
        {
            controlledPawn.Rotate(Input.GetAxis("CameraRotation"));
        }
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        HumanoidPawn humanoidPawn = controlledPawn as HumanoidPawn;
        if (humanoidPawn != null)
        {
            //toggle crouch
            isCrouching = !isCrouching;
            humanoidPawn.Crouch(isCrouching); 
            
        }
        
    }
}
