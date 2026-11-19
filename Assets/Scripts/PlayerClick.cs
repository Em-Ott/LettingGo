using UnityEngine;

public class PlayerClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ClickMade(Vector2 mouseLocation)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mouseLocation);

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        if (!hit) { return; }

        if (hit.tag == "Tie")
        {
            TieClicked(hit.transform);
        }
    }

    private void TieClicked(Transform t)
    {
        ATie tie = t.GetComponentInParent<ATie>();
        Debug.Log(tie);
        if (tie) { tie.TieClicked(t); }
    }
}
