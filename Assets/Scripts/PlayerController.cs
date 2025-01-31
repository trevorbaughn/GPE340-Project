using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    
    [Tooltip("Does the pawn rotate towards the mouse position?")]
    public bool isMouseRotation;
    public int lives = 3;
    
    #region InputVars
    private PlayerInputActions _playerControls;
    private InputAction _move;
    private InputAction _crouch;
    private InputAction _primaryAction;
    private InputAction _secondaryAction;
    private InputAction _pause;
    
    #endregion

    #region MovementSmoothingVars
    private Vector2 _currentMoveDir;
    private Vector2 _smoothVelocity;
    [Tooltip("Speed to smooth between animations")]
    [SerializeField] private float smoothSpeed;
    #endregion
    
    private bool _isCrouching;
    
    private void Awake()
    {
        _playerControls = new PlayerInputActions();
        lives -= 1;
    }

    private void OnEnable()
    {
        //Enable Input Actions
        #region enableInput
        _move = _playerControls.Player.Move;
        _move.Enable();

        _crouch = _playerControls.Player.Crouch;
        _crouch.Enable();
        _crouch.started += Crouch;

        _primaryAction = _playerControls.Player.PrimaryAction;
        _primaryAction.Enable();
        _primaryAction.performed += PrimaryActionBegin;
        _primaryAction.canceled += PrimaryActionEnd;

        _secondaryAction = _playerControls.Player.SecondaryAction;
        _secondaryAction.Enable();
        _secondaryAction.performed += SecondaryActionBegin;
        _secondaryAction.canceled += SecondaryActionEnd;

        _pause = _playerControls.Player.TogglePause;
        _pause.Enable();
        _pause.started += TogglePause;
        #endregion
    }

    private void OnDisable()
    {
        _move.Disable();
        _crouch.Disable();
        _primaryAction.Disable();
        _secondaryAction.Disable();
        _pause.Disable();
    }

    protected override void MakeDecisions()
    {
        if (controlledPawn == null || _move == null) return;
        
        //get input from InputActions, smooth it, pass it to a vector3 and move
        Vector2 moveDirection = _move.ReadValue<Vector2>();
        _currentMoveDir = Vector2.SmoothDamp(_currentMoveDir, moveDirection, ref _smoothVelocity, smoothSpeed);
        Vector3 direction = new Vector3(_currentMoveDir.x, 0, _currentMoveDir.y);
        //direction = Vector3.ClampMagnitude(direction, 1);

        if (controlledPawn != null && direction != null)
        {
            controlledPawn.Move(direction);
            
            //Rotate controlled pawn
            RotatePawn(isMouseRotation);
        }

        
    }

    /// <summary>
    /// Rotates the pawn either based on an input axis or towards the mouse cursor
    /// </summary>
    /// <param name="isMouseRotation">Whether the pawn should be rotated towards the cursor or not</param>
    private void RotatePawn(bool isMouseRotation)
    {
        if (isMouseRotation) // Rotation towards mouse position
        {
            //a "Ray" is an infinite line; in this case in the direction of the camera from where the cursor is
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Plane footPlane = new Plane(Vector3.up, controlledPawn.transform.position);

            //rotate the controlled pawn towards the point where mouseRay intersects with footPlane
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

    //Crouch input action function.  Toggles _isCrouching and sets it on the pawn (and through that, the animator).
    private void Crouch(InputAction.CallbackContext context)
    {
        HumanoidPawn humanoidPawn = controlledPawn as HumanoidPawn;
        if (humanoidPawn != null)
        {
            //toggle crouch
            _isCrouching = !_isCrouching;
            humanoidPawn.SetCrouch(_isCrouching); 
            
        }
        
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        GameManager.instance.TogglePause();
    }

    
    private void PrimaryActionBegin(InputAction.CallbackContext context)
    {
        if (controlledPawn.weapon != null)
        {
            controlledPawn.weapon.OnPrimaryAttackBegin.Invoke();
        }
    }
    private void PrimaryActionEnd(InputAction.CallbackContext context)
    {
        if (controlledPawn.weapon != null)
        {
            controlledPawn.weapon.OnPrimaryAttackEnd.Invoke();
        }
    }
    private void SecondaryActionBegin(InputAction.CallbackContext context)
    {
        if (controlledPawn.weapon != null)
        {
            controlledPawn.weapon.OnSecondaryAttackBegin.Invoke();
        }
    }
    private void SecondaryActionEnd(InputAction.CallbackContext context)
    {
        if (controlledPawn.weapon != null)
        {
            controlledPawn.weapon.OnSecondaryAttackEnd.Invoke();
        }
    }
}
