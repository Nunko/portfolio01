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
                case "BGM" :OnOffMasterVolume(audioMixerName);                                                                                                
                            break;
                case "SFX" :OnOffMasterVolume(audioMixerName);                              
                            break;
                default :   AudioListener.volume = (AudioListener.volume == 0f) ? 1f : 0f;
                            break;
            }
        }

        void OnOffMasterVolume(string audioMixerName)
        {
            string masterName = audioMixerName + "Master";
            float masterVolume = (MasterVolume(masterName) == 0f) ? -80f : 0f;
            audioMixer.SetFloat(masterName, masterVolume);  
        }

        float MasterVolume(string masterName)
        {
            float masterVolume = -80f;
            if (audioMixer.GetFloat(masterName, out masterVolume)) return masterVolume;
            else return -80f;               
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