using UnityEngine;
using System.Collections.Generic;

// For wizards whose ties effect objects.
public class WizardTieHolder : ATie
{
    [SerializeField]
    private List<ATie> _wizardTies;

    public override void TieClicked()
    {
        foreach (ATie tie in _wizardTies)
        {
            tie.TieClicked();
        }
    }
}
