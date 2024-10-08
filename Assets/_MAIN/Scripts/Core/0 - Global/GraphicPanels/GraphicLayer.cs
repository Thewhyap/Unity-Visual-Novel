using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class GraphicLayer
{
    public const string LAYER_OBJECT_NAME_FORMAT = "Layer: {0}";
    public int layerDepth = 0;
    public Transform panel;

    public GraphicObject currentGraphic = null;
    private List<GraphicObject> oldGraphics = new();

    public void SetTexture(string filePath, float transitionSpeed = 1f, Texture blendingTexture = null) //TODO maybe change blending texture and see how to make better transitions
    {
        Texture texture = Resources.Load<Texture2D>(filePath);

        if (texture == null)
        {
            Debug.LogError($"Could not load graphic texture from path '{filePath}'.");
            return;
        }

        SetTexture(texture, transitionSpeed, blendingTexture);
    }

    public void SetTexture(Texture texture, float transitionSpeed = 1f, Texture blendingTexture = null)
    {
        if (currentGraphic != null && !oldGraphics.Contains(currentGraphic)) oldGraphics.Add(currentGraphic);

        currentGraphic = new GraphicTexture(this, texture);
        currentGraphic.FadeIn(transitionSpeed, blendingTexture);
    }

    public void SetVideo(string filePath, float transitionSpeed = 1f, Texture blendingTexture = null, bool useAudio = true, bool looping = true)
    {
        VideoClip clip = Resources.Load<VideoClip>(filePath);
        if (clip == null)
        {
            Debug.LogError($"Could not load graphic video from path '{filePath}'.");
            return;
        }

        SetVideo(clip, transitionSpeed, blendingTexture, useAudio, looping);
    }
    public void SetVideo(VideoClip video, float transitionSpeed = 1f, Texture blendingTexture = null, bool useAudio = true, bool looping = true)
    {
        if (currentGraphic != null && !oldGraphics.Contains(currentGraphic)) oldGraphics.Add(currentGraphic);

        currentGraphic = new GraphicVideo(this, video, useAudio, looping);
        currentGraphic.FadeIn(transitionSpeed, blendingTexture);
    }

    public void DestroyOldGraphics()
    {
        foreach (var oldGraphic in oldGraphics) Object.Destroy(oldGraphic.renderer.gameObject);
        oldGraphics.Clear();
    }

    public void Clear()
    {
        currentGraphic?.FadeOut();
        foreach (var oldGraphic in oldGraphics) oldGraphic.FadeOut();
    }
}
