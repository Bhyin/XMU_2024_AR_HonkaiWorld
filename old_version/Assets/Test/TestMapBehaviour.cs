using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;


public class TestMapBehaviour : MonoBehaviour
{
    //Map map = null;
    //[SerializeField] SquareRenderingProperty squareRenderingProperty;
    //public InputManager interactionManager;

    //public SquareType type = SquareType.START;

    //private void Awake()
    //{
    //    CreateMap();
    //}

    //private void OnEnable()
    //{
    //    interactionManager.pressEvent += OnPressCallback;
    //    //interactionManager.longPressEvent += OnLongPressCallback;
    //    //interactionManager.clickEvent += OnClickCallback;
    //    //interactionManager.releaseEvent += OnReleaseCallback;
    //}

    //public void OnPressCallback()
    //{
    //    int randrow = URandom.Range(0, 9);
    //    int rnadcol = URandom.Range(0, 9);
    //    map.SetSquareType(new Vector2Int(randrow, rnadcol), type);
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    map.UpdateMaterial();
    //}

    ///// <summary>
    ///// ������ͼ�������޸ĵ�ͼ�����״̬
    ///// </summary>
    //public void CreateMap()
    //{
    //    // ��ȡ��ͼ����ʵ��
    //    map = Map.instance;
    //    // ���õ�ͼ��Ⱦ����
    //    map.SetSquareRenderProperty(squareRenderingProperty);


    //    Transform child;
    //    MeshRenderer renderer;
    //    string[] coordStr;
    //    int row, col;
    //    for(int i = 0; i < transform.childCount; ++i)
    //    {
    //        child = transform.GetChild(i);
    //        coordStr = child.name.Split('_');
    //        row = int.Parse(coordStr[0]);
    //        col = int.Parse(coordStr[1]);

    //        renderer = child.GetComponent<MeshRenderer>();
    //        map.AddSquare(new Vector2Int(row, col), child, ref renderer);
    //    }
    //}

    //private IEnumerator CreateMapCoroutine()
    //{
    //    // ͨ��Э������ﴴ����ͼ�Ĺ��̣�ʵ�ֶ���Ч��
    //    yield return null;
    //}

    ///// <summary>
    ///// ��ʼ����ͼ
    ///// ������㡢�յ㡢����ϰ�����õ���·���ȣ�ͬʱ���Ŷ���������ʼ����ͼ�Ĺ���
    ///// </summary>
    //public void InitMap()
    //{
        
    //}

    //private IEnumerator InitMapCoroutine()
    //{
    //    // ͨ��Э�̱���ʼ����ͼ�Ĺ��̣�ʵ�ֶ���Ч��
    //    yield return null;
    //}

    ///// <summary>
    ///// �ڽ����п��Ƶ�ͼ��ת
    ///// </summary>
    ///// <param name="factor"></param>
    //public void SetMapRotation(float factor)
    //{
    //}

    ///// <summary>
    ///// �����п��Ƶ�ͼ����
    ///// </summary>
    ///// <param name="factor"></param>
    //public void SetMapScale(float factor)
    //{

    //}
}
