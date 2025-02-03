using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

internal class GunController : MonoBehaviour {
    internal static GunController Instance { get; private set; }

    [Header("---------- Variables ----------")]
    float rotateOffset = 180f;
    int maxBullets = 20;
    int bulletsRemaining;
    float shootDelay = 0.15f;
    float nextShoot;
    bool reloading;

    [Header("---------- Components ----------")]
    [SerializeField] Transform shootingPoint;
    // [SerializeField] GameObject bulletPrefab;

    // [Header("---------- Element ----------")]
    // ObjectPooling bulletPool;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        bulletsRemaining = maxBullets;
        // StartCoroutine(TimeToLoadBullet());
    }

    private void Update() {
        GunRotation();
        HandleShooting();
        HandleReload();
    }

    void HandleShooting() {
        if (Input.GetMouseButtonDown(0) && bulletsRemaining > 0 /*&& overLoad < 100*/ && Time.time > nextShoot && !reloading) {
            nextShoot = Time.time + shootDelay;
            Shoot();
        }
    }

    void HandleReload() {
        if (Input.GetKeyDown(KeyCode.R)) {
            if (bulletsRemaining == maxBullets)
                return;

            reloading = true;
            StartCoroutine(WaitToReload());
        }
    }

    void GunRotation() {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
            return;

        Vector3 displayment = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displayment.y, displayment.x) * Mathf.Rad2Deg;
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

    void Shoot() {
        //     Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        //     bulletsRemaining--;
        //     // overLoad += 10;

        GameObject bullet = PlayerBulletOP.Instance.GetBullet();
        bullet.transform.rotation = shootingPoint.rotation;
        bullet.transform.position = shootingPoint.position;
        bulletsRemaining--;
    }

    IEnumerator TimeToLoadBullet() {
        while (true) {
            if (bulletsRemaining < maxBullets) {
                yield return new WaitForSeconds(3f);
                bulletsRemaining++;
            }
            else {
                yield return null;
            }
        }
    }

    IEnumerator WaitToReload() {
        yield return new WaitForSeconds(2f);

        bulletsRemaining = maxBullets;
        reloading = false;
    }

    internal bool IsReloading() {
        return reloading;
    }
}
