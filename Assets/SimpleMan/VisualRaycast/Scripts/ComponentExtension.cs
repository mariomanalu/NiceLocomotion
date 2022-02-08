using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    public static class ComponentExtension
    {
        private const float DEFAULT_MAX_DISTANCE = 100;
        private const int DEFAULT_LAYER_MASK = ~0;
        private const bool DEFAULT_CAST_ALL = false;

        #region RAYCAST
        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, float maxDistance, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float maxDistance, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, mask, castAll, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, mask, castAll, ignoreSelf);
        }

        private static CastResult MakeRaycast(Component sender, Ray ray, float maxDistance, LayerMask mask, bool castAll, bool ignoreSelf)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, mask);
            CastResult result = CalculateResult(hits, sender, !castAll, ignoreSelf);


            VisualCastDrawer.Instance?.DrawGizmo(new RaycastInfo(ray, maxDistance, castAll, result));
            return result;
        }
        #endregion

        private static CastResult CalculateResult(RaycastHit[] hits, Component sender, bool fistHitOnly, bool ignoreSelf)
        {
            List<RaycastHit> hitsList = hits.ToList();
            hits = null;


            if (ignoreSelf)
            {
                Transform senderTransform = sender.transform;
                List<RaycastHit> selfHits = new List<RaycastHit>();
                foreach (var item in hitsList)
                {
                    if (item.transform == senderTransform || item.transform.IsChildOf(senderTransform))
                        selfHits.Add(item);
                }
                foreach (var item in selfHits)
                {
                    hitsList.Remove(item);
                }
            }

            if (hitsList.Count > 0 && fistHitOnly)
            {
                RaycastHit fitsHit = hitsList[0];
                hitsList.Clear();
                hitsList.Add(fitsHit);
            }
            return new CastResult(hitsList.ToArray());
        }
    }
}
