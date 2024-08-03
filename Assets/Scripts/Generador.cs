using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class Generador : MonoBehaviour
{
    [SerializeField] private SpriteShapeController ControlCarretera;
    [SerializeField, Range(3f, 100f)] private int tamaño = 50;
    [SerializeField, Range(1f, 50f)] private float xMultiplicador = 2f;
    [SerializeField, Range(1f, 50f)] private float yMultiplicador = 2f;
    [SerializeField, Range(0f, 1f)] private float curvas = 0.5f;
    [SerializeField] private float boton = 10f;
    [SerializeField] private float Ruido = 0.5f;
    private Vector3 PuntoAnterior;

    private void OnValidate() {
        ControlCarretera.spline.Clear();

        for (int i = 0; i < tamaño; i++) {
            PuntoAnterior = transform.position + new Vector3(i * xMultiplicador, Mathf.PerlinNoise(0, i * Ruido) * yMultiplicador);
            ControlCarretera.spline.InsertPointAt(i, PuntoAnterior);

            if (i != 0 && i != tamaño - 1) {
                ControlCarretera.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                ControlCarretera.spline.SetLeftTangent(i, Vector3.left * xMultiplicador * curvas);
                ControlCarretera.spline.SetRightTangent(i, Vector3.right * xMultiplicador * curvas);
            }
        }

        ControlCarretera.spline.InsertPointAt(tamaño, new Vector3(PuntoAnterior.x, transform.position.y - boton));
        ControlCarretera.spline.InsertPointAt(tamaño + 1, new Vector3(transform.position.x, transform.position.y - boton));
        
        // Establece la altura del sprite a 0.1
        for (int i = 0; i < ControlCarretera.spline.GetPointCount(); i++) {
            ControlCarretera.spline.SetHeight(i, 0.1f);
        }
    }
}
