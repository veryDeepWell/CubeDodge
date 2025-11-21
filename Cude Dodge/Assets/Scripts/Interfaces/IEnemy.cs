using System.Collections;
using UnityEngine;

public interface IEnemy
{
    void BeginAttack();

    void ForeshadowEnemy();

    IEnumerator DelayedAction(float timeToWait, GameObject foreshadowObject);
}
