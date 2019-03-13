using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : Action
{
    public override void Act(StateController controller)
    {
        Nothing(controller);
    }

    private void Nothing(StateController controller)
    {
        //add in idle animation
    }
}
