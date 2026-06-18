#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NivelGenerador : EditorWindow
{
    [MenuItem("Tools/Generar Niveles")]
    public static void ShowWindow()
    {
        GetWindow<NivelGenerador>("Generador de Niveles");
    }

    void OnGUI()
    {
        GUILayout.Label("Generador de Niveles Roll-a-Ball", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (GUILayout.Button("Generar Nivel 2 (Nivel1) - Plataformas"))
            GenerarNivel1();

        if (GUILayout.Button("Generar Nivel 3 (Nivel2) - Laberinto"))
            GenerarNivel2();

        if (GUILayout.Button("Generar Nivel 4 (Nivel3) - Obstáculos"))
            GenerarNivel3();

        if (GUILayout.Button("Generar Nivel 5 (Nivel4) - Combinación"))
            GenerarNivel4();

        GUILayout.Space(10);
        if (GUILayout.Button("Configurar Escena Opciones"))
            ConfigurarEscenaOpciones();

        GUILayout.Space(20);
        GUILayout.Label("IMPORTANTE: Después de generar cada nivel,", EditorStyles.miniLabel);
        GUILayout.Label("asigna manualmente las referencias del Canvas", EditorStyles.miniLabel);
        GUILayout.Label("al GameManager y al Jugador en el Inspector.", EditorStyles.miniLabel);
    }

    // ============== NIVEL 2 (escena Nivel1) - Plataformas y Rampas ==============
    void GenerarNivel1()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Suelo grande
        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        suelo.name = "Suelo";
        suelo.transform.position = new Vector3(0, -0.25f, 0);
        suelo.transform.localScale = new Vector3(30, 0.5f, 30);

        // Paredes bordes
        CrearParedesBorde(15f, 1f);

        // Plataformas con rampas
        CrearPlataformaConRampa(new Vector3(-5, 0.75f, -5), 0.75f);
        CrearPlataformaConRampa(new Vector3(5, 1.5f, -5), 1.5f);
        CrearPlataformaConRampa(new Vector3(-5, 2.0f, 5), 2.0f);
        CrearPlataformaConRampa(new Vector3(5, 2.5f, 5), 2.5f);

        // Coleccionables en suelo (8)
        CrearColeccionable(new Vector3(-3, 0.5f, -3));
        CrearColeccionable(new Vector3(3, 0.5f, -3));
        CrearColeccionable(new Vector3(-3, 0.5f, 3));
        CrearColeccionable(new Vector3(3, 0.5f, 3));
        CrearColeccionable(new Vector3(0, 0.5f, -6));
        CrearColeccionable(new Vector3(0, 0.5f, 6));
        CrearColeccionable(new Vector3(-7, 0.5f, 0));
        CrearColeccionable(new Vector3(7, 0.5f, 0));

        // Coleccionables en plataformas (2 por plataforma = 8)
        CrearColeccionable(new Vector3(-5.5f, 1.5f, -5));
        CrearColeccionable(new Vector3(-4.5f, 1.5f, -5));
        CrearColeccionable(new Vector3(5.5f, 2.25f, -5));
        CrearColeccionable(new Vector3(4.5f, 2.25f, -5));
        CrearColeccionable(new Vector3(-5.5f, 2.75f, 5));
        CrearColeccionable(new Vector3(-4.5f, 2.75f, 5));
        CrearColeccionable(new Vector3(5.5f, 3.25f, 5));
        CrearColeccionable(new Vector3(4.5f, 3.25f, 5));

        // Jugador
        CrearJugador(new Vector3(0, 0.5f, 0), 16);

        // GameManager
        CrearGameManager(50f);

        // Canvas con UI
        CrearCanvasUI();

        // PausaManager
        CrearPausaManager();

        // Cámara
        ConfigurarCamara();

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Nivel1.unity.unity");
        Debug.Log("Nivel 2 (Nivel1) generado con éxito.");
    }

    // ============== NIVEL 3 (escena Nivel2) - Laberinto ==============
    void GenerarNivel2()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Suelo
        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        suelo.name = "Suelo";
        suelo.transform.position = new Vector3(0, -0.25f, 0);
        suelo.transform.localScale = new Vector3(30, 0.5f, 30);

        // Paredes borde
        CrearParedesBorde(15f, 1.5f);

        // Laberinto
        GameObject laberinto = new GameObject("ParedesLaberinto");

        // Paredes del laberinto (forma de S con callejones)
        CrearParedLaberinto(laberinto, new Vector3(0, 0.5f, -5), new Vector3(10, 1, 0.3f));
        CrearParedLaberinto(laberinto, new Vector3(-5, 0.5f, -2.5f), new Vector3(0.3f, 1, 5));
        CrearParedLaberinto(laberinto, new Vector3(0, 0.5f, 0), new Vector3(10, 1, 0.3f));
        CrearParedLaberinto(laberinto, new Vector3(5, 0.5f, 2.5f), new Vector3(0.3f, 1, 5));
        CrearParedLaberinto(laberinto, new Vector3(0, 0.5f, 5), new Vector3(10, 1, 0.3f));
        CrearParedLaberinto(laberinto, new Vector3(-7, 0.5f, -7), new Vector3(0.3f, 1, 4));
        CrearParedLaberinto(laberinto, new Vector3(7, 0.5f, -7), new Vector3(0.3f, 1, 4));
        CrearParedLaberinto(laberinto, new Vector3(-7, 0.5f, 7), new Vector3(0.3f, 1, 4));
        CrearParedLaberinto(laberinto, new Vector3(7, 0.5f, 3), new Vector3(0.3f, 1, 4));
        CrearParedLaberinto(laberinto, new Vector3(3, 0.5f, -8), new Vector3(4, 1, 0.3f));

        // Coleccionables: 4 en camino principal
        CrearColeccionable(new Vector3(-3, 0.5f, -7));
        CrearColeccionable(new Vector3(3, 0.5f, -3));
        CrearColeccionable(new Vector3(-3, 0.5f, 2.5f));
        CrearColeccionable(new Vector3(3, 0.5f, 7));

        // Coleccionables: 8 en callejones sin salida
        CrearColeccionable(new Vector3(-7, 0.5f, -9));
        CrearColeccionable(new Vector3(7, 0.5f, -9));
        CrearColeccionable(new Vector3(-9, 0.5f, -3));
        CrearColeccionable(new Vector3(9, 0.5f, -3));
        CrearColeccionable(new Vector3(-7, 0.5f, 9));
        CrearColeccionable(new Vector3(7, 0.5f, 1));
        CrearColeccionable(new Vector3(-9, 0.5f, 5));
        CrearColeccionable(new Vector3(9, 0.5f, 7));

        // Jugador
        CrearJugador(new Vector3(-10, 0.5f, -10), 12);

        // GameManager
        CrearGameManager(60f);

        // Canvas
        CrearCanvasUI();

        // PausaManager
        CrearPausaManager();

        // Cámara
        ConfigurarCamara();

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Nivel2.unity.unity");
        Debug.Log("Nivel 3 (Nivel2) generado con éxito.");
    }

    // ============== NIVEL 4 (escena Nivel3) - Obstáculos Móviles ==============
    void GenerarNivel3()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Suelo
        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        suelo.name = "Suelo";
        suelo.transform.position = new Vector3(0, -0.25f, 0);
        suelo.transform.localScale = new Vector3(30, 0.5f, 30);

        // Paredes borde
        CrearParedesBorde(15f, 1f);

        // Obstáculos móviles
        CrearObstaculo("Obstaculo1", new Vector3(-8, 0.25f, 0), new Vector3(8, 0.25f, 0), 2f);
        CrearObstaculo("Obstaculo2", new Vector3(0, 0.25f, -8), new Vector3(0, 0.25f, 8), 2.5f);
        CrearObstaculo("Obstaculo3", new Vector3(-6, 0.25f, -6), new Vector3(6, 0.25f, 6), 1.8f);
        CrearObstaculo("Obstaculo4", new Vector3(-8, 0.25f, 5), new Vector3(8, 0.25f, 5), 3f);

        // Coleccionables: 4 en zonas seguras
        CrearColeccionable(new Vector3(-12, 0.5f, -12));
        CrearColeccionable(new Vector3(12, 0.5f, -12));
        CrearColeccionable(new Vector3(-12, 0.5f, 12));
        CrearColeccionable(new Vector3(12, 0.5f, 12));

        // Coleccionables: 6 entre obstáculos
        CrearColeccionable(new Vector3(0, 0.5f, 0));
        CrearColeccionable(new Vector3(4, 0.5f, -4));
        CrearColeccionable(new Vector3(-4, 0.5f, 4));
        CrearColeccionable(new Vector3(6, 0.5f, 2));
        CrearColeccionable(new Vector3(-6, 0.5f, -2));
        CrearColeccionable(new Vector3(0, 0.5f, 8));

        // Jugador
        CrearJugador(new Vector3(-12, 0.5f, 0), 10);

        // GameManager
        CrearGameManager(45f);

        // Canvas
        CrearCanvasUI();

        // PausaManager
        CrearPausaManager();

        // Cámara
        ConfigurarCamara();

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Nivel3.unity.unity");
        Debug.Log("Nivel 4 (Nivel3) generado con éxito.");
    }

    // ============== NIVEL 5 (escena Nivel4) - Combinación Final ==============
    void GenerarNivel4()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Suelo más pequeño (laberinto 8x8 aprox)
        GameObject suelo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        suelo.name = "Suelo";
        suelo.transform.position = new Vector3(0, -0.25f, 0);
        suelo.transform.localScale = new Vector3(20, 0.5f, 20);

        // Paredes borde
        CrearParedesBorde(10f, 1.5f);

        // Laberinto compacto
        GameObject laberinto = new GameObject("ParedesLaberinto");
        CrearParedLaberinto(laberinto, new Vector3(0, 0.5f, -4), new Vector3(8, 1, 0.3f));
        CrearParedLaberinto(laberinto, new Vector3(-4, 0.5f, -1), new Vector3(0.3f, 1, 6));
        CrearParedLaberinto(laberinto, new Vector3(0, 0.5f, 2), new Vector3(8, 1, 0.3f));
        CrearParedLaberinto(laberinto, new Vector3(4, 0.5f, 5), new Vector3(0.3f, 1, 6));
        CrearParedLaberinto(laberinto, new Vector3(-2, 0.5f, 6), new Vector3(6, 1, 0.3f));
        CrearParedLaberinto(laberinto, new Vector3(2, 0.5f, -7), new Vector3(4, 1, 0.3f));
        CrearParedLaberinto(laberinto, new Vector3(-6, 0.5f, 4), new Vector3(0.3f, 1, 4));
        CrearParedLaberinto(laberinto, new Vector3(6, 0.5f, -2), new Vector3(0.3f, 1, 4));

        // Obstáculos móviles dentro del laberinto
        CrearObstaculo("Obstaculo1", new Vector3(-3, 0.25f, -2), new Vector3(3, 0.25f, -2), 2f);
        CrearObstaculo("Obstaculo2", new Vector3(0, 0.25f, 3), new Vector3(0, 0.25f, 7), 2.5f);
        CrearObstaculo("Obstaculo3", new Vector3(-5, 0.25f, 0), new Vector3(5, 0.25f, 0), 1.5f);

        // Coleccionables (12) distribuidos por el laberinto
        CrearColeccionable(new Vector3(-7, 0.5f, -7));
        CrearColeccionable(new Vector3(7, 0.5f, -7));
        CrearColeccionable(new Vector3(-7, 0.5f, 7));
        CrearColeccionable(new Vector3(7, 0.5f, 7));
        CrearColeccionable(new Vector3(0, 0.5f, -6));
        CrearColeccionable(new Vector3(0, 0.5f, 4));
        CrearColeccionable(new Vector3(-5, 0.5f, -3));
        CrearColeccionable(new Vector3(5, 0.5f, -3));
        CrearColeccionable(new Vector3(-3, 0.5f, 5));
        CrearColeccionable(new Vector3(3, 0.5f, 8));
        CrearColeccionable(new Vector3(-8, 0.5f, 0));
        CrearColeccionable(new Vector3(8, 0.5f, 0));

        // Jugador
        CrearJugador(new Vector3(-8, 0.5f, -8), 12);

        // GameManager
        CrearGameManager(40f);

        // Canvas
        CrearCanvasUI();

        // PausaManager
        CrearPausaManager();

        // Cámara
        ConfigurarCamara();

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Nivel4.unity.unity");
        Debug.Log("Nivel 5 (Nivel4) generado con éxito.");
    }

    // ============== MÉTODOS AUXILIARES ==============

    void CrearParedesBorde(float halfSize, float altura)
    {
        GameObject paredes = new GameObject("Paredes");

        GameObject paredN = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paredN.name = "ParedNorte";
        paredN.transform.position = new Vector3(0, altura / 2f, halfSize);
        paredN.transform.localScale = new Vector3(halfSize * 2, altura, 0.5f);
        paredN.transform.parent = paredes.transform;

        GameObject paredS = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paredS.name = "ParedSur";
        paredS.transform.position = new Vector3(0, altura / 2f, -halfSize);
        paredS.transform.localScale = new Vector3(halfSize * 2, altura, 0.5f);
        paredS.transform.parent = paredes.transform;

        GameObject paredE = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paredE.name = "ParedEste";
        paredE.transform.position = new Vector3(halfSize, altura / 2f, 0);
        paredE.transform.localScale = new Vector3(0.5f, altura, halfSize * 2);
        paredE.transform.parent = paredes.transform;

        GameObject paredO = GameObject.CreatePrimitive(PrimitiveType.Cube);
        paredO.name = "ParedOeste";
        paredO.transform.position = new Vector3(-halfSize, altura / 2f, 0);
        paredO.transform.localScale = new Vector3(0.5f, altura, halfSize * 2);
        paredO.transform.parent = paredes.transform;
    }

    void CrearPlataformaConRampa(Vector3 posPlataforma, float altura)
    {
        // Plataforma
        GameObject plataforma = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plataforma.name = "Plataforma_" + altura;
        plataforma.transform.position = posPlataforma;
        plataforma.transform.localScale = new Vector3(4, 0.5f, 4);

        // Rampa - más larga para ángulo más suave
        GameObject rampa = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rampa.name = "Rampa_" + altura;
        // Largo de rampa proporcional a la altura (más larga = más fácil de subir)
        float largoRampa = altura * 5f; // Rampa 5 veces más larga que alta
        if (largoRampa < 4f) largoRampa = 4f;
        float angulo = Mathf.Atan2(altura, largoRampa) * Mathf.Rad2Deg;
        // Posicionar rampa: centro entre el suelo y la plataforma
        float rampaCentroZ = posPlataforma.z - (largoRampa / 2f) - 1f;
        float rampaCentroY = altura / 2f;
        rampa.transform.position = new Vector3(posPlataforma.x, rampaCentroY, rampaCentroZ);
        rampa.transform.localScale = new Vector3(2.5f, 0.15f, largoRampa);
        rampa.transform.rotation = Quaternion.Euler(angulo, 0, 0);

        // Añadir fricción alta para que la bola no resbale
        rampa.AddComponent<ConfigurarRampa>();
    }

    void CrearColeccionable(Vector3 posicion)
    {
        GameObject col = GameObject.CreatePrimitive(PrimitiveType.Cube);
        col.name = "Coleccionable";
        col.transform.position = posicion;
        col.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        col.transform.rotation = Quaternion.Euler(45, 45, 45);
        col.tag = "Coleccionable";

        // Configurar como trigger
        Collider collider = col.GetComponent<Collider>();
        collider.isTrigger = true;

        // Agregar Rigidbody cinemático
        Rigidbody rb = col.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        // Agregar script Rotador
        col.AddComponent<Rotador>();
    }

    void CrearJugador(Vector3 posicion, int totalColeccionables)
    {
        GameObject jugador = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        jugador.name = "Jugador";
        jugador.transform.position = posicion;

        Rigidbody rb = jugador.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        JugadorController jc = jugador.AddComponent<JugadorController>();
        jc.velocidad = 5f;
        jc.totalColeccionables = totalColeccionables;
    }

    void CrearGameManager(float tiempo)
    {
        GameObject gm = new GameObject("GameManager");
        GameManager manager = gm.AddComponent<GameManager>();
        manager.tiempoInicial = tiempo;
    }

    void CrearObstaculo(string nombre, Vector3 puntoA, Vector3 puntoB, float velocidad)
    {
        GameObject obs = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obs.name = nombre;
        obs.transform.position = puntoA;
        obs.transform.localScale = new Vector3(3, 0.5f, 0.5f);

        // Color rojo
        Renderer rend = obs.GetComponent<Renderer>();
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Color.red;
        rend.material = mat;

        MoverObstaculo mover = obs.AddComponent<MoverObstaculo>();
        mover.puntoA = puntoA;
        mover.puntoB = puntoB;
        mover.velocidad = velocidad;
    }

    void CrearParedLaberinto(GameObject parent, Vector3 posicion, Vector3 escala)
    {
        GameObject pared = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pared.name = "ParedLaberinto";
        pared.transform.position = posicion;
        pared.transform.localScale = escala;
        pared.transform.parent = parent.transform;
    }

    void CrearCanvasUI()
    {
        // Crear Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // TextoContador
        GameObject textoContadorObj = new GameObject("TextoContador");
        textoContadorObj.transform.SetParent(canvasObj.transform, false);
        Text textoContador = textoContadorObj.AddComponent<Text>();
        textoContador.text = "Contador: 0";
        textoContador.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textoContador.fontSize = 24;
        textoContador.color = Color.white;
        RectTransform rtContador = textoContadorObj.GetComponent<RectTransform>();
        rtContador.anchorMin = new Vector2(0, 1);
        rtContador.anchorMax = new Vector2(0, 1);
        rtContador.pivot = new Vector2(0, 1);
        rtContador.anchoredPosition = new Vector2(10, -10);
        rtContador.sizeDelta = new Vector2(300, 40);

        // TextoReloj
        GameObject textoRelojObj = new GameObject("TextoReloj");
        textoRelojObj.transform.SetParent(canvasObj.transform, false);
        Text textoReloj = textoRelojObj.AddComponent<Text>();
        textoReloj.text = "Tiempo: 0";
        textoReloj.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textoReloj.fontSize = 24;
        textoReloj.color = Color.white;
        RectTransform rtReloj = textoRelojObj.GetComponent<RectTransform>();
        rtReloj.anchorMin = new Vector2(0.5f, 1);
        rtReloj.anchorMax = new Vector2(0.5f, 1);
        rtReloj.pivot = new Vector2(0.5f, 1);
        rtReloj.anchoredPosition = new Vector2(0, -10);
        rtReloj.sizeDelta = new Vector2(300, 40);

        // TextoVictoria
        GameObject textoVictoriaObj = new GameObject("TextoVictoria");
        textoVictoriaObj.transform.SetParent(canvasObj.transform, false);
        Text textoVictoria = textoVictoriaObj.AddComponent<Text>();
        textoVictoria.text = "";
        textoVictoria.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textoVictoria.fontSize = 48;
        textoVictoria.color = Color.yellow;
        textoVictoria.alignment = TextAnchor.MiddleCenter;
        RectTransform rtVictoria = textoVictoriaObj.GetComponent<RectTransform>();
        rtVictoria.anchorMin = new Vector2(0.5f, 0.5f);
        rtVictoria.anchorMax = new Vector2(0.5f, 0.5f);
        rtVictoria.pivot = new Vector2(0.5f, 0.5f);
        rtVictoria.anchoredPosition = Vector2.zero;
        rtVictoria.sizeDelta = new Vector2(600, 100);
        textoVictoriaObj.SetActive(false);

        // Asignar referencias al GameManager
        GameManager gm = Object.FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.textoReloj = textoReloj;
            gm.textoVictoria = textoVictoria;
        }

        // Asignar referencias al JugadorController
        JugadorController jc = Object.FindObjectOfType<JugadorController>();
        if (jc != null)
        {
            jc.textoContador = textoContador;
        }
    }

    void CrearPausaManager()
    {
        // Crear el panel de pausa dentro de un Canvas existente
        Canvas canvas = Object.FindObjectOfType<Canvas>();
        if (canvas == null) return;

        GameObject panelPausa = new GameObject("PanelPausa");
        panelPausa.transform.SetParent(canvas.transform, false);
        Image panelImg = panelPausa.AddComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0.7f);
        RectTransform rtPanel = panelPausa.GetComponent<RectTransform>();
        rtPanel.anchorMin = Vector2.zero;
        rtPanel.anchorMax = Vector2.one;
        rtPanel.offsetMin = Vector2.zero;
        rtPanel.offsetMax = Vector2.zero;

        // Título PAUSA
        GameObject tituloPausa = new GameObject("TituloPausa");
        tituloPausa.transform.SetParent(panelPausa.transform, false);
        Text txtPausa = tituloPausa.AddComponent<Text>();
        txtPausa.text = "PAUSA";
        txtPausa.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txtPausa.fontSize = 48;
        txtPausa.color = Color.white;
        txtPausa.alignment = TextAnchor.MiddleCenter;
        RectTransform rtTitulo = tituloPausa.GetComponent<RectTransform>();
        rtTitulo.anchorMin = new Vector2(0.5f, 0.75f);
        rtTitulo.anchorMax = new Vector2(0.5f, 0.75f);
        rtTitulo.pivot = new Vector2(0.5f, 0.5f);
        rtTitulo.anchoredPosition = Vector2.zero;
        rtTitulo.sizeDelta = new Vector2(400, 80);

        // Botón Reanudar
        CrearBotonPausa(panelPausa, "BtnReanudar", "Reanudar", new Vector2(0, 30));
        // Botón Reiniciar
        CrearBotonPausa(panelPausa, "BtnReiniciar", "Reiniciar", new Vector2(0, -30));
        // Botón Menú Principal
        CrearBotonPausa(panelPausa, "BtnMenu", "Menú Principal", new Vector2(0, -90));

        panelPausa.SetActive(false);

        // Crear PausaManager GameObject (se auto-configura en Awake)
        GameObject pausaGO = new GameObject("PausaManager");
        pausaGO.AddComponent<PausaManager>();
    }

    void CrearBotonPausa(GameObject parent, string nombre, string texto, Vector2 posicion)
    {
        GameObject btnObj = new GameObject(nombre);
        btnObj.transform.SetParent(parent.transform, false);
        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        Button btn = btnObj.AddComponent<Button>();
        RectTransform rtBtn = btnObj.GetComponent<RectTransform>();
        rtBtn.anchorMin = new Vector2(0.5f, 0.5f);
        rtBtn.anchorMax = new Vector2(0.5f, 0.5f);
        rtBtn.pivot = new Vector2(0.5f, 0.5f);
        rtBtn.anchoredPosition = posicion;
        rtBtn.sizeDelta = new Vector2(200, 50);

        GameObject txtObj = new GameObject("Text");
        txtObj.transform.SetParent(btnObj.transform, false);
        Text txt = txtObj.AddComponent<Text>();
        txt.text = texto;
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.fontSize = 20;
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;
        RectTransform rtTxt = txtObj.GetComponent<RectTransform>();
        rtTxt.anchorMin = Vector2.zero;
        rtTxt.anchorMax = Vector2.one;
        rtTxt.offsetMin = Vector2.zero;
        rtTxt.offsetMax = Vector2.zero;
    }

    void ConfigurarCamara()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            GameObject jugador = GameObject.Find("Jugador");
            if (jugador != null)
            {
                CamaraController cc = cam.gameObject.AddComponent<CamaraController>();
                cc.jugador = jugador;
                cam.transform.position = jugador.transform.position + new Vector3(0, 10, -10);
                cam.transform.LookAt(jugador.transform);
            }
        }
    }

    void ConfigurarEscenaOpciones()
    {
        // Abrir la escena Opciones existente
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/Opciones.unity.unity");

        // Buscar si ya existe un OpcionesManager
        OpcionesManager existing = Object.FindObjectOfType<OpcionesManager>();
        if (existing == null)
        {
            GameObject opcionesGO = new GameObject("OpcionesManager");
            opcionesGO.AddComponent<OpcionesManager>();
        }

        EditorSceneManager.SaveScene(scene);
        Debug.Log("Escena Opciones configurada con OpcionesManager.");
    }
}
#endif
