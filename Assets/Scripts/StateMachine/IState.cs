using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    public void OnEnter();
    public void OnHandle();
    public void OnUpdate();
    public void OnFixedUpdate();
    public void OnExit();

    public void OnTriggerEnter(Collider2D collider);
    public void OnTriggerStay(Collider2D collider);
    public void OnTriggerExit(Collider2D collider);
}
