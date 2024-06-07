using UnityEngine;

namespace Game.Map
{
    public class MapSquareBehaviour : MonoBehaviour
    {
        [SerializeField]
        private MapSquareProperty propertyAssets;
        private Vector2Int m_Coordinate = Vector2Int.zero;

        private MeshRenderer m_MeshRenderer;
        private Material m_Material;

        /// <summary>
        /// ����ľֲ�����λ�ú������Ѿ�������Ԥ����ʱ�������
        /// ����ֻ���ݵ�ǰ��״̬���ֲ�ͬ��Ч����Ч����صĲ�����property��������
        /// �����Ի�ȡ�����״̬���任�ȸ�������
        /// ���޷��޸�״̬֮����κ�����
        /// </summary>
        public Type type = Type.SPACE;
        public Direction direction;
        float visiable = 1.0f;
        public Vector2Int Coordinate => m_Coordinate;
        public Vector3 Position => transform.position;
        public Vector3 Forward => transform.forward;
        public Vector3 Up => transform.up;

        private void Awake()
        {
            m_Material = new Material(propertyAssets.shader);
            m_MeshRenderer = GetComponent<MeshRenderer>();
            m_MeshRenderer.sharedMaterial = m_Material;
        }

        private void OnDestroy()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            
        }

        private void LateUpdate()
        {
            m_Material.SetTexture(propertyAssets.uniformIcon, property.icon);
            m_Material.SetColor(propertyAssets.uniformColor, property.color);
            m_Material.SetFloat(propertyAssets.uniformEmisson, property.emission);
            m_Material.SetFloat(propertyAssets.uniformTransparency, visiable);
            m_Material.SetFloat(propertyAssets.uniformDirection, (float)direction);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetCoordinate(Vector2Int coord)
        {
            m_Coordinate = coord;
        }

        public void SetType(Type t) => type = t;

        public void SetDirection(Vector2Int dir)
        {
            if (dir.x == 0 && dir.y > 0) direction = Direction.UP;
            else if (dir.x == 0 && dir.y < 0) direction = Direction.DOWN;
            else if (dir.x < 0 && dir.y == 0) direction = Direction.LEFT;
            else direction = Direction.RIGHT;
        }

        public PropertyItem property => propertyAssets[type];
    }
}

