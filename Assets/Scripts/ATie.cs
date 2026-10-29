using UnityEngine;

// To Do: make this an abstract class, may not need to be one depending on how much we differentiate the ties
public abstract class ATie : MonoBehaviour
{

    protected virtual void Start()
    {
    }

    public virtual void TieClicked()
    {

    }

    protected virtual void OnDestroy()
    {
    }
}
