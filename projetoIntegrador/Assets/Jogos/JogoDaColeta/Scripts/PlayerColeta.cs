using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColeta : MonoBehaviour
{
    public float playerSpeed = 5f;
    public static int missingObjects = 5;
    public bool playerTrash;

    [SerializeField] Image lifeOn;
    [SerializeField] Image lifeOn2;
    [SerializeField] Image lifeOn3;
    [SerializeField] Image lifeOn4;
    [SerializeField] Image lifeOn5;

    public Sprite corazonBroken;

    SpriteRenderer playerFlip;
    private Animator pAnimator;
    private BoxCollider2D playerCollider;
    private int playerRunState;
    private int playerIdleState;
    private int idleLixoState;
    private int runLixoState;

    public AudioSource objectGotAudio;
    public AudioSource objectError;

    private void Awake()
    {
        playerFlip = GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();

        playerRunState = Animator.StringToHash("PlayerRun");
        playerIdleState = Animator.StringToHash("PlayerIdle");
        idleLixoState = Animator.StringToHash("IdleLixo");
        runLixoState = Animator.StringToHash("RunLixo");
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0);
        transform.Translate(Time.deltaTime * playerSpeed * movement);

        objectGotAudio.volume = VolumeControl.volumeEffect;
        objectError.volume = VolumeControl.volumeEffect;

        DropLife();

        if (horizontalInput > 0)
        {
            playerFlip.flipX = false;
            playerCollider.offset = new Vector2(3.88f, 2.34f);
        }
        else if (horizontalInput < 0)
        {
            playerFlip.flipX = true;
            playerCollider.offset = new Vector2(-3.88f, 2.34f);
        }

        if (horizontalInput == 0)
        {
            pAnimator.Play(playerIdleState);
            if (playerTrash)
                pAnimator.Play(idleLixoState);
        }
        else if (horizontalInput != 0)
        {
            pAnimator.Play(playerRunState);
            if (playerTrash)
                pAnimator.Play(runLixoState);
        }

        if (transform.position.x > 8f)
        {
            transform.position = new Vector2(8f, transform.position.y);
        }
        else if (transform.position.x < -8f)
        {
            transform.position = new Vector2(-8f, transform.position.y);
        }
    }

    private void DropLife()
    {
        if (missingObjects == 4)
        {
            lifeOn.sprite = corazonBroken;
        }

        if (missingObjects == 3)
        {
            lifeOn2.sprite = corazonBroken;
        }

        if (missingObjects == 2)
        {
            lifeOn3.sprite = corazonBroken;
        }

        if (missingObjects == 1)
        {
            lifeOn4.sprite = corazonBroken;
        }

        if (missingObjects == 0)
        {
            lifeOn5.sprite = corazonBroken;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish") && !playerTrash)
        {
            objectGotAudio.Play();

            // Destruir o objeto pego
            Destroy(other.gameObject);

            // Aumentar a pontuação do jogador
            FishsFalling.points++;
        }

        else if (other.CompareTag("Fish") && playerTrash)
        {
            objectError.Play();

            // Destruir o objeto pego
            Destroy(other.gameObject);
            // Aumentar os erros
            missingObjects--;
        }

        else if (other.CompareTag("lixo") && playerTrash)
        {
            objectGotAudio.Play();

            // Destruir o objeto pego
            Destroy(other.gameObject);

            // Aumentar os erros
            FishsFalling.points++;
        }

        else if (other.CompareTag("lixo") && !playerTrash)
        {
            objectError.Play();

            // Destruir o objeto pego
            Destroy(other.gameObject);

            // Aumentar os erros
            missingObjects--;
        }
    }
}
