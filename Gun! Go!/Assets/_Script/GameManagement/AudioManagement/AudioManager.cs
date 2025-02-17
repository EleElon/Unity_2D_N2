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

    [Header("---------- Audio Clips ----------")]
    [SerializeField] AudioClip shootingSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip outOfBulletSound;
    [SerializeField] AudioClip hittingSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip loseSound;

    int currentMusicTrackIndex = -1;

    void Start() {
        if (musicTracks.Count > 0) {
            PlayRandomMusic();
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
}
