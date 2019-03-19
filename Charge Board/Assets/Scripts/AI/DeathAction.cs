using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Death")]
public class DeathAction : Action
{
    float random;
    public override void Act(StateController controller)
    {
        Death(controller);
    }

    private void Death(StateController controller)
    {
        
        Destroy(controller.gameObject);
        
    }

    
}
