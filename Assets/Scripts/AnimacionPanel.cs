using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionPanel : MonoBehaviour
{
    public Vector2 posicionInicial;
    public Vector2 posicionFinal;
    public float duracion = 1.0f;
    public iTween.EaseType tipoDeInterpolacion = iTween.EaseType.easeInBounce;
    public RectTransform panel;


    private void Start()
    {
        /*posicionInicial = new Vector2(Screen.width, Screen.height);
        Resolution resolution = Screen.currentResolution;
        float centerX = resolution.width / 2f;
        float centerY = resolution.height / 2f;
        centerX = posicionInicial.x;
        centerY = posicionInicial.y;*/
    }

    public void CentrarPanelEnResolucion()
    {
        // Obtén el tamaño de la pantalla actual
        Resolution resolution = Screen.currentResolution;

        // Calcula el centro de la pantalla
        float centerX = resolution.width / 2f;
        float centerY = resolution.height / 2f;

        // Establece la posición del panel en el centro de la pantalla
        panel.anchoredPosition = new Vector2(centerX, centerY);
    }

    public void MostrarPanel()
    {
        // Mueve el panel a la posición inicial (puedes ajustar las coordenadas según tus necesidades)
        iTween.MoveTo(gameObject, iTween.Hash("position", posicionInicial, "time", duracion, "easetype", tipoDeInterpolacion));
    }

    public void OcultarPanel()
    {
        // Mueve el panel a la posición final (puedes ajustar las coordenadas según tus necesidades)
        iTween.MoveTo(gameObject, iTween.Hash("position", posicionFinal, "time", duracion, "easetype", tipoDeInterpolacion));
    }


}
