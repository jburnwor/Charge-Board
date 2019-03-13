using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ToIdle")]
public class ToIdleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool stop = PlayerReady(controller);
        return stop;
    }

    private bool PlayerReady(StateController controller)
    {
        
        //if player in range go to chase state
        if (GameManager.freezeTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
