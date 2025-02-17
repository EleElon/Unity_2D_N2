using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerController : MonoBehaviour {

    internal static PlayerController Instance { get; private set; }

    [Header("---------- Variables ----------")]
    Vector2 moveSpeed = Vector2.zero;
    float maxSpeed = 9f, giaToc = 5f, giaTocGiam = 15f;
    int maxHP = 100, currentHP, checkHP;
    int maxHPBottle = 80, currentHPBottle;
    int maxEnergy = 50, currentEnergy;
    bool ismoving;

    int level = 1, baseExp = 60, currentExp, checkLevel;
    float growthRate = 1.5f;

    [SerializeField] LayerMask wallLayer;

    [Header("---------- Components ----------")]
    Rigidbody2D _rb;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        checkHP = currentHP = maxHP;
        currentEnergy = maxEnergy;

        UIsManager.Instance.SetLevelText("Lv: " + level);
    }

    private void Update() {
        if (GameManager.Instance.IsGameOver()) return;
        HandleMovement();
        UpdateAnimation();
        UpdatePlayerBloodAnimation();
        HandleInput();

        if (currentHP <= 0) {
            Die();
        }

        if (currentHP > maxHP) {
            currentHP = maxHP;
        }

        if (currentHPBottle > maxHPBottle) {
            currentHPBottle = maxHPBottle;
        }

        if (currentEnergy > maxEnergy) {
            currentEnergy = maxEnergy;
        }
        LevelUp();
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            HealingSkill();
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            GunController.Instance?.StartCoroutine(GunController.Instance.RageBullet());
        }
    }

    void HandleMovement() {
        // float x_Posi = Input.GetAxisRaw("Horizontal");
        // float y_Posi = Input.GetAxisRaw("Vertical");

        // Vector2 direction = new Vector2(x_Posi, y_Posi).normalized;

        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (IsBlockedInDerection(direction)) {
            moveSpeed = new Vector2(0.1f, 0.1f);
        }
        else {
            Move(direction);
        }

        transform.Translate(moveSpeed * Time.deltaTime);

        // if (direction.x > 0) {
        //     transform.localScale = new Vector2(1, 1);
        // }
        // else if (direction.x < 0) {
        //     transform.localScale = new Vector2(-1, 1);
        // }

        if (direction.x < 0) {
            _spriteRenderer.flipX = true;
        }
        else if (direction.x > 0) {
            _spriteRenderer.flipX = false;
        }

    }

    void Move(Vector2 direction) {
        if (direction.magnitude > 0) {
            moveSpeed = Vector2.MoveTowards(moveSpeed, direction * maxSpeed, giaToc * Time.deltaTime);
            ismoving = true;
        }
        else {
            moveSpeed = Vector2.MoveTowards(moveSpeed, Vector2.zero, giaTocGiam * Time.deltaTime);
            ismoving = false;
        }
    }

    bool IsBlockedInDerection(Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, wallLayer);
        return hit.collider != null;
    }

    void HealingSkill() {
        if (currentHPBottle <= 0)
            return;

        int neededHP = maxHP - currentHP;

        if (neededHP == 0)
            return;

        int hpTransfer = Mathf.Min(neededHP, currentHPBottle);

        currentHP += hpTransfer;
        currentHPBottle -= hpTransfer;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.GetHealingSound());
    }

    void UpdatePlayerBloodAnimation() {
        if (currentHP < checkHP) {
            GameObject bloods = BloodOP.Instance?.GetBlood();
            bloods.transform.position = gameObject.transform.position;
            checkHP = currentHP;
        }
    }

    void LevelUp() {
        while (currentExp >= ExpToLevelUp(level)) {
            currentExp -= ExpToLevelUp(level);

            level++;
            PlayerHPManager.Instance.ResetHPBarState();
            UIsManager.Instance.SetLevelText("Lv: " + level);
            GunController.Instance.SetBulletDamageWhenLevelUp();

            if (EnemySpawnManager.Instance.GetTimeSpawner() >= 2.8f) {
                EnemySpawnManager.Instance?.SetTimeSpawner(0.2f);
            }

            SetHPWhenLevelUp();
        }
    }

    int SetHPWhenLevelUp() {
        return maxHP = Mathf.RoundToInt((maxHP + (maxHP * 1.2f) + level) + 1);
    }

    internal void GainExp(int exp) {
        currentExp += exp;
    }

    int ExpToLevelUp(int level) {
        return Mathf.FloorToInt(baseExp * Mathf.Pow(growthRate, level - 1));
    }

    void UpdateAnimation() {
        bool isMovingAnimation = ismoving;

        _animator.SetBool("IsMoving", isMovingAnimation);
    }

    internal void TakeDMG(int dmg) {
        currentHP -= dmg;
    }

    internal void Heal(int healValue) {
        currentHP += healValue;
        checkHP = currentHP;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.GetHealingSound());
    }

    internal void TakeHPBall(int amount) {
        currentHPBottle += amount;
    }

    internal void TakeEnergy(int energy) {
        currentEnergy += energy;
    }

    internal void UsedEnergy(int energy) {
        currentEnergy -= energy;
    }

    void Die() {
        Destroy(gameObject);
        GameManager.Instance.SetGameOver(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.GetDeathSound());
    }

    internal int GetMaxHP() {
        return maxHP;
    }

    internal int GetCurrentHP() {
        return currentHP;
    }

    internal int GetMaxHPBottle() {
        return maxHPBottle;
    }

    internal int GetCurrentHPBottle() {
        return currentHPBottle;
    }

    internal int GetMaxEnergy() {
        return maxEnergy;
    }

    internal int GetCurrentEnergy() {
        return currentEnergy;
    }

    internal int GetMaxExp() {
        return baseExp;
    }

    internal int GetCurrentExp() {
        return currentExp;
    }

    internal int GetPlayerLevel() {
        return level;
    }
}
