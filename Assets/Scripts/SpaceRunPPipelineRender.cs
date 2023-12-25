using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpaceRunPPipelineRender : RenderPipeline
{
    CameraRender _cameraRender;
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        _cameraRender = new CameraRender();
        CamerasRender(context, cameras);
    }

    private void CamerasRender(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (var camera in cameras)
        {
            _cameraRender.Render(context, camera);
        }
    }
}
