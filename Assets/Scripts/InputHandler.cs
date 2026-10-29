using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // MonoBehaviour Scripts
    [SerializeField]
    private PlayerMovement _playerMove;
    // TODO: Maybe this should be a C# script?
    [SerializeField]
    private PlayerClick _playerClick;
    private InputAction _movementAction;
    private InputAction _playerClickAction;
    void Awake()
    {
        _movementAction = InputSystem.actions.FindAction("Move");
        _playerClickAction = InputSystem.actions.FindAction("Attack");
        _movementAction.performed += HandleMovement;
        _playerClickAction.performed += HandleClick;
        _movementAction.canceled += CancelMovement;
    }

    private void HandleClick(InputAction.CallbackContext clickInformation)
    {
        if (!_playerClick) { return; }
        _playerClick.ClickMade(Pointer.current.position.ReadValue());
    }

    private void HandleMovement(InputAction.CallbackContext movementInformation)
    {
        if (movementInformation.performed)
        {
            Vector2 movementVector = movementInformation.ReadValue<Vector2>();
            float xMovement = movementVector.x;
            float yMovement = movementVector.y;
            //float yMovement = 0;

            _playerMove.UpdateMovement(xMovement, yMovement);
        }
    }

    private void CancelMovement(InputAction.CallbackContext movementInformation)
    {
        _playerMove.UpdateMovement(0, 0);
    }
}
