using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLayerTesting : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        GraphicPanel panel = GraphicPanelManager.Instance.GetPanel("Background");
        GraphicLayer layer0 = panel.GetLayer(0, true);
        GraphicLayer layer1 = panel.GetLayer(1, true);

        yield return new WaitForSeconds(1);

        Texture blendTex = Resources.Load<Texture>("Graphics/Transition Effects/hurricane");
        layer0.SetTexture("Graphics/BG Images/Spaceshipinterior");


        layer1.SetVideo("Graphics/BG Videos/Nebula");

        layer0.SetTexture("Graphics/BG Images/Spaceshipinterior");

        //layer.currentGraphic.renderer.material.SetColor("_Color", Color.red);

        yield return null;
    }
}
