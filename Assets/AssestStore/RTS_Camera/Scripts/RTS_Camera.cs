﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("RTS Camera")]
    public class RTS_Camera : MonoBehaviour
    {
		public bool useFixedUpdate = true; //use FixedUpdate() or Update()
        static RTS_Camera instance;

        public RawImage rawImage;
        public static RTS_Camera GetInstance{get{return instance;}}
        public static Camera Camera{
            get{
                if(instance == null)
                    return null;
                return instance.GetComponent<Camera>();}}
        #region Foldouts

#if UNITY_EDITOR

        public int lastTab = 0;

        public bool movementSettingsFoldout;
        public bool zoomingSettingsFoldout;
        public bool rotationSettingsFoldout;
        public bool heightSettingsFoldout;
        public bool mapLimitSettingsFoldout;
        public bool targetingSettingsFoldout;
        public bool inputSettingsFoldout;

#endif

        #endregion

        private Transform m_Transform; //camera tranform
        
        #region Movement

        public float keyboardMovementSpeed = 5f; //speed with keyboard movement
        public float screenEdgeMovementSpeed = 3f; //spee with screen edge movement
        public float followingSpeed = 5f; //speed when following a target
        public float rotationSped = 3f;
        public float panningSpeed = 10f;
        public float mouseRotationSpeed = 10f;

        #endregion

        #region Height

        public LayerMask groundMask = -1; //layermask of ground or other objects that affect height

        public float maxHeight = 10f; //maximal height
        public float minHeight = 15f; //minimnal height
        public float heightDampening = 5f; 
        public float keyboardZoomingSensitivity = 2f;
        public float scrollWheelZoomingSensitivity = 25f;

        private float zoomPos = 0; //value in range (0, 1) used as t in Matf.Lerp

        #endregion

        #region MapLimits

        public bool limitMap = true;
        public float limitX = 50f; //x limit of map
        public float limitY = 50f; //z limit of map

        #endregion

        #region Targeting

        public Transform targetFollow; //target to follow
        public Vector3 targetOffset;

        /// <summary>
        /// are we following target
        /// </summary>
        public bool FollowingTarget
        {
            get
            {
                return targetFollow != null;
            }
        }

        #endregion

        #region Input

        public bool useScreenEdgeInput = true;
        public float screenEdgeBorder = 25f;

        public bool useKeyboardInput = true;
        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";

        public bool usePanning = true;
        public KeyCode panningKey = KeyCode.Mouse2;

        public bool useKeyboardZooming = true;
        public KeyCode zoomInKey = KeyCode.E;
        public KeyCode zoomOutKey = KeyCode.Q;

        public bool useScrollwheelZooming = true;
        public string zoomingAxis = "Mouse ScrollWheel";

        public bool useKeyboardRotation = true;
        public KeyCode rotateRightKey = KeyCode.X;
        public KeyCode rotateLeftKey = KeyCode.Z;

        public bool useMouseRotation = true;
        public KeyCode mouseRotationKey = KeyCode.Mouse1;

        private Vector2 KeyboardInput
        {
            get {
                float horizontal = (Input.GetKey(KeyCode.A)) ? -1 : 0 + ((Input.GetKey(KeyCode.D)) ? 1 : 0);
                float vertical = (Input.GetKey(KeyCode.S)) ? -1 : 0 + ((Input.GetKey(KeyCode.W)) ? 1 : 0);

                Vector2 input = new Vector2(horizontal, vertical);
                return useKeyboardInput ? input : Vector2.zero;
            }
        }

        private Vector2 MouseInput
        {
            get { return Input.mousePosition; }
        }

        private float ScrollWheel
        {
            get { return Input.GetAxis(zoomingAxis); }
        }

        private Vector2 MouseAxis
        {
            get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
        }

        private int ZoomDirection
        {
            get
            {
                bool zoomIn = Input.GetKey(zoomInKey);
                bool zoomOut = Input.GetKey(zoomOutKey);
                if (zoomIn && zoomOut)
                    return 0;
                else if (!zoomIn && zoomOut)
                    return 1;
                else if (zoomIn && !zoomOut)
                    return -1;
                else 
                    return 0;
            }
        }

        private int RotationDirection
        {
            get
            {
                bool rotateRight = Input.GetKey(rotateRightKey);
                bool rotateLeft = Input.GetKey(rotateLeftKey);
                if(rotateLeft && rotateRight)
                    return 0;
                else if(rotateLeft && !rotateRight)
                    return -1;
                else if(!rotateLeft && rotateRight)
                    return 1;
                else 
                    return 0;
            }
        }

        #endregion

        #region Unity_Methods

        private void Start()
        {
            m_Transform = transform;
			zoomPos = 0.5f;
            instance = this;
        }

        private void Update()
        {
            if (!useFixedUpdate)
                CameraUpdate();
        }

        private void FixedUpdate()
        {
            if (useFixedUpdate)
                CameraUpdate();
        }

        #endregion

        #region RTSCamera_Methods

        int width;
        int height;
        /// <summary>
        /// update camera movement and rotation
        /// </summary>
        private void CameraUpdate()
        {
            if (FollowingTarget)
                FollowTarget();
            else
                Move();

            HeightCalculation();
            Rotation();
            LimitPosition();
            if(height != Screen.height  || width != Screen.width)
                SetRenderTexutre();
        }

        // set Rendertexture
        void SetRenderTexutre(){
            if(Camera.targetTexture != null)
                Camera.targetTexture.Release();
            width = Screen.width;
            height = Screen.height;
            Camera.targetTexture = new RenderTexture( width, height, 24 );
            rawImage.texture = Camera.targetTexture;
        }

        /// <summary>
        /// move camera with keyboard or with screen edge
        /// </summary>
        private void Move()
        {
            if (useKeyboardInput)
            {
                Vector3 desiredMove = new Vector3(KeyboardInput.x, 0, KeyboardInput.y);

                desiredMove *= keyboardMovementSpeed;
                desiredMove *= Time.deltaTime;
				desiredMove *= 1 + zoomPos;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);

                m_Transform.Translate(desiredMove, Space.Self);
            }

			if (useScreenEdgeInput && !Input.GetKey(mouseRotationKey))
            {
                Vector3 desiredMove = new Vector3();

                Rect leftRect = new Rect(0, 0, screenEdgeBorder, Screen.height);
                Rect rightRect = new Rect(Screen.width - screenEdgeBorder, 0, screenEdgeBorder, Screen.height);
                Rect upRect = new Rect(0, Screen.height - screenEdgeBorder, Screen.width, screenEdgeBorder);
                Rect downRect = new Rect(0, 0, Screen.width, screenEdgeBorder);

                desiredMove.x = leftRect.Contains(MouseInput) ? -1 : rightRect.Contains(MouseInput) ? 1 : 0;
                desiredMove.z = upRect.Contains(MouseInput) ? 1 : downRect.Contains(MouseInput) ? -1 : 0;

                desiredMove *= screenEdgeMovementSpeed;
                desiredMove *= Time.deltaTime;
				desiredMove *= 1 + zoomPos;

                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);

                m_Transform.Translate(desiredMove, Space.Self);
            }       
        
            if(usePanning && Input.GetKey(panningKey) && MouseAxis != Vector2.zero)
            {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

                desiredMove *= panningSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);

                m_Transform.Translate(desiredMove, Space.Self);
            }
        }

        /// <summary>
        /// calcualte height
        /// </summary>
        private void HeightCalculation()
        {
			if (useScrollwheelZooming)
				zoomPos += ScrollWheel * Time.deltaTime * scrollWheelZoomingSensitivity;
			if (useKeyboardZooming)
				zoomPos += ZoomDirection * Time.deltaTime * keyboardZoomingSensitivity;

			zoomPos = Mathf.Clamp01 (zoomPos);

			float targetHeight = Mathf.Lerp (minHeight, maxHeight, zoomPos);

			Plane plane = new Plane (Vector3.up, Vector3.up * targetHeight);
			float dist;
			Ray ray = new Ray (transform.position, transform.forward);
			plane.Raycast (ray, out dist);

			//Vector3 position = m_Transform.Translate ( for targetHeight,Space.Self );
			m_Transform.position = Vector3.Lerp (m_Transform.position, ray.GetPoint(dist), Time.deltaTime * heightDampening);
			
        }

        /// <summary>
        /// rotate camera
        /// </summary>
        private void Rotation()
        {
			
            if(useKeyboardRotation)
                transform.Rotate(Vector3.up, RotationDirection * Time.deltaTime * rotationSped, Space.World);

			if (useMouseRotation && Input.GetKey (mouseRotationKey)) {
				Ray ray = new Ray (transform.position, transform.forward);
				Plane p = new Plane(Vector3.up, Vector3.zero);
				float dist;
				p.Raycast(ray, out dist);
				transform.RotateAround (ray.GetPoint(dist),Vector3.up,MouseAxis.x * Time.deltaTime * mouseRotationSpeed);
				//m_Transform.Rotate (Vector3.up, -MouseAxis.x * Time.deltaTime * mouseRotationSpeed, Space.World);
		
			}
        }

        /// <summary>
        /// follow targetif target != null
        /// </summary>
        private void FollowTarget()
        {
            Vector3 targetPos = new Vector3(targetFollow.position.x, m_Transform.position.y, targetFollow.position.z) + targetOffset;
            m_Transform.position = Vector3.MoveTowards(m_Transform.position, targetPos, Time.deltaTime * followingSpeed);
        }

        /// <summary>
        /// limit camera position
        /// </summary>
        private void LimitPosition()
        {
            if (!limitMap)
                return;
                
            m_Transform.position = new Vector3(Mathf.Clamp(m_Transform.position.x, -limitX, limitX),
                m_Transform.position.y,
                Mathf.Clamp(m_Transform.position.z, -limitY, limitY));
        }

        /// <summary>
        /// set the target
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            targetFollow = target;
        }

		public void SetTarget(Vector2 value)
		{
			Vector3 target = new Vector3(value.x * limitX , 0, value.y * limitY);
			Plane plane = new Plane (Vector3.up, transform.position);
			Ray ray = new Ray (target , -transform.forward);
			float dist;
			plane.Raycast (ray, out dist);
			transform.position = ray.GetPoint(dist);
		}

        /// <summary>
        /// reset the target (target is set to null)
        /// </summary>
        public void ResetTarget()
        {
            targetFollow = null;
        }

        /// <summary>
        /// calculate distance to ground
        /// </summary>
        /// <returns></returns>
        private float DistanceToGround()
        {
            Ray ray = new Ray(m_Transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, groundMask.value))
                return (hit.point - m_Transform.position).magnitude;

            return 0f;
        }

        #endregion
    }
