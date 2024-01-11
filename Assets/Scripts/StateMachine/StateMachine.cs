using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine
{
    public IState current_state;

    public void ChangeState(IState new_state)
    {
        current_state?.OnExit();

        current_state = new_state;

        current_state.OnEnter();
    }

    public void OnUpdate()
    {
        current_state?.OnUpdate();
    }
    public void OnHandle()
    {
        current_state?.OnHandle();
    }

    public void OnFixedUpdate()
    {
        current_state?.OnFixedUpdate();
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        current_state?.OnTriggerEnter(collider);
    }
    public void OnTriggerStay(Collider2D collider)
    {
        current_state?.OnTriggerStay(collider);
    }
    public void OnTriggerExit(Collider2D collider)
    {
        current_state?.OnTriggerExit(collider);
    }

}


