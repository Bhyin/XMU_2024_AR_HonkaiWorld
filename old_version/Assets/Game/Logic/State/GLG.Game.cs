using Game.Map;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace Logic
    {
        /// <summary>
        /// ��Ϸ�߼�
        /// </summary>
        public partial class GameLogic : MonoBehaviour
        {

            public class GameStateFunction : BaseStateFunction
            {
                public override State state => State.GAMING;

                public override void OnUpdate()
                {
                    enemies.CreateEnemy();
                }

                public override void OnLateUpdate()
                {

                }

                public override void OnFixedUpdate()
                {

                }

                public override void OnPress()
                {
                    if (context.addingCharacters)
                    {
                        context.addingCharacters = false;
                        Vector2 screenPosition = input.Position;
                        Ray ray = camera.ScreenPointToRay(screenPosition);
                        RaycastHit hit;
                        if(Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Map")))
                        {
                            MapSquareBehaviour mb = hit.collider.GetComponent<MapSquareBehaviour>();
                            if(mb != null && mb.type == Map.Type.PLACINGCHAR)
                            {
                                characters.CreateCharacter(mb.Position, mb.Forward, mb.Up);
                                map.SetSquareType(mb.Coordinate, Map.Type.CHARACTER);
                                map.ChangeSquareType(Type.PLACINGCHAR, Type.SPACE);
                            }
                        }
                    }
                }

                public override void OnClick()
                {

                }

                public override void OnLongPress()
                {
                    Vector2 screenPosition = input.Position;
                    Ray ray = camera.ScreenPointToRay(screenPosition);
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Map")))
                    {
                        MapSquareBehaviour mb = hit.collider.GetComponent<MapSquareBehaviour>();
                        if(mb != null && mb.type == Type.CHARACTER)
                        {
                            MoveCharacter();
                        }
                        
                    }
                }

                public override void OnRelease()
                {
                    if (context.movingCharacters)
                    {
                        Vector2 screenPosition = input.Position;
                        Ray ray = camera.ScreenPointToRay(screenPosition);
                        RaycastHit hit;
                        if(Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Map")))
                        {
                            MapSquareBehaviour mb = hit.collider.GetComponent<MapSquareBehaviour>();
                            if(mb.type == Type.SPACE)
                            {

                            }
                        }
                    }
                }

                public void MoveCharacter()
                {
                    context.movingCharacters = true;
                    
                    map.ChangeSquareType(Map.Type.SPACE, Map.Type.PLACINGCHAR);
                }

                public void AddCharacter()
                {
                    context.addingCharacters = true;
                    map.ChangeSquareType(Map.Type.SPACE, Map.Type.PLACINGCHAR);
                }

                public override void Enter()
                {
                    // �����ͼ
                    map.SetActive(true);

                    // TODO ���ļ��󲿷ִ��붼����ʱ����ģ������ع�

                    // ����ϰ�
                    for(int i = 0; i < 15; ++i)
                    {
                        map.SetSquareType(map.RandomCoord(), Map.Type.VIRTUAL_BARRIER);
                    }
                    // ����յ�
                    map.SetStart(new Vector2Int(0, 0));
                    map.SetEnd(new Vector2Int(9, 9));
                    map.UpdatePath(); // ����·��

                    // ����·��
                    enemies.SetPath(map.GetPath());
                    enemies.SetScale(map.scaleFactor);
                    enemies.SetActive(true);
                    
                    // �����������ͣ������㡢�յ�->����ϰ�->���·��->��ӽ�ɫ

                    // �����ɫϵͳ�͵���ϵͳ
                    characters.SetScale(map.scaleFactor);
                    characters.SetMovement(map.squareLength);
                    characters.SetActive(true);
                }

                public override void Exit()
                {
                    map.SetActive(false);
                    enemies.SetActive(false);
                    characters.SetActive(false);
                }

                public override void Pause()
                {
                    base.Pause();
                    enemies.Pause();
                    
                }

                public override void Continue()
                {
                    base.Continue();
                    enemies.Continue();
                }
                
            }
        }
    }
}

