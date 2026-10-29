using UnityEngine;

public class PlayerTies : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMove;
    // To do: Change to use action callbacks
    public static PlayerTies Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple TurnManagers found; destroying the duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ChangePlayerLocation(Transform transform)
    {
        _playerMove.ChangeLocation(transform.position);
    }

    public void UpdatePlayerSpeed(float newMoveSpeed)
    {
        _playerMove.ChangeMoveSpeed(newMoveSpeed);
    }
}
