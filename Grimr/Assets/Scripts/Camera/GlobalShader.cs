using UnityEngine;

namespace Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class GlobalShader : MonoBehaviour
    {
        [SerializeField] private Shader _shader;
        
        private void Start()
        {
            // Sets the global shader for the camera to render.
            GetComponent<UnityEngine.Camera>().SetReplacementShader(_shader, null);
        }
    }
}
