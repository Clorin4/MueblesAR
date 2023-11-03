using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionPanel : MonoBehaviour
{
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;

    public float duracion = 1.0f;
    public iTween.EaseType tipoDeInterpolacion = iTween.EaseType.easeInBounce;
    public RectTransform panelres;

    private bool show;

    private void Start()
    {
        show = true;
        panelres = gameObject.GetComponentInParent<RectTransform>();
        float despDinam = panelres.position.x * 5;

        Vector3 pos = transform.localPosition;

        posicionInicial = new Vector3(pos.x + panelres.position.x, pos.y + panelres.position.y, 0);
        posicionFinal = new Vector3(posicionInicial.x + despDinam, posicionInicial.y, posicionInicial.z);

    }

    public void MostrarOcultarPanel()
    {
        if (show)
        {
            OcultarPanel();
            show = false;
        }
        else
        {
            MostrarPanel();
            show = true;
        }

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
