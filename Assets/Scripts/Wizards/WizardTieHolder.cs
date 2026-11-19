using UnityEngine;
using System.Collections.Generic;

// For wizards whose ties effect objects.
public class WizardTieHolder : ATie
{
    [SerializeField]
    private List<ATie> _wizardTies;
    [SerializeField]
    private Transform _tieTransform;

    public override void TieClicked()
    {
        foreach (ATie tie in _wizardTies)
        {
            tie.TieClicked();
        }
    }

    public override void FormTie()
    {
        _tieTransform.gameObject.SetActive(true);
        foreach (ATie tie in _wizardTies)
        {
            tie.FormTie();
        }
        DistanceJoint2D tJoint = this.gameObject.GetComponent<DistanceJoint2D>();
        if (!tJoint) { return; }
        tJoint.enabled = true;
    }
}
