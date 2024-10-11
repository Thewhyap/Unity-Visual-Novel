using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GraphicTexture: GraphicObject
{
    public GraphicTexture(GraphicLayer layer, Texture texture) : base(layer, texture.name)
    {
        renderer.material.SetTexture(MATERIAL_FIELD_MAINTEX, texture);
    }

    public override IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
