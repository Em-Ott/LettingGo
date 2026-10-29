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
        DistanceJoint2D tJoint = t.GetComponentInParent<DistanceJoint2D>();
        ATie tie = t.GetComponentInParent<ATie>();
        if (!tJoint) { return; }
        if (tie) { tie.TieClicked(); }
        Destroy(t.gameObject);
        Destroy(tJoint);
    }
}
