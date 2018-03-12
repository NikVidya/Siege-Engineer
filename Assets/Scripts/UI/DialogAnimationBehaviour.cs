using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationBehaviour : StateMachineBehaviour {

    enum StateTransitionState
    {
        ENTER,
        EXIT
    }
    [SerializeField]
    StateTransitionState state;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        DialogUIController controller = animator.gameObject.GetComponent<DialogUIController>();
        if(controller == null)
        {
            Debug.LogErrorFormat("DialogAnimationBehaviour attached to animator on something without a DialogUIController. Remove this animation from {0}", animator.name);
            return;
        }

        switch(state)
        {
            case StateTransitionState.ENTER:
                controller.OnFinishedShowDialog();
                break;
            case StateTransitionState.EXIT:
                controller.OnFinishedHideDialog();
                break;
        }
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
