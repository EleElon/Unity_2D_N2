using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnergyManager : MonoBehaviour {
    private void OnEnable() {
        StartCoroutine(TimeToDestroy());
    }

    IEnumerator TimeToDestroy() {
        yield return new WaitForSeconds(10f);

        if (gameObject.activeSelf) {
            EnergyOP.Instance.ReturnEnergy(gameObject);
        }
    }
}
