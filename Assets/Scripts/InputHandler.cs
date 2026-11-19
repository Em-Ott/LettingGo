using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    // MonoBehaviour Scripts
    [SerializeField]
    private PlayerMovement _playerMove;
    // TODO: Maybe this should be a C# script?
    [SerializeField]
    private PlayerClick _playerClick;
    [SerializeField]
    private PlayerInteract _playerInteract;
    private InputAction _movementAction;
    private InputAction _playerClickAction;
    private InputAction _playerRestartAction;
    private InputAction _playerKindleAction;
    void Awake()
    {
        _movementAction = InputSystem.actions.FindAction("Move");
        _playerClickAction = InputSystem.actions.FindAction("Attack");
        _playerRestartAction = InputSystem.actions.FindAction("Restart");
        _playerKindleAction = InputSystem.actions.FindAction("Kindle");
        _movementAction.performed += HandleMovement;
        _playerClickAction.performed += HandleClick;
        _movementAction.canceled += CancelMovement;
        _playerRestartAction.performed += HandleRestart;
        _playerKindleAction.performed += HandleKindleTie;
    }

    private void HandleClick(InputAction.CallbackContext clickInformation)
    {
        if (!_playerClick) { return; }
        _playerClick.ClickMade(Pointer.current.position.ReadValue());
    }

    private void HandleRestart(InputAction.CallbackContext restartInformation)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleKindleTie(InputAction.CallbackContext restartInformation)
    {
        _playerInteract.HandleKindleTie();
    }

    private void HandleMovement(InputAction.CallbackContext movementInformation)
    {
        if (movementInformation.performed)
        {
            Vector2 movementVector = movementInformation.ReadValue<Vector2>();
            float xMovement = movementVector.x;
            float yMovement = movementVector.y;

            _playerMove.UpdateMovement(xMovement, yMovement);
        }
    }

    private void CancelMovement(InputAction.CallbackContext movementInformation)
    {
        _playerMove.UpdateMovement(0, 0);
    }
}
