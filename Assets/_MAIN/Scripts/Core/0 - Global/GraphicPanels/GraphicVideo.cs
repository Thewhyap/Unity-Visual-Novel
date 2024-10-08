using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GraphicVideo: GraphicObject
{
    public VideoPlayer video = null;
    public AudioSource audio = null;

    public GraphicVideo(GraphicLayer layer, VideoClip clip, bool useAudio, bool isLooping): base(layer, clip.name)
    {
        RenderTexture texture = new(Mathf.RoundToInt(clip.width), Mathf.RoundToInt(clip.height), 0);
        renderer.material.SetTexture(MATERIAL_FIELD_MAINTEX, texture);

        video = renderer.gameObject.AddComponent<VideoPlayer>();
        video.playOnAwake = true;
        video.source = VideoSource.VideoClip;
        video.clip = clip;
        video.renderMode = VideoRenderMode.RenderTexture;
        video.targetTexture = texture;
        video.isLooping = isLooping;

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

    protected new void ManageAudio(float opacity)
    {
        audio.volume = opacity;
    }

    public override IEnumerator Wait(float time)
    {
        if(time != 0)
        {
            yield return new WaitForSeconds(time);
        }
        else
        {
            if (video.isLooping)
            {
                yield return new WaitForSeconds((float) video.length);
            }
            bool videoComplete = false;
            video.loopPointReached += (VideoPlayer ignored) => videoComplete = true;
            yield return new WaitUntil(() => videoComplete);
        }
    }
}
