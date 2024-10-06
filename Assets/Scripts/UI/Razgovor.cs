using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Razgovor : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    [SerializeField] public float delayBeforeNextLine;
    private bool playerIsClose;
    private Coroutine typingCoroutine;
    private Coroutine delayCoroutine;

    [SerializeField] private AudioClip zgemoSound1;
    [SerializeField] private AudioClip zgemoSound2;
    [SerializeField] private AudioClip zgemoSound3;
    private AudioSource audioSource;

    private void Start()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerIsClose && dialoguePanel.activeInHierarchy)
        {
            NextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            ResetDialogue();
            StartDialogue();
            PlayRandomSound();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            ZeroText();
        }
    }

    private void StartDialogue()
    {
        if (!dialoguePanel.activeInHierarchy)
        {
            dialoguePanel.SetActive(true);
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = "";
        index = 0;
        typingCoroutine = StartCoroutine(Typing());
    }

    public void ResetDialogue()
    {
        index = 0;
        dialogueText.text = "";
    }

    public void ZeroText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
        }

        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        if (index >= dialogue.Length) yield break;

        dialogueText.text = "";
        foreach (char letter in dialogue[index])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        if (index < dialogue.Length - 1)
        {
            delayCoroutine = StartCoroutine(DelayBeforeNextLine());
        }
    }

    IEnumerator DelayBeforeNextLine()
    {
        yield return new WaitForSeconds(delayBeforeNextLine);
        NextLine();
    }

    public void NextLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
        }

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            typingCoroutine = StartCoroutine(Typing());
            PlayRandomSound();
        }
        else
        {
            ZeroText();
        }
    }

    private void PlayRandomSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        AudioClip[] sounds = { zgemoSound1, zgemoSound2, zgemoSound3 };
        AudioClip chosenSound = sounds[Random.Range(0, sounds.Length)];

        audioSource.clip = chosenSound;
        audioSource.Play();
    }
}
