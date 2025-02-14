using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ExplosionManager : MonoBehaviour {
    private void OnEnable() {
        StartCoroutine(TimeToDestroy());
    }

    IEnumerator TimeToDestroy() {
        yield return new WaitForSeconds(0.5f);

        if (gameObject.activeSelf) {
            ExplosionOP.Instance?.ReturnExplosion(gameObject);
        }
    }
}
