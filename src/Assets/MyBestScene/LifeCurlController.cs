using UnityEngine;
using UnityEngine.UI;

public class LifeCurlController : MonoBehaviour
{
    public ComputeShader compute;
    public RenderTexture stateRT;
    public int textureSize = 512;

    int kernel;
    Vector2Int resolution;

    void Start()
    {
        // RenderTexture ‰Šú‰»
        if (stateRT == null)
        {
            stateRT = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.R8);
            stateRT.enableRandomWrite = true;
            stateRT.wrapMode = TextureWrapMode.Repeat;
            stateRT.filterMode = FilterMode.Bilinear;
            stateRT.Create();
        }

        kernel = compute.FindKernel("Update");
        resolution = new Vector2Int(stateRT.width, stateRT.height);

        compute.SetVector("Resolution", new Vector2(resolution.x, resolution.y));
        compute.SetTexture(kernel, "State", stateRT);
        compute.SetTexture(kernel, "StateNext", stateRT); // ƒVƒ“ƒvƒ‹‚É“¯‚¶RT‚É‘‚­ê‡
    }

    void Update()
    {
        compute.SetFloat("Time", Time.time);
        compute.SetFloat("DeltaTime", Time.deltaTime);

        int groupsX = Mathf.CeilToInt(resolution.x / 8.0f);
        int groupsY = Mathf.CeilToInt(resolution.y / 8.0f);

        compute.Dispatch(kernel, groupsX, groupsY, 1);
    }
}