using UnityEngine;
using UnityEngine.Audio;

namespace Fruit.Audio
{
    public class AudioManager : MonoBehaviour 
    {
        public AudioMixer audioMixer;
        public GameObject audioSourceBGMGObj;
        public GameObject audioSourceSFXGObj;

        AudioSource audioSourceBGM;
        AudioSource audioSourceSFX;

        void OnEnable() 
        {
            DontDestroyOnLoad(this.gameObject);   

            audioSourceBGM = audioSourceBGMGObj.GetComponent<AudioSource>();
            audioSourceSFX = audioSourceSFXGObj.GetComponent<AudioSource>();
        }

        public void PlayBGM(AudioClip audioClip)
        {            
            audioSourceBGM.clip = audioClip;
            audioSourceBGM.Play();
        }

        public void PlaySFX(AudioClip audioClip)
        {            
            audioSourceSFX.clip = audioClip;
            audioSourceSFX.Play();
        }

        public void ToggleAudioVolume(string audioMixerName)
        {
            switch (audioMixerName)
            {
                case "BGM" :audioSourceBGM.mute = !audioSourceBGM.mute;                                                                        
                            break;
                case "SFX" :audioSourceSFX.mute = !audioSourceSFX.mute;                              
                            break;
                default :   AudioListener.volume = (AudioListener.volume == 0f) ? 1f : 0f;
                            break;
            }
        }

        public void ControlAudioVolume(string audioMixerName, float volume)
        {
            switch (audioMixerName)
            {
                case "BGM" :audioMixer.SetFloat(audioMixerName, volume);                    
                            break;
                case "SFX" :audioMixer.SetFloat(audioMixerName, volume);   
                            break;
                default :   audioMixer.SetFloat(audioMixerName, volume);
                            break;
            }
        }

        /*
        void OnGUI()
        {
            GUILayout.Label("Review output in the console:");
            
            if (GUILayout.Button("ToggleMasterVolume")) {
                ToggleAudioVolume("Master");
            }

            if (GUILayout.Button("ToggleBGMVolume")) {
                ToggleAudioVolume("BGM");
            }
            
            if (GUILayout.Button("ToggleSFXVolume")) {
                ToggleAudioVolume("SFX");
            }
        }
        */               
    }
}