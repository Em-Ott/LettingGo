using UnityEngine;

// Currently does nothing other than provide distance
public class DistanceTie : ATie
{
    //[SerializeField]
    //private float _speedWithTie = 6f;
    protected override void Start()
    {
        //PlayerTies.Instance.UpdatePlayerSpeed(_speedWithTie);
    }

    protected override void OnDestroy()
    {
        Debug.Log("trying to destroy");
        //PlayerTies.Instance.UpdatePlayerSpeed(0f);
    }
}
