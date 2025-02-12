using UnityEngine;

internal class BossEnemy : Enemy, IEnemy {

    [Header("---------- Variables ----------")]
    protected override float moveSpeed { get; } = 0.25f;
    protected override int damageDeal { get; } = 35;
    protected int maxHP = 250;
    protected int currentHP;
    int healingValue = 100;

    [SerializeField] GameObject enemyBullet;
    [SerializeField] GameObject miniEnemy;
    [SerializeField] Transform shootPosition;

    float canUseSkillCoolDown = 7f;
    float nextTimeToUseSkill;

    public int GetEnemiesMaxHP() => maxHP;
    public int GetEnemiesCurrentHP() => currentHP;

    [Header("---------- Components ----------")]
    EnemyHPManager _enemyHPManager;

    private void Awake() {
        currentHP = maxHP;

        _enemyHPManager = GetComponentInChildren<EnemyHPManager>();
    }

    protected override void Update() {
        base.Update();

        if (Time.time >= nextTimeToUseSkill) {
            UsingSkill();
        }

        if (currentHP <= 0) {
            Die();
        }
    }

    protected override void FlipEnemy() {
        base.FlipEnemy();

        if (PlayerController.Instance != null) {
            _enemyHPManager.transform.localScale = new Vector3(PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    internal override void TakeDMG(int dmg) {
        currentHP -= dmg;
    }

    protected override float GetMoveSpeed() {
        return moveSpeed;
    }

    internal override int GetDamageDeal() {
        return damageDeal;
    }

    internal override void Die() {
        // BasicEnemyOP.Instance.ReturnBasicEnemy(transform.parent.gameObject);
    }

    void NormalShoot() {
        if (PlayerController.Instance != null) {
            Vector3 directionToPlayer = PlayerController.Instance.transform.position - shootPosition.position.normalized;
            GameObject bullet = Instantiate(enemyBullet, shootPosition.position, Quaternion.identity);
            EnemyBulletController _enemyBulletController = bullet.AddComponent<EnemyBulletController>();
            _enemyBulletController.setMoveDireciton(directionToPlayer * _enemyBulletController.GetBulletSpeed());
        }
    }

    void CircleShoot() {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount; ;
        for (int i = 0; i < bulletCount; i++) {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
            GameObject bullet = Instantiate(enemyBullet, shootPosition.position, Quaternion.identity);
            EnemyBulletController _enemyBulletController = bullet.AddComponent<EnemyBulletController>();
            _enemyBulletController.setMoveDireciton(bulletDirection * _enemyBulletController.GetBulletCircleSpeed());
        }
    }

    void Summon() {
        Instantiate(miniEnemy, shootPosition.position, Quaternion.identity);
    }

    void TelePort() {
        if (PlayerController.Instance != null) {
            transform.position = PlayerController.Instance.transform.position;
        }
    }

    void Healing(int hpAmout) {
        currentHP = Mathf.Min(currentHP + hpAmout, maxHP);
    }

    void RandomlySkillChoosing() {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill) {
            case 0:
                NormalShoot();
                break;
            case 1:
                CircleShoot();
                break;
            case 2:
                Healing(healingValue);
                break;
            case 3:
                Summon();
                break;
            case 4:
                TelePort();
                break;
        }
    }

    void UsingSkill() {
        nextTimeToUseSkill = Time.time + canUseSkillCoolDown;
        RandomlySkillChoosing();
    }

    protected override void ResetEnemyState() {
        currentHP = maxHP;
        ResetParentGameObjectPosition();
        ResetGameObjectPosition();
    }

    protected override void ResetGameObjectPosition() {
        transform.position = Vector2.zero;
    }

    protected override void ResetParentGameObjectPosition() {
        if (transform.parent != null) {
            transform.parent.position = Vector2.zero;
        }
    }
}
