using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    [AddComponentMenu("Scripts/MRTK/Examples/HandInteractionTouch")]
    public class CubeFourOri : MonoBehaviour, IMixedRealityTouchHandler
    {
        [SerializeField]
        private TextMesh debugMessage = null;
        [SerializeField]
        private TextMesh debugMessage2 = null;

        #region Event handlers
        public TouchEvent OnTouchCompleted;
        public TouchEvent OnTouchStarted;
        public TouchEvent OnTouchUpdated;
        #endregion

        private Renderer TargetRenderer;
        private Color originalColor;
        private Color highlightedColor;

        protected float duration = 1.5f;
        protected float t = 0;
        public float positionX;
        public float positionY;
        public float positionZ;
        private void Start()
        {
            TargetRenderer = GetComponentInChildren<Renderer>();
            positionX = TargetRenderer.transform.position.x;
            positionY = TargetRenderer.transform.position.y;
            positionZ = TargetRenderer.transform.position.z;
            if ((TargetRenderer != null) && (TargetRenderer.sharedMaterial != null))
            {
                originalColor = TargetRenderer.sharedMaterial.color;
                highlightedColor = new Color(originalColor.r + 0.2f, originalColor.g + 0.2f, originalColor.b + 0.2f);
            }
        }
        void Update()
        {

        }

        void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
        {
            OnTouchCompleted.Invoke(eventData);

            if (debugMessage != null)
            {
                debugMessage.text = "OnTouchCompleted: " + Time.unscaledTime.ToString();
            }

            if ((TargetRenderer != null) && (TargetRenderer.material != null))
            {
                TargetRenderer.material.color = originalColor;
            }
        }

        void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
        {
            OnTouchStarted.Invoke(eventData);

            if (debugMessage != null)
            {
                debugMessage.text = "OnTouchStarted: " + Time.unscaledTime.ToString();
            }

            if (TargetRenderer != null)
            {
                TargetRenderer.sharedMaterial.color = Color.Lerp(originalColor, highlightedColor, 2.0f);
            }
            Renderer m_renderer = GetComponent<Renderer>();
            m_renderer.enabled = false;

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("FakeCubeFour"))
            {
                Renderer renderer = go.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = true;
                }
            }
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("CubeFiveOri"))
            {
                Renderer renderer = go.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.green;
                }
            }
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Board"))
            {
                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                if (renderer != null)
                {
                    renderer.text = "Step five: Touch the next green rectangle.";
                }
            }
            
        }

        void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
        {
            OnTouchUpdated.Invoke(eventData);

            if (debugMessage2 != null)
            {
                debugMessage2.text = "OnTouchUpdated: " + Time.unscaledTime.ToString();
            }

            if ((TargetRenderer != null) && (TargetRenderer.material != null))
            {
                TargetRenderer.material.color = Color.Lerp(Color.green, Color.red, t);
                t = Mathf.PingPong(Time.time, duration) / duration;
            }
        }
    }
}

