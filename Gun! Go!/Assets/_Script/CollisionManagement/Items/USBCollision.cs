using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class USBCollision : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            USB_OP.Instance?.ReturnUSB(transform.parent.gameObject);
        }
    }
}
