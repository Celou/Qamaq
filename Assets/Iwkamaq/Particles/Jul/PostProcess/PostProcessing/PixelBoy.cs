/*
 * Je ne sais hélas pas qui à rédigé ce script à l'origine :(
 * 
 * Glisser ce script directement sur votre camera
 * 
 * Dans la variable "w", mettez la largeur de la resolution attendu
 * 
 * /!\ Attention ! Pensez-bien à mettre dans la size de votre camera la hauteur divisé par deux de la résolution attendu !
 * 
 * la bise.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/PixelBoy")]
public class PixelBoy : MonoBehaviour
{
    public int w = 720;
    private int h;

    public Camera cam;

    protected void Start()
    {
        cam = GetComponent<Camera>();

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }
    void Update()
    {

        float ratio = ((float)cam.pixelHeight / (float)cam.pixelWidth);
        h = Mathf.RoundToInt(w * ratio);

    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture buffer = RenderTexture.GetTemporary(w, h, -1);
        buffer.filterMode = FilterMode.Point;
        Graphics.Blit(source, buffer);
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    }
}