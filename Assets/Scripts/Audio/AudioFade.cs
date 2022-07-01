using UnityEngine;
using System.Collections;

//Useful class from thread https://forum.unity.com/threads/fade-out-audio-source.335031/ 
public static class AudioFade
{

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}