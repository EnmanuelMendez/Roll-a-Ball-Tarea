using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausaManager : MonoBehaviour
{
    public GameObject panelPausa;
    private bool pausado = false;

    void Start()
    {
        // Si no hay panel asignado, crear uno por código
        if (panelPausa == null)
            CrearPanelPausa();
        else
            panelPausa.SetActive(false);
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
        // Buscar o crear Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject canvasObj;
        if (canvas == null)
        {
            canvasObj = new GameObject("CanvasPausa");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100; // Siempre encima
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        else
        {
            canvasObj = canvas.gameObject;
        }

        // Panel oscuro de fondo
        panelPausa = new GameObject("PanelPausa");
        panelPausa.transform.SetParent(canvasObj.transform, false);
        Image panelImg = panelPausa.AddComponent<Image>();
        panelImg.color = new Color(0f, 0f, 0f, 0.85f);
        RectTransform rtPanel = panelPausa.GetComponent<RectTransform>();
        rtPanel.anchorMin = Vector2.zero;
        rtPanel.anchorMax = Vector2.one;
        rtPanel.offsetMin = Vector2.zero;
        rtPanel.offsetMax = Vector2.zero;

        // Título "PAUSA"
        CrearTexto(panelPausa, "PAUSA", 52, Color.white, new Vector2(0, 120));

        // Botones
        CrearBoton(panelPausa, "Reanudar", new Vector2(0, 30), Reanudar);
        CrearBoton(panelPausa, "Reiniciar", new Vector2(0, -40), ReiniciarNivel);
        CrearBoton(panelPausa, "Menu Principal", new Vector2(0, -110), IrAlMenu);

        panelPausa.SetActive(false);
    }

    void CrearTexto(GameObject parent, string contenido, int fontSize, Color color, Vector2 pos)
    {
        GameObject txtObj = new GameObject("Texto_" + contenido);
        txtObj.transform.SetParent(parent.transform, false);
        Text txt = txtObj.AddComponent<Text>();
        txt.text = contenido;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = fontSize;
        txt.color = color;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        txt.verticalOverflow = VerticalWrapMode.Overflow;
        RectTransform rt = txtObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(400, 60);
    }

    void CrearBoton(GameObject parent, string texto, Vector2 pos,
                    UnityEngine.Events.UnityAction accion)
    {
        GameObject btnObj = new GameObject("Btn_" + texto);
        btnObj.transform.SetParent(parent.transform, false);

        // Fondo del botón
        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.15f, 0.45f, 0.75f, 1f);

        Button btn = btnObj.AddComponent<Button>();
        ColorBlock cb = btn.colors;
        cb.highlightedColor = new Color(0.2f, 0.55f, 0.9f, 1f);
        cb.pressedColor = new Color(0.1f, 0.35f, 0.6f, 1f);
        btn.colors = cb;

        RectTransform rtBtn = btnObj.GetComponent<RectTransform>();
        rtBtn.anchorMin = new Vector2(0.5f, 0.5f);
        rtBtn.anchorMax = new Vector2(0.5f, 0.5f);
        rtBtn.pivot = new Vector2(0.5f, 0.5f);
        rtBtn.anchoredPosition = pos;
        rtBtn.sizeDelta = new Vector2(250, 55);

        // Texto del botón
        GameObject txtObj = new GameObject("Text");
        txtObj.transform.SetParent(btnObj.transform, false);
        Text txt = txtObj.AddComponent<Text>();
        txt.text = texto;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = 26;
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        RectTransform rtTxt = txtObj.GetComponent<RectTransform>();
        rtTxt.anchorMin = Vector2.zero;
        rtTxt.anchorMax = Vector2.one;
        rtTxt.offsetMin = Vector2.zero;
        rtTxt.offsetMax = Vector2.zero;

        // Asignar acción
        btn.onClick.AddListener(accion);
    }
}
