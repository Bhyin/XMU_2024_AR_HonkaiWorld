using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace Utils
    {
        /// <summary>
        /// ����ͼ���������ں�������֮����������
        /// </summary>
        public enum DistanceType
        {
            D4, // �ֳƳ��нֵ����룬D(p, q) = abs(x - s) + abs(y - t)
            D8, // �ֳ����̾��룬D(p, q) = max(abs(x - s), abs(y - t))
        }
        public class Distance
        {
            public delegate int DistanceAction(Vector2Int p, Vector2Int q);

            private static Dictionary<DistanceType, DistanceAction> m_Dict = new Dictionary<DistanceType, DistanceAction>()
            {
                {DistanceType.D4, D4 },
                {DistanceType.D8, D8 }
            };


            public static int D4(Vector2Int p, Vector2Int q) => SumComponents(Abs(p - q));
            public static int D8(Vector2Int p, Vector2Int q) => MaxComponents(Abs(p - q));

            public static int SumComponents(Vector2Int p) => p.x + p.y;
            public static int MaxComponents(Vector2Int p) => Mathf.Max(p.x, p.y);

            public static int Abs(int a) => Mathf.Abs(a);
            public static Vector2Int Abs(Vector2Int a) => new Vector2Int(Abs(a.x), Abs(a.y));
        }
    }


}

