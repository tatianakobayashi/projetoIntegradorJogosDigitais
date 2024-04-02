using UnityEngine;
using System.Collections.Generic;

public class AudioSystem : MonoBehaviour
{
    // Estrutura para armazenar �udios associados a chaves
    [System.Serializable]
    public struct SoundEntry
    {
        public string key;
        public AudioClip audioClip;
    }

    // Lista de entradas de �udio
    public List<SoundEntry> soundEntries;

    private AudioSource audioSource;

    private void Awake()
    {
        // Obt�m ou cria um objeto AudioSource para reprodu��o de �udio
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

    // Chame este m�todo para reproduzir um som pelo nome da chave
    public void PlaySound(string key)
    {
        // Procura o �udio correspondente � chave na lista
        SoundEntry soundEntry = soundEntries.Find(entry => entry.key == key);

        // Se encontrar, reproduz o �udio
        if (soundEntry.audioClip != null)
        {
            audioSource.clip = soundEntry.audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("�udio n�o encontrado para a chave: " + key);
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