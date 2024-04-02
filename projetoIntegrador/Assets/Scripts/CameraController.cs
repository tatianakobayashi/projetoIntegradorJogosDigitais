using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referência ao objeto do jogador
    public float cameraSpeed = 5f; // Velocidade de suavização da câmera

    public float maxXLimit; //= 39.55f; // Limite máximo do mapa em X
    public float minXLimit; //= -25.55f; // Limite mínimo do mapa em X
    public float maxYLimit; //= 38.35f; // Limite máximo do mapa em Y
    public float minYLimit;//= -2.85f; // Limite mínimo do mapa em Y
    public Vector3 offset; // armazenar a diferença entre a posição da câmera e a posição do jogador no início do jogo.

    void Update()
    {
        CamFollowingPlayer(maxXLimit, minXLimit, maxYLimit, minYLimit);
    }
    public void CamFollowingPlayer(float maxXLimit, float minXLimit, float maxYLimit, float minYLimit)
    {
        // Calcula a posição desejada da câmera com base na posição do jogador e o offset
        // Calcula a posição desejada da câmera somando a posição atual do jogador (player.position) com o deslocamento (offset) que foi calculado anteriormente
        Vector3 desiredPosition = player.position + offset;

        // Limita a posição desejada nos limites definidos
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minXLimit, maxXLimit);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minYLimit, maxYLimit);
        // Debug.Log(maxXLimit + "\n" + minXLimit + "\n" + maxYLimit + "\n" +minYLimit);

        // Suaviza o movimento da câmera usando Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;
    }
}
