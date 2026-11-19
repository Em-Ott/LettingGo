using UnityEngine;
using System.Collections.Generic;

// For wizards whose ties effect objects.
public class WizardTieHolder : ATie
{
    [SerializeField]
    private List<ATie> _wizardTies;
    [SerializeField]
    private Transform _tieTransform;
    [SerializeField]
    private bool _startWithTie;

    protected override void Start()
    {
        if (!_startWithTie)
        {
            TieClicked(_tieTransform);
        }
    }

    public override void TieClicked(Transform t)
    {
        DistanceJoint2D tJoint = t.GetComponentInParent<DistanceJoint2D>();
        foreach (ATie tie in _wizardTies)
        {
            tie.TieClicked(t);
        }
        t.gameObject.SetActive(false);
        if (!tJoint) { return; }
        tJoint.enabled = false;
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
