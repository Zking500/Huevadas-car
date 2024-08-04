using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotMoving : MonoBehaviour
{
    WheelJoint2D[] union;
    JointMotor2D YantaAdelante;
    JointMotor2D YantaAtras;
    public float Angulo = 0;
    public float Desaceleracion = 40f;
    public float Aceleracion = 50f;
    public float VelocidadMAX = -800f;
    public float VelocidadMAXReversa = 600f;
    public float tamañoYanta = 0.283f;
    public bool Suelo = false;
    public LayerMask TagSuelo;
    public Transform yanta;

    private bool Decaveza = false;

    void Start()
    {
        union = gameObject.GetComponents<WheelJoint2D>();
        YantaAdelante = union[0].motor;
        YantaAtras = union[1].motor;
    }

    void FixedUpdate()
    {
        Suelo = Physics2D.OverlapCircle(yanta.transform.position, tamañoYanta, TagSuelo);
        Angulo = transform.localEulerAngles.z;

        if (Angulo > 180) Angulo -= 180;

        if (Suelo && !Decaveza)
        {
            YantaAtras.motorSpeed = Mathf.Clamp(YantaAtras.motorSpeed - Aceleracion * Time.deltaTime, VelocidadMAX, VelocidadMAXReversa);
        }
        else
        {
            // Si no está en el suelo o está de cabeza, desacelera lentamente
            YantaAtras.motorSpeed = Mathf.MoveTowards(YantaAtras.motorSpeed, 0, Desaceleracion * Time.deltaTime);
        }

        YantaAdelante = YantaAtras;
        union[0].motor = YantaAtras;
        union[1].motor = YantaAdelante;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(yanta.transform.position, tamañoYanta);
    }

}
