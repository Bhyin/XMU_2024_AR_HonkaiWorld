using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Map
{

    public partial class MapManager : MonoBehaviour
    {
        // ��Ա����
        [SerializeField] Transform m_Map;
        [SerializeField] GameObject m_SquarePrefab; // ��ͼ����Ԥ����
        [SerializeField] int m_Row; // ��ͼ����
        [SerializeField] int m_Col; // ��ͼ����

        // ��ͼʹ�����Ҵ�����Ԥ���壬��������ʱ����
        private Dictionary<Vector2Int, MapSquareBehaviour> m_SquareDict = new Dictionary<Vector2Int, MapSquareBehaviour>();

        // ��������յ�Ļ·��
        private List<Vector2Int> m_Path = new List<Vector2Int>();
        private List<Vector3> m_PathPositions = new List<Vector3>();
        private Vector2Int m_Start, m_End;

        private Utils.AStar m_AStarInstance;

        // ����
        public int row => m_Row;
        public int col => m_Col;

        public float squareLength => Vector3.Distance(this[0, 0].Position, this[0, 1].Position);

        public float scaleFactor => GetMapScale() / Mathf.Min(m_Row, m_Col); // �������ý�ɫ�͵��˵�����

        public MapSquareBehaviour this[Vector2Int coord] => GetBehaviour(coord);

        public MapSquareBehaviour this[int row, int col] => GetBehaviour(new Vector2Int(row, col));

    }
}


