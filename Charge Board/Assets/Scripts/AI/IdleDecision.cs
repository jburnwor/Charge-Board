using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Idle")]
public class IdleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = PlayerReady(controller);
        return targetInRange;
    }

    private bool PlayerReady(StateController controller)
    {
        float distance = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);
        //if player in range go to chase state
        if (distance <= controller.lookRadius && !GameManager.freezeTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
