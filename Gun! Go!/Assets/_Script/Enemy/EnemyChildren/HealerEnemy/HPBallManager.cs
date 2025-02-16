using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HPBallManager : MonoBehaviour {
    private void OnEnable() {
        StartCoroutine(TimeToDestroy());
    }

    IEnumerator TimeToDestroy() {
        yield return new WaitForSeconds(10f);

        if (gameObject.activeSelf) {
            HPBallOP.Instance?.ReturnHPBall(gameObject);
        }
    }
}
