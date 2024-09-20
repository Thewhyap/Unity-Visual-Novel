using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GraphicObject
{
    private const string NAME_FORMAT = "Graphic - [{0}]";
    private const string MATERIAL_PATH = "Materials/layerTransitionMaterial";
    private const string MATERIAL_FIELD_COLOR = "_Color";
    private const string MATERIAL_FIELD_MAINTEX = "_MainTex";
    private const string MATERIAL_FIELD_BLENDTEX = "_BlendTex";
    private const string MATERIAL_FIELD_BLEND = "_Blend";
    private const string MATERIAL_FIELD_ALPHA = "_Alpha";
    public RawImage renderer;

    private GraphicLayer layer;

    public bool isVideo { get { return video != null; } }
    public VideoPlayer video = null;
    public AudioSource audio = null;

    public string graphicPath = "";
    public string graphicName { get; private set; }
    private Coroutine co_fadingIn = null;
    private Coroutine co_fadingOut = null;

    public GraphicObject(GraphicLayer layer, string graphicPath, Texture texture)
    {
        this.graphicPath = graphicPath;
        this.layer = layer;

        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(layer.panel);
        renderer = gameObject.AddComponent<RawImage>();

        graphicName = texture.name;

        InitGraphic();

        renderer.name = string.Format(NAME_FORMAT, graphicName);
        renderer.material.SetTexture(MATERIAL_FIELD_MAINTEX, texture);
    }

    public GraphicObject(GraphicLayer layer, string graphicPath, VideoClip clip, bool useAudio)
    {
        this.graphicPath = graphicPath;
        this.layer = layer;

        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(layer.panel);
        renderer = gameObject.AddComponent<RawImage>();
  
        graphicName = clip.name;
        renderer.name = string.Format(NAME_FORMAT, graphicName);

        InitGraphic();

        RenderTexture texture = new RenderTexture(Mathf.RoundToInt(clip.width), Mathf.RoundToInt(clip.height), 0);
        renderer.material.SetTexture(MATERIAL_FIELD_MAINTEX, texture);

        video = renderer.gameObject.AddComponent<VideoPlayer>();
        video.playOnAwake = true;
        video.source = VideoSource.VideoClip;
        video.clip = clip;
        video.renderMode = VideoRenderMode.RenderTexture;
        video.targetTexture = texture;
        video.isLooping = true;

        video.audioOutputMode = VideoAudioOutputMode.AudioSource;
        audio = video.gameObject.AddComponent<AudioSource>();

        audio.volume = 0;
        if (!useAudio) audio.mute = true;

        video.SetTargetAudioSource(0, audio);

        video.frame = 0;
        video.Prepare();
        video.Play();

        video.enabled = false;
        video.enabled = true; //TODO find a better way to do it
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

    GraphicPanelManager panelManager => GraphicPanelManager.instance;
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

    public IEnumerator Fading(float target, float speed, Texture blend)
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

            if (isVideo) audio.volume = opacity;

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
