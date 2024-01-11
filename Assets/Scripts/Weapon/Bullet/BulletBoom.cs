using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoom : MonoBehaviour
{
    public void OnAnimationExit()
    {
        Destroy(gameObject);
    }
}
