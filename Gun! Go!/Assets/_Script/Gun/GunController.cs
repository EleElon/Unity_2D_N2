using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class GunController : MonoBehaviour {
    float rotateOffset = 180f;

    private void Update() {
        GunRotation();
    }

    void GunRotation() {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
            return;

        Vector3 displayeMent = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displayeMent.y, displayeMent.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        if (angle < -90 || angle > 90) {
            transform.localScale = new Vector2(1, 1);
            transform.localPosition = new Vector2(0.4f, -0.3f);
        }
        else if (angle > -90 || angle < 90) {
            transform.localScale = new Vector2(1, -1);
            transform.localPosition = new Vector2(-0.4f, -0.3f);
        }
    }
}
