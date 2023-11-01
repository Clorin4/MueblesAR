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
        // Obt�n el tama�o de la pantalla actual
        Resolution resolution = Screen.currentResolution;

        // Calcula el centro de la pantalla
        float centerX = resolution.width / 2f;
        float centerY = resolution.height / 2f;

        // Establece la posici�n del panel en el centro de la pantalla
        panel.anchoredPosition = new Vector2(centerX, centerY);
    }

    public void MostrarPanel()
    {
        // Mueve el panel a la posici�n inicial (puedes ajustar las coordenadas seg�n tus necesidades)
        iTween.MoveTo(gameObject, iTween.Hash("position", posicionInicial, "time", duracion, "easetype", tipoDeInterpolacion));
    }

    public void OcultarPanel()
    {
        // Mueve el panel a la posici�n final (puedes ajustar las coordenadas seg�n tus necesidades)
        iTween.MoveTo(gameObject, iTween.Hash("position", posicionFinal, "time", duracion, "easetype", tipoDeInterpolacion));
    }


}
