using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpcionesManager : MonoBehaviour
{
    private Slider sliderVolumen;
    private Slider sliderSensibilidad;
    private Toggle togglePantallaCompleta;

    void Start()
    {
        CrearUI();
    }

    void CrearUI()
    {
        // Canvas
        GameObject canvasObj = new GameObject("CanvasOpciones");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        canvasObj.AddComponent<GraphicRaycaster>();

        // Panel de fondo
        GameObject panel = new GameObject("PanelFondo");
        panel.transform.SetParent(canvasObj.transform, false);
        Image panelImg = panel.AddComponent<Image>();
        panelImg.color = new Color(0.1f, 0.1f, 0.2f, 1f);
        RectTransform rtPanel = panel.GetComponent<RectTransform>();
        rtPanel.anchorMin = Vector2.zero;
        rtPanel.anchorMax = Vector2.one;
        rtPanel.offsetMin = Vector2.zero;
        rtPanel.offsetMax = Vector2.zero;

        // Título
        CrearTexto(panel, "OPCIONES", 48, Color.white, new Vector2(0, 250));

        // --- Volumen ---
        CrearTexto(panel, "Volumen", 28, Color.white, new Vector2(0, 140));
        sliderVolumen = CrearSlider(panel, new Vector2(0, 100),
            PlayerPrefs.GetFloat("Volumen", 1f));
        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);

        // --- Sensibilidad ---
        CrearTexto(panel, "Velocidad Jugador", 28, Color.white, new Vector2(0, 20));
        sliderSensibilidad = CrearSlider(panel, new Vector2(0, -20),
            PlayerPrefs.GetFloat("Velocidad", 5f) / 10f);
        sliderSensibilidad.onValueChanged.AddListener(CambiarVelocidad);

        // --- Pantalla Completa ---
        togglePantallaCompleta = CrearToggle(panel, "Pantalla Completa",
            new Vector2(0, -100), Screen.fullScreen);
        togglePantallaCompleta.onValueChanged.AddListener(CambiarPantallaCompleta);

        // --- Botón Volver ---
        CrearBoton(panel, "Volver", new Vector2(0, -220), Volver);
    }

    void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("Volumen", valor);
    }

    void CambiarVelocidad(float valor)
    {
        float velocidad = valor * 10f;
        PlayerPrefs.SetFloat("Velocidad", velocidad);
    }

    void CambiarPantallaCompleta(bool activo)
    {
        Screen.fullScreen = activo;
    }

    void Volver()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("MenuPrincipal");
    }

    // --- Métodos auxiliares de UI ---

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
        rt.sizeDelta = new Vector2(400, 50);
    }

    Slider CrearSlider(GameObject parent, Vector2 pos, float valorInicial)
    {
        // Contenedor del slider
        GameObject sliderObj = new GameObject("Slider");
        sliderObj.transform.SetParent(parent.transform, false);
        RectTransform rtSlider = sliderObj.AddComponent<RectTransform>();
        rtSlider.anchorMin = new Vector2(0.5f, 0.5f);
        rtSlider.anchorMax = new Vector2(0.5f, 0.5f);
        rtSlider.pivot = new Vector2(0.5f, 0.5f);
        rtSlider.anchoredPosition = pos;
        rtSlider.sizeDelta = new Vector2(350, 30);

        // Background
        GameObject bgObj = new GameObject("Background");
        bgObj.transform.SetParent(sliderObj.transform, false);
        Image bgImg = bgObj.AddComponent<Image>();
        bgImg.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        RectTransform rtBg = bgObj.GetComponent<RectTransform>();
        rtBg.anchorMin = Vector2.zero;
        rtBg.anchorMax = Vector2.one;
        rtBg.offsetMin = Vector2.zero;
        rtBg.offsetMax = Vector2.zero;

        // Fill Area
        GameObject fillAreaObj = new GameObject("Fill Area");
        fillAreaObj.transform.SetParent(sliderObj.transform, false);
        RectTransform rtFillArea = fillAreaObj.AddComponent<RectTransform>();
        rtFillArea.anchorMin = new Vector2(0, 0.25f);
        rtFillArea.anchorMax = new Vector2(1, 0.75f);
        rtFillArea.offsetMin = new Vector2(5, 0);
        rtFillArea.offsetMax = new Vector2(-5, 0);

        GameObject fillObj = new GameObject("Fill");
        fillObj.transform.SetParent(fillAreaObj.transform, false);
        Image fillImg = fillObj.AddComponent<Image>();
        fillImg.color = new Color(0.2f, 0.6f, 1f, 1f);
        RectTransform rtFill = fillObj.GetComponent<RectTransform>();
        rtFill.anchorMin = Vector2.zero;
        rtFill.anchorMax = Vector2.one;
        rtFill.offsetMin = Vector2.zero;
        rtFill.offsetMax = Vector2.zero;

        // Handle Area
        GameObject handleAreaObj = new GameObject("Handle Slide Area");
        handleAreaObj.transform.SetParent(sliderObj.transform, false);
        RectTransform rtHandleArea = handleAreaObj.AddComponent<RectTransform>();
        rtHandleArea.anchorMin = Vector2.zero;
        rtHandleArea.anchorMax = Vector2.one;
        rtHandleArea.offsetMin = new Vector2(10, 0);
        rtHandleArea.offsetMax = new Vector2(-10, 0);

        GameObject handleObj = new GameObject("Handle");
        handleObj.transform.SetParent(handleAreaObj.transform, false);
        Image handleImg = handleObj.AddComponent<Image>();
        handleImg.color = Color.white;
        RectTransform rtHandle = handleObj.GetComponent<RectTransform>();
        rtHandle.sizeDelta = new Vector2(20, 0);
        rtHandle.anchorMin = new Vector2(0, 0);
        rtHandle.anchorMax = new Vector2(0, 1);

        // Configurar Slider
        Slider slider = sliderObj.AddComponent<Slider>();
        slider.fillRect = rtFill;
        slider.handleRect = rtHandle;
        slider.targetGraphic = handleImg;
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = valorInicial;

        return slider;
    }

    Toggle CrearToggle(GameObject parent, string texto, Vector2 pos, bool valorInicial)
    {
        GameObject toggleObj = new GameObject("Toggle_" + texto);
        toggleObj.transform.SetParent(parent.transform, false);
        RectTransform rtToggle = toggleObj.AddComponent<RectTransform>();
        rtToggle.anchorMin = new Vector2(0.5f, 0.5f);
        rtToggle.anchorMax = new Vector2(0.5f, 0.5f);
        rtToggle.pivot = new Vector2(0.5f, 0.5f);
        rtToggle.anchoredPosition = pos;
        rtToggle.sizeDelta = new Vector2(350, 40);

        // Background del checkbox
        GameObject bgObj = new GameObject("Background");
        bgObj.transform.SetParent(toggleObj.transform, false);
        Image bgImg = bgObj.AddComponent<Image>();
        bgImg.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        RectTransform rtBg = bgObj.GetComponent<RectTransform>();
        rtBg.anchorMin = new Vector2(0, 0.5f);
        rtBg.anchorMax = new Vector2(0, 0.5f);
        rtBg.pivot = new Vector2(0, 0.5f);
        rtBg.anchoredPosition = new Vector2(0, 0);
        rtBg.sizeDelta = new Vector2(30, 30);

        // Checkmark
        GameObject checkObj = new GameObject("Checkmark");
        checkObj.transform.SetParent(bgObj.transform, false);
        Image checkImg = checkObj.AddComponent<Image>();
        checkImg.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        RectTransform rtCheck = checkObj.GetComponent<RectTransform>();
        rtCheck.anchorMin = new Vector2(0.15f, 0.15f);
        rtCheck.anchorMax = new Vector2(0.85f, 0.85f);
        rtCheck.offsetMin = Vector2.zero;
        rtCheck.offsetMax = Vector2.zero;

        // Label
        GameObject labelObj = new GameObject("Label");
        labelObj.transform.SetParent(toggleObj.transform, false);
        Text label = labelObj.AddComponent<Text>();
        label.text = texto;
        label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        label.fontSize = 24;
        label.color = Color.white;
        label.alignment = TextAnchor.MiddleLeft;
        RectTransform rtLabel = labelObj.GetComponent<RectTransform>();
        rtLabel.anchorMin = new Vector2(0, 0);
        rtLabel.anchorMax = new Vector2(1, 1);
        rtLabel.offsetMin = new Vector2(40, 0);
        rtLabel.offsetMax = Vector2.zero;

        // Toggle component
        Toggle toggle = toggleObj.AddComponent<Toggle>();
        toggle.targetGraphic = bgImg;
        toggle.graphic = checkImg;
        toggle.isOn = valorInicial;

        return toggle;
    }

    void CrearBoton(GameObject parent, string texto, Vector2 pos,
                    UnityEngine.Events.UnityAction accion)
    {
        GameObject btnObj = new GameObject("Btn_" + texto);
        btnObj.transform.SetParent(parent.transform, false);

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

        btn.onClick.AddListener(accion);
    }
}
