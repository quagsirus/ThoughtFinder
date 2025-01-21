using System;
using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
    {
        private Rigidbody _rb;
        private Inputs _inputs;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            
            _inputs = new Inputs();
            _inputs.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _inputs.Player.Enable();
        }

        private void OnDisable()
        {
            _inputs.Player.Disable();
        }

        void Inputs.IPlayerActions.OnMove(InputAction.CallbackContext context)
        {
            _rb.AddForce(context.ReadValue<Vector2>());
        }

        void Inputs.IPlayerActions.OnLook(InputAction.CallbackContext context)
        {
            
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
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            
        }
    }
}