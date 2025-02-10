using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BloodManager : MonoBehaviour {

    private void OnEnable() {
        StartCoroutine(TimeToDestroy());
    }

    IEnumerator TimeToDestroy() {
        yield return new WaitForSeconds(1f);

        if (gameObject.activeSelf) {
            BloodOP.Instance.ReturnBlood(gameObject);
        }
    }
}
