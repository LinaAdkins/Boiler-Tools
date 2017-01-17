using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoilerTools.Tools2D
{
    /// <summary>
    /// Provides auto-scrolling texture based on camera movement. You are expected to attach this as a child object of a camera.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    [ExecuteInEditMode]
    public class Parallax2D : MonoBehaviour {

        [Tooltip("Determine how much each axis moves when the camera moves. 0 is no movement, -1 is opposite movement.")]
        public Vector2 MovementScale; // 

        [Tooltip("Pad out the size of the camera so that the quad doesn't end exactly on the edges of the view frustum.")]
        public Vector2 ScalePadding;

        #region Private Members

        new Camera camera;
        Material material;
        Vector3 lastCamera;
        Vector3 cameraDiff;

        #endregion

        void Start() {
            Debug.Assert(IsAttachedToCamera(), "Parallax2D Layer must be child of camera object!");
            camera = GetCamera();
            material = GetComponent<MeshRenderer>().sharedMaterial;

            material.mainTextureOffset = Vector2.zero; // Reset offset so if we quit in editor we aren't left with a weird offset

            ScaleToCamera();
        }

        void Update() {

            // So we can see our quad's size in editor
            if (Application.isEditor)
            {
                ScaleToCamera();
            }

            UpdateTextureOffset();

        }

        /// <summary>
        /// Update our texture offset to reflect camera movement and settings.
        /// </summary>
        void UpdateTextureOffset()
        {
            // Get difference from last camera position
            cameraDiff = lastCamera - camera.transform.position;
            lastCamera = camera.transform.position;

            // Calculate offset movement based on camera difference
            Vector2 offset = new Vector2();
            offset.x = cameraDiff.x * MovementScale.x * Time.deltaTime;
            offset.y = cameraDiff.y * MovementScale.y * Time.deltaTime;

            // Calculate our total offset by adding to material's current offset and repeating value at 1
            offset.x = Mathf.Repeat(offset.x + material.mainTextureOffset.x, 1);
            offset.y = Mathf.Repeat(offset.y + material.mainTextureOffset.y, 1);

            // Apply offset
            material.mainTextureOffset = offset;
        }

        /// <summary>
        /// Check to see if we are the child of a camera like we're supposed to be.
        /// </summary>
        bool IsAttachedToCamera()
        {
            Transform parent = transform.parent;

            if (parent == null) { return false; }

            if (transform.parent.GetComponent<Camera>() == null)
            {
                return false;
            }

            return true;
        }

        Camera GetCamera()
        {
            if (IsAttachedToCamera())
            {
                return transform.parent.GetComponent<Camera>();
            }

            return null;
        }

        /// <summary>
        /// Scale our quad to the camera's size, accounting for any padding settings given.
        /// </summary>
        void ScaleToCamera()
        {
            if (camera == null) { return; }

            Vector3 ViewPort = new Vector3();
            Vector3 BottomLeft = new Vector3();
            Vector3 TopRight = new Vector3();

            // Get bottom left ( world )
            ViewPort.Set(0, 0, transform.position.z);
            BottomLeft = camera.ViewportToWorldPoint(ViewPort);
            
            // Get top right ( world )
            ViewPort.Set(1, 1, transform.position.z);
            TopRight = camera.ViewportToWorldPoint(ViewPort);

            // Get width and height ( world units )
            float width = BottomLeft.x - TopRight.x;
            float height = BottomLeft.y - TopRight.y;

            // Mind our signs when adding to width/height scales
            width += ScalePadding.x * Mathf.Sign(width);
            height += ScalePadding.y * Mathf.Sign(height);

            // Apply scale changes
            transform.localScale = new Vector3(width, height, transform.localScale.z);
        }
    }

}