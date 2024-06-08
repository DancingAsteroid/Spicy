using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TeachR.Measurements
{
    public class SceneData : MonoBehaviour
    {
        public string PreviewRooms;
        public string ChemistryRoom;
        public UniversalRendererData UniversalRendererData;
        public UniversalRenderPipelineAsset UniversalRenderPipelineAsset;
        public LightingSettings LightingSettings;
        public bool EnvironmentSkybox;
    }
}
