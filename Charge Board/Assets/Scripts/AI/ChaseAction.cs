using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {

        controller.navMeshAgent.SetDestination(controller.chaseTarget.position);
        controller.FaceTarget();

        controller.shotCounter -= Time.deltaTime;
        if (controller.shotCounter <= 0)
        {
            controller.shotCounter = controller.timeBetweenShots;
            Rigidbody newBullet = Instantiate(controller.bullet, controller.firePoint.position, controller.firePoint.rotation);
            newBullet.velocity = controller.transform.forward * controller.bulletSpeed + controller.RB.velocity;
            Destroy(newBullet.gameObject, 2);

        }
    }
}
