using Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
    {
        [SerializeField] private new CharacterController characterController;
        [SerializeField] private Transform cameraTransform;
        
        private Inputs _inputs;
        private Vector2 _look;
        private Vector2 _movement;

        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        private float _speed;

        private void Awake()
        {
            _inputs = new Inputs();
            _inputs.Player.SetCallbacks(this);
        }

        private void OnEnable() => _inputs.Player.Enable();

        private void OnDisable() => _inputs.Player.Disable();

        private void Update()
        {
            if (_movement != Vector2.zero)
            {
                var localisedMovement = transform.right * _movement.x
                                             + transform.forward * _movement.y;
                characterController.Move(localisedMovement * (Time.deltaTime * _speed));
            }

            if (_look != Vector2.zero)
            {
                //rigidbody.rotation *= Quaternion.Euler(0, _look.x * Time.deltaTime, 0);
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
            _speed = context.ReadValue<bool>() ? runSpeed : walkSpeed;
        }
        #endregion
    }
}