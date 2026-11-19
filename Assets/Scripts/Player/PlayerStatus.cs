using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private PlayerUI _ui;
    [SerializeField]
    private int _health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ui.ChangeHealthValue(_health);
    }

    public void DealDamage(int damage)
    {
        _health -= damage;
        _ui.ChangeHealthValue(_health);
        if (_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
