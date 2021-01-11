using DCL.SettingsController;
using UnityEngine;

namespace DCL.SettingsPanelHUD.Controls
{
    [CreateAssetMenu(menuName = "Settings/Controllers/Controls/Draw Distance", fileName = "DrawDistanceControlController")]
    public class DrawDistanceControlController : SettingsControlController
    {
        public override object GetStoredValue()
        {
            return currentQualitySetting.cameraDrawDistance;
        }

        public override void OnControlChanged(object newValue)
        {
            currentQualitySetting.cameraDrawDistance = (float)newValue;

            if (QualitySettingsReferences.i.thirdPersonCamera)
                QualitySettingsReferences.i.thirdPersonCamera.m_Lens.FarClipPlane = currentQualitySetting.cameraDrawDistance;

            if (QualitySettingsReferences.i.firstPersonCamera)
                QualitySettingsReferences.i.firstPersonCamera.m_Lens.FarClipPlane = currentQualitySetting.cameraDrawDistance;

            RenderSettings.fogEndDistance = currentQualitySetting.cameraDrawDistance;
            RenderSettings.fogStartDistance = currentQualitySetting.cameraDrawDistance * 0.8f;
        }
    }
}