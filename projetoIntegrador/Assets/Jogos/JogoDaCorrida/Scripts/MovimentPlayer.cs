using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovimentPlayer : MonoBehaviour
{
    public AudioSource jumpSound;
    public AudioSource crashSound;
    public float speedPoints;
    public float increaseSpeedPoints;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool jump;
    [SerializeField] private bool isGrounded = true;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    PlayerInputActions input;
    public float distance { private set; get; } 
    private GameControllerJCorrida gameController;
    private Progression progressionScript;
    public TMP_Text distanceText;
    public bool progressMovimentPScript;
    public Animator RonilcoAnimator;

    private Animator animator;
    private PauseJCorrida pauseJCorridaScript;

    public AudioSystem audioSystem;

    private void Awake()
    {
        input = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        gameController = FindObjectOfType<GameControllerJCorrida>();
        progressionScript = FindObjectOfType<Progression>();
        progressMovimentPScript = false;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        distance = 0f;
        animator = GetComponent<Animator>();
        pauseJCorridaScript = FindObjectOfType<PauseJCorrida>();
    }

    //private void OnEnable() // executado quando um objeto é ativado
    //{
    //    input.Enable();
    //}

    //private void OnDisable() //quando desativado
    //{
    //    input.Disable();
    //}

    public void DisableInput()
    {
        playerInput.enabled = false;
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (isGrounded && pauseJCorridaScript.gamePaused == false)
        {
            animator.SetBool("Jump", true);
            isGrounded = false;
            jump = false;
            rb.AddForce(Vector2.up * jumpForce);   
            jumpSound.Play();
            jumpSound.volume = VolumeControl.volumeEffect;
        }
    }
    void OnCollisionEnter2D(Collision2D collision) // verifica se ta no chao
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameController.gameOver = true;
            rb.velocity = Vector3.zero;
            crashSound.Play();
            crashSound.volume = VolumeControl.volumeEffect;
        }
    }
    void Update() //aumenta a velocidade da pontuacao quando atinge a meta
    {
        if (!gameController.gameOver && pauseJCorridaScript.gamePaused == false) //se nao for gameover e nao tiver pausado
        {
            distance += Time.deltaTime * speedPoints;
            RonilcoAnimator.speed = 1;
            distanceText.text = distance.ToString("F0");
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        }
        if (progressionScript.atingiuAMeta)
        {
            progressMovimentPScript = true;
            speedPoints = speedPoints + increaseSpeedPoints;
        }
        if(pauseJCorridaScript.gamePaused)// se tiver pausado 
        {
            RonilcoAnimator.speed = 0;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

        }

    }

}
