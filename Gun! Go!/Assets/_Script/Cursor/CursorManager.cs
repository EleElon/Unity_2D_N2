using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CursorManager : MonoBehaviour {
    internal static CursorManager Instance { get; private set; }

    [Header("---------- Variable ----------")]
    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D shootCursor;
    [SerializeField] Texture2D reloadCursor;

    Vector2 hotspot = new Vector2(16, 48);

    private Texture2D currentCursor;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
    }

    // private void Update() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         Cursor.SetCursor(shootCursor, hotspot, CursorMode.Auto);
    //     }
    //     else if (Input.GetMouseButtonUp(0)) {
    //         Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
    //     }

    //     if (GunController.Instance.IsReloading()) {
    //         Cursor.SetCursor(reloadCursor, hotspot, CursorMode.Auto);
    //     }
    //     else {
    //         Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
    //     }
    // }


    private void Update() {
        Texture2D newCursor = normalCursor;

        if (GunController.Instance.IsReloading()) {
            newCursor = reloadCursor;
        }
        else if (Input.GetMouseButton(0)) {
            newCursor = shootCursor;
        }

        if (newCursor != currentCursor) {
            Cursor.SetCursor(newCursor, hotspot, CursorMode.Auto);
            currentCursor = newCursor;
        }
    }
}
