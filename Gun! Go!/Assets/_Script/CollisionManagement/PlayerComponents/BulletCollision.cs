using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BulletCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int damage;

    internal void SetBulletDamage(int dmg) {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.TakeDMG(damage);
                if (GunController.Instance.GunSkillIsActivated()) {
                    PlayerBulletWithSkillOP.Instance?.ReturnBulletSkill(gameObject);
                }
                else if (!GunController.Instance.GunSkillIsActivated()) {
                    PlayerBulletOP.Instance?.ReturnBullet(gameObject);
                }

                EnemyHPManager _enemyHPManager = enemy.GetComponentInChildren<EnemyHPManager>();

                _enemyHPManager.SetLastDMGTakeDMG(Time.time);
            }
        }

        if (other.CompareTag("EnemyBullet")) {
            EnemyBulletController _enemyBulletController = other.GetComponent<EnemyBulletController>();

            if (_enemyBulletController != null) {
                _enemyBulletController.EnemyBulletTakeDMG(damage);

                if (GunController.Instance.GunSkillIsActivated()) {
                    PlayerBulletWithSkillOP.Instance?.ReturnBulletSkill(gameObject);
                }
                else if (!GunController.Instance.GunSkillIsActivated()) {
                    PlayerBulletOP.Instance?.ReturnBullet(gameObject);
                }
            }
        }

        if (other.CompareTag("Wall")) {
            if (GunController.Instance.GunSkillIsActivated()) {
                PlayerBulletWithSkillOP.Instance?.ReturnBulletSkill(gameObject);
            }
            else if (!GunController.Instance.GunSkillIsActivated()) {
                PlayerBulletOP.Instance?.ReturnBullet(gameObject);
            }

            AudioManager.Instance.PlaySFX(AudioManager.Instance.GetBulletHittingWithWallSound());
        }
    }
}
