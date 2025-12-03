using UnityEngine;

public class InitializeMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] CustomRenderTexture tex = default;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tex.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
