using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PausaManager : MonoBehaviour
{
    private GameObject panelPausa;
    private bool pausado = false;
    private Canvas canvasPausa;

    void Awake()
    {
        CrearPanelPausa();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
                Reanudar();
            else
                Pausar();
        }
    }

    void Pausar()
    {
        pausado = true;
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        pausado = false;
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    void CrearPanelPausa()
    {
        // Asegurar que existe un EventSystem (sin él, los botones no responden)
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject esObj = new GameObject("EventSystem");
            esObj.AddComponent<EventSystem>();
            esObj.AddComponent<StandaloneInputModule>();
        }

        // Crear Canvas propio para la pausa (siempre encima de todo)
        GameObject canvasObj = new GameObject("CanvasPausa");
        canvasObj.transform.SetParent(this.transform);
        canvasPausa = canvasObj.AddComponent<Canvas>();
        canvasPausa.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasPausa.sortingOrder = 999;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280, 720);
        canvasObj.AddComponent<GraphicRaycaster>();

        // Panel oscuro que cubre toda la pantalla
        panelPausa = new GameObject("PanelPausa");
        panelPausa.transform.SetParent(canvasObj.transform, false);
        Image panelImg = panelPausa.AddComponent<Image>();
        panelImg.color = new Color(0f, 0f, 0f, 0.8f);
        panelImg.raycastTarget = true;
        RectTransform rtPanel = panelPausa.GetComponent<RectTransform>();
        rtPanel.anchorMin = Vector2.zero;
        rtPanel.anchorMax = Vector2.one;
        rtPanel.offsetMin = Vector2.zero;
        rtPanel.offsetMax = Vector2.zero;

        // Título PAUSA
        CrearTextoUI(panelPausa, "PAUSA", 50, Color.white, new Vector2(0, 140));

        // Botón Reanudar
        CrearBotonUI(panelPausa, "Reanudar", new Vector2(0, 40), delegate { Reanudar(); });
        // Botón Reiniciar
        CrearBotonUI(panelPausa, "Reiniciar", new Vector2(0, -40), delegate { ReiniciarNivel(); });
        // Botón Menú Principal
        CrearBotonUI(panelPausa, "Menu Principal", new Vector2(0, -120), delegate { IrAlMenu(); });

        panelPausa.SetActive(false);
    }

    void CrearTextoUI(GameObject parent, string contenido, int tamano, Color color, Vector2 pos)
    {
        GameObject obj = new GameObject("Txt_" + contenido);
        obj.transform.SetParent(parent.transform, false);
        Text txt = obj.AddComponent<Text>();
        txt.text = contenido;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = tamano;
        txt.fontStyle = FontStyle.Bold;
        txt.color = color;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        txt.verticalOverflow = VerticalWrapMode.Overflow;
        txt.raycastTarget = false;
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(500, 70);
    }

    void CrearBotonUI(GameObject parent, string texto, Vector2 pos,
                      UnityEngine.Events.UnityAction accion)
    {
        // Contenedor del botón
        GameObject btnObj = new GameObject("Btn_" + texto);
        btnObj.transform.SetParent(parent.transform, false);

        // Imagen de fondo (necesaria para que el Button detecte clics)
        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.2f, 0.5f, 0.8f, 1f);
        btnImg.raycastTarget = true;

        // Componente Button
        Button btn = btnObj.AddComponent<Button>();
        btn.targetGraphic = btnImg;
        ColorBlock colors = btn.colors;
        colors.normalColor = new Color(0.2f, 0.5f, 0.8f, 1f);
        colors.highlightedColor = new Color(0.3f, 0.6f, 0.9f, 1f);
        colors.pressedColor = new Color(0.1f, 0.3f, 0.6f, 1f);
        btn.colors = colors;

        // Posición y tamaño
        RectTransform rtBtn = btnObj.GetComponent<RectTransform>();
        rtBtn.anchorMin = new Vector2(0.5f, 0.5f);
        rtBtn.anchorMax = new Vector2(0.5f, 0.5f);
        rtBtn.pivot = new Vector2(0.5f, 0.5f);
        rtBtn.anchoredPosition = pos;
        rtBtn.sizeDelta = new Vector2(280, 60);

        // Texto dentro del botón
        GameObject txtObj = new GameObject("Text");
        txtObj.transform.SetParent(btnObj.transform, false);
        Text txt = txtObj.AddComponent<Text>();
        txt.text = texto;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = 28;
        txt.fontStyle = FontStyle.Bold;
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        txt.raycastTarget = false;
        RectTransform rtTxt = txtObj.GetComponent<RectTransform>();
        rtTxt.anchorMin = Vector2.zero;
        rtTxt.anchorMax = Vector2.one;
        rtTxt.offsetMin = Vector2.zero;
        rtTxt.offsetMax = Vector2.zero;

        // Asignar acción al botón
        btn.onClick.AddListener(accion);
    }
}
