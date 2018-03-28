    using UnityEngine;
     
    public static class RendererExtensions
    {
        /// <summary>
        /// Counts the bounding box corners of the given RectTransform that are visible from the given Camera in screen space.
        /// </summary>
        /// <returns>The amount of bounding box corners that are visible from the Camera.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera.</param>
        private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera, RectTransform within)
        {
			Vector3[] withinCorners = new Vector3[4];
			within.GetWorldCorners(withinCorners);
            float widht = camera.WorldToScreenPoint(withinCorners[2]).x - camera.WorldToScreenPoint(withinCorners[0]).x;
            float height = camera.WorldToScreenPoint(withinCorners[1]).y - camera.WorldToScreenPoint(withinCorners[0]).y;
            Rect screenBounds = new Rect(camera.WorldToScreenPoint(withinCorners[0]).x, camera.WorldToScreenPoint(withinCorners[0]).y, widht, height);
            Vector3[] objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);
            int visibleCorners = 0;
            Vector3 tempScreenSpaceCorner; // Cached
            for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
            {
                tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
                if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
                {
                    visibleCorners++;
                }
            }
            return visibleCorners;
        }
     
        /// <summary>
        /// Determines if this RectTransform is fully visible from the specified camera.
        /// Works by checking if each bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
        /// </summary>
        /// <returns><c>true</c> if is fully visible from the specified camera; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera.</param>
        public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera,  RectTransform within)
        {
            return CountCornersVisibleFrom(rectTransform, camera,within) == 4; // True if all 4 corners are visible
        }
     
        /// <summary>
        /// Determines if this RectTransform is at least partially visible from the specified camera.
        /// Works by checking if any bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
        /// </summary>
        /// <returns><c>true</c> if is at least partially visible from the specified camera; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera.</param>
        public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera, RectTransform within)
        {
            return CountCornersVisibleFrom(rectTransform, camera, within) > 0; // True if any corners are visible
        }
    }
