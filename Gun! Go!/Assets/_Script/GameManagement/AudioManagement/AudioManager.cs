using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class AudioManager : MonoBehaviour {

    internal static AudioManager Instance { get; private set; }

    [Header("---------- Audio Sources ----------")]
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    [Header("---------- Music Tracks ----------")]
    [SerializeField] List<AudioClip> musicTracks;

    [Header("---------- Player Audio Clips ----------")]
    [SerializeField] AudioClip shootingSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip outOfBulletSound;
    [SerializeField] AudioClip healingSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip itemSound;

    [Header("---------- Enemies Audio Clips ----------")]
    [SerializeField] AudioClip bulletHittingSound;
    [SerializeField] AudioClip bulletHittingWithWallSound;
    [SerializeField] AudioClip enemyHittingSound;
    [SerializeField] AudioClip enemyBulletSound;
    [SerializeField] AudioClip enemyBulletMaxRageSound;
    [SerializeField] AudioClip enemySummonSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip teleportSound;

    int currentMusicTrackIndex = -1;

    void Awake() {
        Instance = this;
    }

    void Start() {
        if (musicTracks.Count > 0) {
            // PlayRandomMusic();
            PlayMusic(0);
            StartCoroutine(CheckMusicEnd());
        }
    }

    void PlayRandomMusic() {
        if (musicTracks.Count > 1) {
            int randomIndex;
            do {
                randomIndex = Random.Range(0, musicTracks.Count);
            } while (randomIndex == currentMusicTrackIndex);

            currentMusicTrackIndex = randomIndex;
            PlayMusic(randomIndex);
        }
        else if (musicTracks.Count == 1) {
            PlayMusic(0);
        }
    }

    internal IEnumerator FadeOutAndChangeMusic(int trackIndex, float fadeDuration = 1.5f) {
        float startVolume = musicAudioSource.volume;

        while (musicAudioSource.volume > 0) {
            musicAudioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicAudioSource.Stop();
        musicAudioSource.clip = musicTracks[trackIndex];
        musicAudioSource.Play();

        while (musicAudioSource.volume < startVolume) {
            // musicAudioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            musicAudioSource.volume = startVolume;
            yield return null;
        }
    }


    IEnumerator CheckMusicEnd() {
        while (true) {
            if (!musicAudioSource.isPlaying && musicTracks.Count > 0) {
                PlayRandomMusic();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void PlayMusic(int trackIndex) {
        if (trackIndex >= 0 && trackIndex < musicTracks.Count) {
            musicAudioSource.clip = musicTracks[trackIndex];
            musicAudioSource.Play();
            currentMusicTrackIndex = trackIndex;
        }
    }

    internal void PlaySFX(AudioClip sfx) {
        sfxAudioSource.clip = sfx;
        sfxAudioSource.PlayOneShot(sfx);
    }

    internal AudioClip GetShootingSound() {
        return shootingSound;
    }

    internal AudioClip GetReloadSound() {
        return reloadSound;
    }

    internal AudioClip GetOutOfBulletSound() {
        return outOfBulletSound;
    }

    internal AudioClip GetBulletHittingSound() {
        return bulletHittingSound;
    }

    internal AudioClip GetBulletHittingWithWallSound() {
        return bulletHittingWithWallSound;
    }

    internal AudioClip GetEnemyHittingSound() {
        return enemyHittingSound;
    }

    internal AudioClip GetHealingSound() {
        return healingSound;
    }

    internal AudioClip GetDeathSound() {
        return deathSound;
    }

    internal AudioClip GetEnemyBulletSound() {
        return enemyBulletSound;
    }

    internal AudioClip GetEnemyBulletMaxRageSound() {
        return enemyBulletMaxRageSound;
    }

    internal AudioClip GetEnemySummonSound() {
        return enemySummonSound;
    }

    internal AudioClip GetExplosionSound() {
        return explosionSound;
    }

    internal AudioClip GetTelePortSound() {
        return teleportSound;
    }

    internal AudioClip GetItemSound() {
        return itemSound;
    }
}
