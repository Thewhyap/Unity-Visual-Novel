using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public abstract class GraphicObject
{
    private const string NAME_FORMAT = "Graphic - [{0}]";
    private const string MATERIAL_PATH = "Materials/layerTransitionMaterial";
    private const string MATERIAL_FIELD_COLOR = "_Color";
    protected const string MATERIAL_FIELD_MAINTEX = "_MainTex";
    private const string MATERIAL_FIELD_BLENDTEX = "_BlendTex";
    private const string MATERIAL_FIELD_BLEND = "_Blend";
    private const string MATERIAL_FIELD_ALPHA = "_Alpha";
    public RawImage renderer;

    protected GraphicLayer layer;

    public string graphicName { get; private set; }
    private Coroutine co_fadingIn = null;
    private Coroutine co_fadingOut = null;

    public GraphicObject(GraphicLayer layer, string graphicName)
    {
        this.layer = layer;

        GameObject gameObject = new();
        gameObject.transform.SetParent(layer.panel);
        renderer = gameObject.AddComponent<RawImage>();

        InitGraphic();

        renderer.name = string.Format(NAME_FORMAT, graphicName);
    }

    private void InitGraphic()
    {
        renderer.transform.localPosition = Vector3.zero;
        renderer.transform.localScale = Vector3.one;

        RectTransform rect = renderer.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.one;

        renderer.material = GetTransitionMaterial();

        renderer.material.SetFloat(MATERIAL_FIELD_BLEND, 0);
        renderer.material.SetFloat(MATERIAL_FIELD_ALPHA, 0);
    }

    private Material GetTransitionMaterial()
    {
        //TODO
        //Material material = Resources.Load<Material>(MATERIAL_PATH);
        //if (material != null) return new Material(material);
        return null;
    }

    GraphicPanelManager panelManager => GraphicPanelManager.Instance;
    public Coroutine FadeIn(float speed = 1f, Texture blend = null)
    {
        if (co_fadingOut != null) panelManager.StopCoroutine(co_fadingOut);

        if (co_fadingIn != null) return co_fadingIn;

        co_fadingIn = panelManager.StartCoroutine(Fading(1f, speed, blend));

        return co_fadingIn;
    }

    public Coroutine FadeOut(float speed = 1f, Texture blend = null)
    {
        if (co_fadingIn != null) panelManager.StopCoroutine(co_fadingIn);

        if (co_fadingOut != null) return co_fadingOut;

        co_fadingOut = panelManager.StartCoroutine(Fading(0f, speed, blend));

        return co_fadingOut;
    }

    public IEnumerator Fading(float target, float speed, Texture blend) //TODO verif if no blend means no fading
    {
        bool isBlending = blend != null;
        bool fadingIn = target > 0;

        renderer.material.SetTexture(MATERIAL_FIELD_BLENDTEX, blend);
        renderer.material.SetFloat(MATERIAL_FIELD_ALPHA, isBlending ? 1 : fadingIn ? 0 : 1);
        renderer.material.SetFloat(MATERIAL_FIELD_BLEND, isBlending ? fadingIn ? 0 : 1 : 1);

        string opacityParam = isBlending ? MATERIAL_FIELD_BLEND : MATERIAL_FIELD_ALPHA;

        while(renderer.material.GetFloat(opacityParam) != target)
        {
            float opacity = Mathf.MoveTowards(renderer.material.GetFloat(opacityParam), target, speed * Time.deltaTime); //Want to multiply by default transition speed of the panelManager??
            renderer.material.SetFloat(opacityParam, opacity);

            ManageAudio(opacity);

            yield return null;
        }

        co_fadingIn = null;
        co_fadingOut = null;

        if(target == 0)
        {
            Destroy();
        }
        else
        {
            DestroyOnSameLayers();
        }
    }

    protected void ManageAudio(float opacity) //TODO manage audio fading for videos in a better way
    {
        return;
    }

    public abstract IEnumerator Wait(float time = 0);

    private void Destroy()
    {
        if (layer.currentGraphic != null && layer.currentGraphic.renderer == renderer) layer.currentGraphic = null;
        Object.Destroy(renderer.gameObject);
    }

    private void DestroyOnSameLayers()
    {
        layer.DestroyOldGraphics();
    }
}
