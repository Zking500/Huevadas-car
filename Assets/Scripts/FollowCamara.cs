using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCamara : MonoBehaviour
{
    private void Start()
    {
        // Encuentra el objeto con la etiqueta "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Asegúrate de que el objeto con la etiqueta "Player" existe
        if (player != null)
        {
            // Encuentra la CinemachineVirtualCamera en el mismo objeto
            CinemachineVirtualCamera vCam = GetComponent<CinemachineVirtualCamera>();

            // Asegúrate de que la CinemachineVirtualCamera existe
            if (vCam != null)
            {
                // Asigna el objeto "Player" como el objetivo de seguimiento
                vCam.Follow = player.transform;
            }
            else
            {
                Debug.LogError("No se encontró la CinemachineVirtualCamera en el objeto.");
            }
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con la etiqueta 'Player'.");
        }
    }
}
