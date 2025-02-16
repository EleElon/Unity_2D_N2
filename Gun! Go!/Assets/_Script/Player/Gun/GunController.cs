using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

internal class GunController : MonoBehaviour {

    internal static GunController Instance { get; private set; }

    [Header("---------- Variables ----------")]
    float rotateOffset = 180f;
    int maxBullets = 20, bulletsRemaining;
    int bulletDamage = 2, saveBulletDMG;
    float timeToReload = 2f;
    float shootDelay = 0.15f;
    float nextShoot;
    bool reloading;

    float skillCD = 27f, nextTimeToUseSkill, timeMaintain = 10f;
    bool skillIsActivated = false;

    [Header("---------- Components ----------")]
    [SerializeField] Transform shootingPoint;
    // [SerializeField] GameObject bulletPrefab;
    // ObjectPooling bulletPool;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        bulletsRemaining = maxBullets;

        UIsManager.Instance.SetBulletText("" + bulletsRemaining, "/" + maxBullets);
    }

    private void Update() {
        GunRotation();
        HandleShooting();
        HandleReload();
    }

    void HandleShooting() {
        if (Input.GetMouseButtonDown(0) && bulletsRemaining > 0 && Time.time > nextShoot && !reloading) {
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
        if (GameManager.Instance.IsGamePaused() || GameManager.Instance.IsGameOver())
            return;
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

        if (GameManager.Instance.IsGamePaused() || GameManager.Instance.IsGameOver())
            return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject bullet = skillIsActivated ? PlayerBulletWithSkillOP.Instance?.GetBulletSkill() : PlayerBulletOP.Instance?.GetBullet();
        bullet.transform.rotation = shootingPoint.rotation;
        bullet.transform.position = shootingPoint.position;

        bullet.GetComponent<BulletsController>().SetTarget(mousePosition);

        BulletCollision _bulletCollision = bullet.GetComponent<BulletCollision>();
        if (_bulletCollision != null) {
            _bulletCollision.SetBulletDamage(bulletDamage);
        }

        bulletsRemaining--;

        UIsManager.Instance.SetBulletText("" + bulletsRemaining, "/" + maxBullets);
    }

    internal IEnumerator RageBullet() {
        int energyCost = 30;

        if (Time.time >= nextTimeToUseSkill && PlayerController.Instance.GetCurrentEnergy() >= energyCost) {
            saveBulletDMG = bulletDamage;
            SetBuffForRageBullet();
            PlayerController.Instance.UsedEnergy(energyCost);
            skillIsActivated = true;

            yield return new WaitForSeconds(timeMaintain);

            bulletDamage = saveBulletDMG;
            nextTimeToUseSkill = Time.time + skillCD;
            skillIsActivated = false;
        }
    }

    int SetBuffForRageBullet() {
        bulletDamage = Mathf.RoundToInt(bulletDamage * 1.4f);
        return bulletDamage;
    }

    internal int SetBulletDamageWhenLevelUp() {
        return bulletDamage = Mathf.RoundToInt(bulletDamage + (bulletDamage * 1.2f) - PlayerController.Instance.GetPlayerLevel());
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
        yield return new WaitForSeconds(timeToReload);

        bulletsRemaining = maxBullets;
        reloading = false;
        UIsManager.Instance.SetBulletText("" + bulletsRemaining, "/" + maxBullets);
    }

    internal bool IsReloading() {
        return reloading;
    }
}
