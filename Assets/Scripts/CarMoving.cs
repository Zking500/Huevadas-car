using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarMoving : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    WheelJoint2D[] union;
    JointMotor2D YantaAdelante;
    JointMotor2D YantaAtras;
    public GameObject CamaraSeguimiento;

    private float Desaceleracion = -400f;
    private float Gravedad = 9.8f;
    public float Angulo = 0;
    public float Aceleracion = 500f;
    public float VelocidadMAX = -800f;
    public float VelocidadMAXReversa = 600f;
    public float tamañoYanta;
    public bool Suelo = false;
    public Button botonAcelerar;
    public Button botonFrenar;
    private Quaternion rotacionInicial;
    public LayerMask TagSuelo;
    public Transform yanta;

    private bool acelerar = false;
    private bool frenar = false;

    void Start()
    {
        if (CamaraSeguimiento != null)
        {
            rotacionInicial = CamaraSeguimiento.transform.rotation;
        }
        else
        {
            Debug.LogWarning("CamaraSeguimiento no está asignado en el Inspector.");
        }

        union = gameObject.GetComponents<WheelJoint2D>();
        YantaAdelante = union[0].motor;
        YantaAtras = union[1].motor;
        
        // Configura los botones para manejar eventos de mantener presionado
        ConfigurarBotones(botonAcelerar, OnAcelerarButtonDown, OnAcelerarButtonUp);
        ConfigurarBotones(botonFrenar, OnFrenarButtonDown, OnFrenarButtonUp);
    }

    void FixedUpdate()
    {
        Suelo = Physics2D.OverlapCircle(yanta.transform.position, tamañoYanta, TagSuelo);
        Angulo = transform.localEulerAngles.z;

        if (Angulo > 180) Angulo -= 180;
        if (Suelo)
        {
            if (acelerar)
            {
                YantaAtras.motorSpeed = Mathf.Clamp(YantaAtras.motorSpeed - (Aceleracion - Gravedad * Mathf.PI * (Angulo / 180) * 80) * Time.deltaTime, VelocidadMAX, VelocidadMAXReversa);
            }
            else if (frenar)
            {
                YantaAtras.motorSpeed = Mathf.Clamp(YantaAtras.motorSpeed - (Desaceleracion - Gravedad * Mathf.PI * (Angulo / 180) * 80) * Time.deltaTime, VelocidadMAX, VelocidadMAXReversa);
            }
        }

        YantaAdelante = YantaAtras;

        union[0].motor = YantaAtras;
        union[1].motor = YantaAdelante;
    }

    void LateUpdate()
    {
        if (CamaraSeguimiento != null)
        {
            // Sincronizar solo el eje Z de la rotación
            CamaraSeguimiento.transform.eulerAngles = new Vector3(CamaraSeguimiento.transform.eulerAngles.x, CamaraSeguimiento.transform.eulerAngles.y, rotacionInicial.eulerAngles.z);
        }
    }

    public void OnAcelerarButtonDown()
    {
        acelerar = true;
    }

    public void OnAcelerarButtonUp()
    {
        acelerar = false;
    }

    public void OnFrenarButtonDown()
    {
        frenar = true;
    }

    public void OnFrenarButtonUp()
    {
        frenar = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Maneja el evento de mantener presionado
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Maneja el evento de soltar el botón
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(yanta.transform.position, tamañoYanta);
    }

    private void ConfigurarBotones(Button boton, UnityEngine.Events.UnityAction accionDown, UnityEngine.Events.UnityAction accionUp)
    {
        boton.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = boton.GetComponent<EventTrigger>();

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { accionDown(); });
        trigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => { accionUp(); });
        trigger.triggers.Add(entryUp);
    }
}
