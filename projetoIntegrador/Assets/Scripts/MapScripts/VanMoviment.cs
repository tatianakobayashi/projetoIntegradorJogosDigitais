using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class VanMoviment : MonoBehaviour
{
    public CustomImput input = null; // Vari�vel para receber o input do jogador
    public float moveSpeed;
    public Vector2 moveVector = Vector2.zero;
    public Sprite spriteVanUpAndDown;
    public Sprite spriteVanRight;
    public Sprite spriteVanLeft;

    public GameObject wheelLeft;
    public GameObject wheelRight;
    public Transform posInitial;
    public bool isMoving = false;


    public List<GameObject> arrowPrefabs = new List<GameObject>();
    private List<GameObject> arrowObjects = new List<GameObject>();
    private GameObject arrowObject;
    private Vector3 offset;

    private void Awake()
    {
        input = new CustomImput();
    }

    private void Start()
    {
        transform.position = posInitial.position;
        StartCoroutine(SpawnArrowRoutine());
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Moviment.performed += SetMovement;
        input.Player.Moviment.canceled += SetMovement;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Moviment.performed -= SetMovement;
        input.Player.Moviment.canceled -= SetMovement;
    }

    public void SetMovement(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    private void StopMoving(InputAction.CallbackContext context)
    {
        isMoving = false;
    }
    void Update(){
      if (isMoving) 
        {
            float horizontalInput = moveVector.x;
            float verticalInput = moveVector.y;
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * moveSpeed * Time.deltaTime;

            transform.Translate(movement);

            
            if (movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            UpdateArrowPosition();
        }
}
void UpdateArrowPosition()
{
    if (arrowObject != null)
    {
        if (GetComponent<SpriteRenderer>().sprite.name == "Vanteca_Sprite_Cima")
        {
            offset = new Vector3(0f, 1.1f, 0f);
        }
        else if (GetComponent<SpriteRenderer>().sprite.name == "Vanteca_Sprite_LadoEsquerdo")
        {
            offset = new Vector3(-0.5f, 1.1f, 0f);
        }
        else if (GetComponent<SpriteRenderer>().sprite.name == "Vanteca_Sprite_LadoDireito")
        {
            offset = new Vector3(0.5f, 1.1f, 0f);
        }

        arrowObject.transform.position = gameObject.transform.position + offset;
    }
}

    IEnumerator SpawnArrowRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);
            SpawnRandomArrow();
            StartCoroutine(DestroyArrowRoutine());
        }
    }

    IEnumerator DestroyArrowRoutine()
    {
        yield return new WaitForSeconds(5f);
        if (arrowObjects.Count > 0)
        {
            foreach (GameObject arrowObject in arrowObjects)
            {
                Destroy(arrowObject);
            }
            arrowObjects.Clear();
        }
    }

    private void SpawnRandomArrow()
    {
        if (arrowPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, arrowPrefabs.Count);
            GameObject selectedArrowPrefab = arrowPrefabs[randomIndex];
            arrowObject = Instantiate(selectedArrowPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);

            //arrowObject.transform.parent = transform;

            arrowObjects.Add(arrowObject);
        }
    }

    public void OpenInstructionsMap(bool value)
    {
        isMoving = value;
    }
}