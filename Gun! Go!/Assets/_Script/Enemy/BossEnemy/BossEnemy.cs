using System.Collections;
using UnityEngine;

internal class BossEnemy : Enemy, IEnemy, IBossEnemy {

    [Header("---------- Variables ----------")]
    protected override float moveSpeed { get; } = 0.25f;
    protected override int damageDeal { get; } = 35;
    int bulletDMG = 18;
    bool isMoving;
    protected int maxHP = 500;
    protected int currentHP;

    int maxRage = 100, currentRage;

    int healingValue = 100;

    [SerializeField] Transform shootPosition;

    float useSkillCD = 4.5f, nextTimeToUseSkill;
    float normalShootCD = 2f, nextTimeToUseNormalShoot;
    float circleShootCD = 3.5f, nextTimeToUseCircleShoot;
    float healingCD = 14.3f, nextTimeToUseHealing;
    float healingPerValueCD = 25f, nextTimeToUseHealingPerValue;
    float summonCD = 8.5f, nextTimeToUseSummon;
    float teleportCD = 15f, nextTimeToUseTeleport;

    enum EnemyState { Idle, Moving }
    EnemyState state;

    Vector2 targetPosition;
    float idleDuration = 3f, movingDuration = 12f;
    float stateChangeTime;

    public int GetEnemiesMaxHP() => maxHP;
    public int GetEnemiesCurrentHP() => currentHP;
    public int GetBossEnemyMaxRage() => maxRage;
    public int GetBossEnemyRage() => currentRage;

    [Header("---------- Components ----------")]
    EnemyHPManager _enemyHPManager;
    RageBarManager _rageBarManager;
    Animator _animator;

    private void Awake() {
        currentHP = maxHP;
        currentRage = 0;

        _enemyHPManager = GetComponentInChildren<EnemyHPManager>();
        _rageBarManager = GetComponentInChildren<RageBarManager>();
        _animator = GetComponent<Animator>();

        ChooseRandomState();
    }

    protected override void Update() {
        switch (state) {
            case EnemyState.Idle:
                if (Time.time >= stateChangeTime) {
                    ChooseRandomState();
                }
                break;
            case EnemyState.Moving:
                if (currentRage >= 70) {
                    base.Update();
                }
                else {
                    MoveToTarget();
                    FlipEnemy();
                }
                break;
        }

        if (PlayerController.Instance != null) {
            UsingSkill();
        }

        UpdateAnimation();

        if (currentHP <= 0) {
            Die();
        }
    }

    protected override void FlipEnemy() {
        if (currentRage >= 70) {
            base.FlipEnemy();
        }
        else {
            transform.localScale = new Vector3(targetPosition.x < transform.position.x ? -1 : 1, 1, 1);
        }

        FlipHPBar();
        FlipRageBar();
    }

    void FlipHPBar() {
        if (currentRage >= 70) {
            _enemyHPManager.transform.localScale = new Vector3(PlayerController.Instance?.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
        else {
            _enemyHPManager.transform.localScale = new Vector3(targetPosition.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    void FlipRageBar() {
        if (currentRage >= 70) {
            _rageBarManager.transform.localScale = new Vector3(PlayerController.Instance?.transform.position.x < transform.position.x ? -2 : 2, 0.5f, 1);
            _rageBarManager.transform.localPosition = new Vector3(PlayerController.Instance?.transform.position.x < transform.position.x ? 1.611f : 1.361f, -0.499f, 3.932711f);
        }
        else {
            _rageBarManager.transform.localScale = new Vector3(targetPosition.x < transform.position.x ? -2 : 2, 0.5f, 1);
            _rageBarManager.transform.localPosition = new Vector3(targetPosition.x < transform.position.x ? 1.611f : 1.361f, -0.499f, 3.932711f);
        }
    }

    internal override void TakeDMG(int dmg) {
        currentHP -= dmg;
        currentRage++;
    }

    internal override float GetMoveSpeed() {
        return moveSpeed;
    }

    internal override int GetDamageDeal() {
        return damageDeal;
    }

    internal override void Die() {
        GameObject usb = USB_OP.Instance?.GetUSB();
        usb.transform.position = transform.position;

        BossEnemyOP.Instance?.ReturnBossEnemy(transform.parent.gameObject);
    }

    void NormalShoot() {
        if (PlayerController.Instance != null) {
            Vector3 directionToPlayer = (PlayerController.Instance.transform.position - shootPosition.position).normalized;

            GameObject bullet = EnemyBulletOP.Instance?.GetEnemyBullet();
            EnemyBulletController _enemyBulletController = bullet.GetComponent<EnemyBulletController>();
            EnemyBulletCollision _enemyBulletCollision = bullet.GetComponent<EnemyBulletCollision>();

            if (_enemyBulletController != null) {
                _enemyBulletCollision.SetBulletDamage(bulletDMG);
            }

            _enemyBulletController.setMoveDireciton(directionToPlayer * _enemyBulletController.GetBulletSpeed());

            bullet.transform.position = shootPosition.position;
        }
    }

    IEnumerator NormalShootWithFullRage() {
        if (PlayerController.Instance != null) {
            int bulletAmount = 12;
            Vector3 directionToPlayer = (PlayerController.Instance.transform.position - shootPosition.position).normalized;

            for (int i = 0; i < bulletAmount; i++) {
                GameObject bullet = EnemyBulletOP.Instance?.GetEnemyBullet();
                EnemyBulletController _enemyBulletController = bullet.AddComponent<EnemyBulletController>();
                EnemyBulletCollision _enemyBulletCollision = bullet.GetComponent<EnemyBulletCollision>();

                if (_enemyBulletController != null) {
                    _enemyBulletCollision.SetBulletDamage(bulletDMG);
                }

                _enemyBulletController.setMoveDireciton(directionToPlayer * _enemyBulletController.GetBulletSpeed());

                bullet.transform.position = shootPosition.position;

                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    void CircleShoot() {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++) {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);

            GameObject bullet = EnemyBulletOP.Instance?.GetEnemyBullet();
            EnemyBulletController _enemyBulletController = bullet.GetComponent<EnemyBulletController>();
            EnemyBulletCollision _enemyBulletCollision = bullet.GetComponent<EnemyBulletCollision>();

            if (_enemyBulletController != null) {
                _enemyBulletCollision.SetBulletDamage(bulletDMG);
            }

            _enemyBulletController.setMoveDireciton(bulletDirection * _enemyBulletController.GetBulletSpeed());

            bullet.transform.position = gameObject.transform.position;
        }
    }

    IEnumerator CircleShootWithFullRage() {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < 3; i++) {
            for (int o = 0; o < bulletCount; o++) {
                float angle = o * angleStep;

                Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);

                GameObject bullet = EnemyBulletOP.Instance?.GetEnemyBullet();
                EnemyBulletController _enemyBulletController = bullet.GetComponent<EnemyBulletController>();
                EnemyBulletCollision _enemyBulletCollision = bullet.GetComponent<EnemyBulletCollision>();

                if (_enemyBulletController != null) {
                    _enemyBulletCollision.SetBulletDamage(bulletDMG);
                }

                _enemyBulletController.setMoveDireciton(bulletDirection * _enemyBulletController.GetBulletSpeed());

                bullet.transform.position = gameObject.transform.position;
            }
            yield return new WaitForSeconds(i < 2 ? 1.3f : 2.7f);
        }
    }

    void Summon() {
        int summonCount = Random.Range(1, 6);

        for (int i = 0; i < summonCount; i++) {
            GameObject miniEnemy = MiniEnemyOP.Instance?.GetMiniEnemy();

            miniEnemy.transform.position = shootPosition.position;
        }
    }

    IEnumerator SummonWithFullRage() {
        int summonCount = Random.Range(3, 6);

        GameObject enemy = null;

        for (int i = 0; i < summonCount; i++) {
            int randomTypeEnemy = Random.Range(0, 4);

            switch (randomTypeEnemy) {
                case 0:
                    enemy = BasicEnemyOP.Instance?.GetBasicEnemy();
                    break;
                case 1:
                    enemy = EnergyEnemyOP.Instance?.GetEnergyEnemy();
                    break;
                case 2:
                    enemy = HealerEnemyOP.Instance?.GetHealerEnemy();
                    break;
                case 3:
                    enemy = ExplosionEnemyOP.Instance?.GetExplosionEnemy();
                    break;
            }
            if (enemy != null) {
                enemy.transform.position = shootPosition.position;
            }
            yield return new WaitForSeconds(1.2f);
        }
    }

    void SummonWithFullRage1() {
        const int miniEnemyCount = 12;
        float angleStep = 360f / miniEnemyCount;

        for (int i = 0; i < miniEnemyCount; i++) {
            float angle = i * angleStep;

            Vector3 miniEnemyDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);

            GameObject miniEnemy = MiniEnemyOP.Instance?.GetMiniEnemy();
            miniEnemy.GetComponent<MiniEnemy>();

            miniEnemy.transform.position = gameObject.transform.position + miniEnemyDirection * 2f;
        }
    }

    void TelePort() {
        if (PlayerController.Instance != null) {
            float teleportRadius = 4f;
            Vector3 teleportPosition;

            do {
                Vector2 randomOffset = Random.insideUnitCircle * teleportRadius;
                teleportPosition = PlayerController.Instance.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            } while (!IsValidPosition(teleportPosition));

            transform.position = teleportPosition;
        }
    }

    bool IsValidPosition(Vector3 position) {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        return hit.collider == null;
    }


    void Healing(int hpAmount) {
        currentHP = Mathf.Min(currentHP + hpAmount, maxHP);
        _enemyHPManager.ResetEnemyHPBarState();
    }

    void HealingPerValue() {
        currentHP += (int)(((float)maxHP * 0.3f) + ((float)currentHP * 0.1f));
        _enemyHPManager.ResetEnemyHPBarState();
    }

    void RandomlySkillChoosing() {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill) {
            case 0:
                if (currentRage == maxRage) {
                    StartCoroutine(NormalShootWithFullRage());
                    currentRage = 0;
                }
                else if (Time.time >= nextTimeToUseNormalShoot && Time.time >= nextTimeToUseSkill) {
                    NormalShoot();
                    SetNextTimeToUseSkill(ref nextTimeToUseNormalShoot, normalShootCD);
                    SetNextTimeToUseSkill(ref nextTimeToUseSkill, useSkillCD);
                }
                break;
            case 1:
                if (currentRage == maxRage) {
                    StartCoroutine(CircleShootWithFullRage());
                    currentRage = 0;
                }
                else if (Time.time >= nextTimeToUseCircleShoot && Time.time >= nextTimeToUseSkill) {
                    CircleShoot();
                    SetNextTimeToUseSkill(ref nextTimeToUseCircleShoot, circleShootCD);
                    SetNextTimeToUseSkill(ref nextTimeToUseSkill, useSkillCD);
                }
                break;
            case 2:
                int lowHPValue = (int)(maxHP * 0.25f);

                if (currentHP <= lowHPValue && Random.value < 0.3f && Time.time >= nextTimeToUseHealingPerValue && Time.time >= nextTimeToUseSkill) {
                    HealingPerValue();

                    SetNextTimeToUseSkill(ref nextTimeToUseHealingPerValue, healingPerValueCD);
                    SetNextTimeToUseSkill(ref nextTimeToUseSkill, useSkillCD);
                }
                else if (Time.time >= nextTimeToUseHealing && Time.time >= nextTimeToUseSkill) {
                    Healing(healingValue);

                    SetNextTimeToUseSkill(ref nextTimeToUseHealing, healingCD);
                    SetNextTimeToUseSkill(ref nextTimeToUseSkill, useSkillCD);
                }
                break;
            case 3:
                if (currentRage == maxRage && Random.value < 0.5) {
                    StartCoroutine(SummonWithFullRage());
                    currentRage = 0;
                }
                else if (currentRage == maxRage && Random.value > 0.5) {
                    SummonWithFullRage1();
                    currentRage = 0;
                }
                else if (Time.time >= nextTimeToUseSummon && Time.time >= nextTimeToUseSkill) {
                    Summon();
                    SetNextTimeToUseSkill(ref nextTimeToUseSummon, summonCD);
                    SetNextTimeToUseSkill(ref nextTimeToUseSkill, useSkillCD);
                }
                break;
            case 4:
                if (Time.time >= nextTimeToUseTeleport && Time.time >= nextTimeToUseSkill) {
                    TelePort();
                    SetNextTimeToUseSkill(ref nextTimeToUseTeleport, teleportCD);
                    SetNextTimeToUseSkill(ref nextTimeToUseSkill, useSkillCD);
                }
                break;
        }
    }

    void UsingSkill() {
        RandomlySkillChoosing();
    }

    void SetNextTimeToUseSkill(ref float nxtTime, float skillCD) {
        nxtTime = Time.time + skillCD;
    }

    void MoveToTarget() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if ((Vector2)transform.position == targetPosition || Time.time >= stateChangeTime) {
            ChooseRandomState();
        }
    }

    Vector2 GetRamdomPosition() {
        float x = Random.Range(-10f, 9f);
        float y = Random.Range(-8f, 6f);

        return new Vector2(x, y);
    }

    void ChooseRandomState() {
        if (Random.value > 0.5f) {
            state = EnemyState.Idle;
            isMoving = false;
            stateChangeTime = Time.time + idleDuration;
        }
        else {
            state = EnemyState.Moving;
            isMoving = true;
            targetPosition = GetRamdomPosition();
            stateChangeTime = Time.time + movingDuration;
        }
    }

    void UpdateAnimation() {
        bool isMovingAnimation = isMoving;

        _animator.SetBool("IsMoving", isMovingAnimation);
    }

    protected override void ResetEnemyState() {
        currentHP = maxHP;
        // ResetParentGameObjectPosition();
        // ResetGameObjectPosition();
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
