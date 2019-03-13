using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Death")]
public class DeathDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Death(controller);
        return targetInRange;
    }

    private bool Death(StateController controller)
    {


        if (controller.health <= 0f)
        {
            return true;
        }
        else
        {
            return false;

        }
    }
}
