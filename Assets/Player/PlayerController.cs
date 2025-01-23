using Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform cameraTransform;
        
        private Inputs _inputs;
        private Vector2 _look;
        private Vector2 _movement;

        [SerializeField] private float gravity;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float lookSensitivity;
        private float _movementSpeed;
        private float _verticalVelocity = 0f;

        private void Awake()
        {
            _inputs = new Inputs();
            _inputs.Player.SetCallbacks(this);
            
            _movementSpeed = walkSpeed;
        }

        private void OnEnable() => _inputs.Player.Enable();

        private void OnDisable() => _inputs.Player.Disable();

        private void Update()
        {
            if (_movement != Vector2.zero)
            {
                var velocity = transform.right * (_movement.x * _movementSpeed)
                                    + transform.forward * (_movement.y * _movementSpeed)
                                    + Vector3.down * _verticalVelocity;
                characterController.Move(velocity * Time.deltaTime);
            }

            if (_look != Vector2.zero)
            {
                transform.Rotate(Vector3.up * _look.x * lookSensitivity);
                cameraTransform.rotation *= Quaternion.Euler(-_look.y * Time.deltaTime, 0, 0);
            }
        }
        
        #region Input Callbacks
        void Inputs.IPlayerActions.OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        void Inputs.IPlayerActions.OnLook(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
        }

        void Inputs.IPlayerActions.OnInteract(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        void Inputs.IPlayerActions.OnJump(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        void Inputs.IPlayerActions.OnSprint(InputAction.CallbackContext context)
        {
            _movementSpeed = context.ReadValue<bool>() ? runSpeed : walkSpeed;
        }
        #endregion
    }
}