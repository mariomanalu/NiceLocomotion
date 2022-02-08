using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    internal abstract class GizmoDrawHandler
    {
        //------PROPERTIES
        public float CurrentAlpha
        {
            get => _fadeTime == 0 ? 1 : 1 / (_fadeTime / (_fadeTime - (_fadeTime - TimeLeft)));
        }
        public ICastInfo Info { get; protected set; }
        public float TimeLeft
        {
            get => _bornTime + _fadeTime - Time.realtimeSinceStartup;
        }




        //------FIELDS
        private float _bornTime;
        private float _fadeTime;




        //------CONSTRUCTORS
        public GizmoDrawHandler(ICastInfo info, float fadeTime)
        {
            Info = info;
            _fadeTime = fadeTime;
            _bornTime = Time.realtimeSinceStartup;
        }




        //------METHODS
        public abstract void Draw();
    }

    internal class RaycastDrawHandler : GizmoDrawHandler
    {
        public RaycastDrawHandler(ICastInfo info, float fadeTime) : base(info, fadeTime)
        {
        }

        public override void Draw()
        {
            if (Info.CastResult)
            {
                //Is using cast all? -> Draw additional gizmo
                if (Info.CastAll)
                {
                    //Set new color
                    Gizmos.color = new Color(VisualCastDrawer.Instance.HitColor.r,
                                             VisualCastDrawer.Instance.HitColor.g,
                                             VisualCastDrawer.Instance.HitColor.b,
                                             CurrentAlpha);

                    //Line from origin to first hit
                    Vector3 lineOriginPoint = Info.OriginPoint,
                            lineEndPoint = Info.CastResult.FirstHit.point;
                    Gizmos.DrawLine(lineOriginPoint, lineEndPoint);

                    //Draw points for each hit
                    for (int i = 0; i < Info.CastResult.Hits.Length; i++)
                    {
                        if (i != 0)
                            Gizmos.DrawLine(Info.OriginPoint + Info.Direction * Info.CastResult.Hits[i - 1].distance, Info.OriginPoint + Info.Direction * Info.MaxDistance);

                        Gizmos.DrawSphere(Info.CastResult.Hits[i].point, VisualCastDrawer.Instance.HitIndicatorScale);
                    }

                    //Set new color
                    Gizmos.color = new Color(VisualCastDrawer.Instance.NoHitColor.r,
                                             VisualCastDrawer.Instance.NoHitColor.g,
                                             VisualCastDrawer.Instance.NoHitColor.b,
                                             CurrentAlpha);

                    //Line from last hit to max distance point
                    lineOriginPoint = Info.OriginPoint + Info.Direction * Info.CastResult.LastHit.distance;
                    lineEndPoint = Info.OriginPoint + Info.Direction * Info.CastResult.LastHit.distance + Info.Direction * (Info.MaxDistance - Info.CastResult.LastHit.distance);
                    Gizmos.DrawLine(lineOriginPoint, lineEndPoint);
                }
                else
                {
                    //Set new color
                    Gizmos.color = new Color(VisualCastDrawer.Instance.HitColor.r,
                                             VisualCastDrawer.Instance.HitColor.g,
                                             VisualCastDrawer.Instance.HitColor.b,
                                             CurrentAlpha);

                    //Line from origin to last hit
                    Gizmos.DrawLine(Info.OriginPoint, Info.OriginPoint + Info.Direction * Info.CastResult.FirstHit.distance);
                    Gizmos.DrawSphere(Info.OriginPoint + Info.Direction * Info.CastResult.LastHit.distance, VisualCastDrawer.Instance.HitIndicatorScale);
                }
            }
            else
            {
                //Set no hitcolor
                Gizmos.color = new Color(VisualCastDrawer.Instance.NoHitColor.r,
                                         VisualCastDrawer.Instance.NoHitColor.g,
                                         VisualCastDrawer.Instance.NoHitColor.b,
                                         CurrentAlpha);


                //Line from origin point to end point
                Gizmos.DrawLine(Info.OriginPoint, Info.OriginPoint + Info.Direction * Info.MaxDistance);
            }
        }
    }


    [AddComponentMenu("Simple Man/Visual Raycast/Visual Raycast Drawer")]
    public class VisualCastDrawer : MonoBehaviour
    {
        //------PROPERTIES
        public static VisualCastDrawer Instance { get; private set; }
        public float FadeTime
        {
            get => _fadeTime;
            set => _fadeTime = Mathf.Clamp(value, 0, float.MaxValue);
        }
        public float HitIndicatorScale
        {
            get => _hitIndicatorScale;
            set => _hitIndicatorScale = Mathf.Clamp(value, 0.01f, 0.5f);
        }
        public Color HitColor
        {
            get => _hitColor;
            set => _hitColor = value;
        }
        public Color NoHitColor
        {
            get => _noHitColor;
            set => _noHitColor = value;
        }




        //------FIELDS
        [SerializeField, Min(0f)]
        private float _fadeTime = 1f;

        [SerializeField]
        private Color _hitColor = Color.green,
                      _noHitColor = Color.red;

        [SerializeField, Range(0.01f, 0.5f)]
        private float _hitIndicatorScale = 0.1f;

        private List<GizmoDrawHandler> _drawHandlers = new List<GizmoDrawHandler>();



        //------EVENTS




        //------METHODS
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.A))
            //    DrawGizmo(new RaycastInfo(Vector3.zero, Vector3.up, 10, new CastResult(new RaycastHit[] { })));

            //if (Input.GetKeyDown(KeyCode.S))
            //    DrawGizmo(new BoxcastInfo(Vector3.zero, Vector3.up, Vector3.one, Quaternion.identity, 10, new CastResult(new RaycastHit[] { })));

            //if (Input.GetKeyDown(KeyCode.D))
            //    DrawGizmo(new SpherecastInfo(Vector3.zero, Vector3.up, 1f, 0, new CastResult(new RaycastHit[] { })));

        }

        private void OnDrawGizmos()
        {
            List<GizmoDrawHandler> toRemove = new List<GizmoDrawHandler>();
            foreach (var item in _drawHandlers)
            {
                //Cache previous color
                Color previousColor = Gizmos.color;

                item.Draw();

                if (item.TimeLeft <= 0)
                    toRemove.Add(item);

                //Reset color
                Gizmos.color = previousColor;
            }

            foreach (var item in toRemove)
            {
                _drawHandlers.Remove(item);
            }
        }

        internal void DrawGizmo(ICastInfo info)
        {
            if (info is RaycastInfo)
                _drawHandlers.Add(new RaycastDrawHandler(info, _fadeTime));
        }
    }
}
