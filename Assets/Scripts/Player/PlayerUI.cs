using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _healthText;
    public void ChangeHealthValue(int newHealth)
    {
        _healthText.text = $"Health: {newHealth}";
    }
}
