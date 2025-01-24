using Common;
using UnityEngine;
using UnityEngine.InputSystem;
using Yapper;

namespace Player
{
    public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private YapperManager yapperManager;
        
        private Inputs _inputs;
        private Vector2 _look;
        private Vector2 _movement;

        [SerializeField] private float downForce;
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
            Cursor.lockState = CursorLockMode.Locked;
            
            _movementSpeed = walkSpeed;
        }

        private void OnEnable() => _inputs.Player.Enable();

        private void OnDisable() => _inputs.Player.Disable();

        private void Update()
        {
            if (_movement != Vector2.zero || !Mathf.Approximately(_verticalVelocity, downForce))
            {
                var velocity = transform.right * (_movement.x * _movementSpeed)
                                    + transform.forward * (_movement.y * _movementSpeed)
                                    + Vector3.up * _verticalVelocity;
                characterController.Move(velocity * Time.deltaTime);
                _verticalVelocity = characterController.isGrounded ? downForce : _verticalVelocity + gravity * Time.deltaTime;
            }

            if (_look != Vector2.zero)
            {
                transform.Rotate(Vector3.up * (_look.x * lookSensitivity * Time.deltaTime));
                cameraTransform.Rotate(Vector3.left * (_look.y * lookSensitivity * Time.deltaTime));
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
            
        }

        void Inputs.IPlayerActions.OnJump(InputAction.CallbackContext context)
        {
            if (!context.ReadValueAsButton()) return;
            if (!characterController.isGrounded) return;
            _verticalVelocity = 10;
        }

        void Inputs.IPlayerActions.OnSprint(InputAction.CallbackContext context)
        {
            _movementSpeed = context.ReadValueAsButton() ? runSpeed : walkSpeed;
        }
        #endregion
    }
}