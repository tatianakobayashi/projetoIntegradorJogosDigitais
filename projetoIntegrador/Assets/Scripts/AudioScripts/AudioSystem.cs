using UnityEngine;
using System.Collections.Generic;

public class AudioSystem : MonoBehaviour
{
    // Estrutura para armazenar áudios associados a chaves
    [System.Serializable]
    public struct SoundEntry
    {
        public string key;
        public AudioClip audioClip;
    }

    // Lista de entradas de áudio
    public List<SoundEntry> soundEntries;

    private AudioSource audioSource;

    private void Awake()
    {
        // Obtém ou cria um objeto AudioSource para reprodução de áudio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        audioSource.volume = VolumeControl.volume;
    }

    // Chame este método para reproduzir um som pelo nome da chave
    public void PlaySound(string key)
    {
        // Procura o áudio correspondente à chave na lista
        SoundEntry soundEntry = soundEntries.Find(entry => entry.key == key);

        // Se encontrar, reproduz o áudio
        if (soundEntry.audioClip != null)
        {
            audioSource.clip = soundEntry.audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Áudio não encontrado para a chave: " + key);
        }

    }


    public void SetLooping(bool loop)
    {
        audioSource.loop = loop;
    }

    public void StopSound()
    {
        audioSource?.Stop();
    }
}