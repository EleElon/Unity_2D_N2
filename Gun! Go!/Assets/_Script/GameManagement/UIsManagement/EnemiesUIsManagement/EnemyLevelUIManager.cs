using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
internal class EnemyLevelUIManager : MonoBehaviour {

    [Header("---------- Texts ----------")]
    [SerializeField] TextMeshProUGUI levelText;

    [Header("---------- Components ----------")]
    Enemy _enemy;

    void Awake() {
        _enemy = GetComponentInParent<Enemy>();
    }

    internal void SetEnemyLevelText(string txt) {
        if (levelText)
            levelText.text = txt;
    }
}
