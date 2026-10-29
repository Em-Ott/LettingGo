using System.Threading.Tasks;
using UnityEngine;

public class TeleportationTie : ATie
{
    [SerializeField]
    private TeleportationTie _destinationTie;
    private bool _teleportOnCooldown = false;
    private bool _teleportEnabled = true;

    public override void TieClicked()
    {
        _teleportEnabled = false;
        _destinationTie.SetTeleportEnabled(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On trigger enter " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player interacted with");
            _ = _destinationTie.SetTeleportOnCooldown();
            if (!_teleportOnCooldown && _teleportEnabled && _destinationTie.gameObject.transform)
            {
                PlayerTies.Instance.ChangePlayerLocation(_destinationTie.gameObject.transform);
            }
        }
    }

    public void SetTeleportEnabled(bool enabled)
    {
        _teleportEnabled = enabled;
    }

    public async Task SetTeleportOnCooldown()
    {
        _teleportOnCooldown = true;
        await Awaitable.WaitForSecondsAsync(0.1f);
        _teleportOnCooldown = false;
    }
}
