using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RobotsGame
{
    /// <summary>
    /// Летаюшее шасси. Старается взлететь как можно выше, может застрять под потолком.
    /// </summary>
    public class FlyShassis : MonoBehaviour, IShassis
    {
        #region private variables

        Transform _transform;
        Robot _robot;
        Animator _animator;
        SoundPlayer _sound;

        #endregion

        #region public propertys

        public ShassisEnum ShassisCode
        {
            get { return ShassisEnum.Fly; }
        }

        public static readonly string shassisName = "Летающий робот";
        public string ShassisName
        {
            get { return shassisName; }
        }

        public Sprite ShassisSprite
        {
            get { return SpriteStorage.instance.flyShassisSprite; }
        }

        #endregion

        #region Unity functions

        void Start()
        {
            _robot = GetComponent<Robot>();
            _transform = GetComponent<Transform>();
            GetComponent<SpriteRenderer>().sprite = ShassisSprite;
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = AnimationStorage.instance.flyShassisAnimator;
            _sound = GetComponent<SoundPlayer>();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Сдвинуться в следующий тайл
        /// </summary>
        /// <param name="direction">Направление движения. True если вперед.</param>
        /// <returns>Сопрограмма</returns>
        public IEnumerator Move(bool direction)
        {
            if (direction)
                yield return StartCoroutine(MoveForward());
            else
                yield return StartCoroutine(MoveBackward());

            UtilityFunctions.Leveling(_transform);
        }

        /// <summary>
        /// Расчитывает и отрисовывает линию-подсказку маршрута
        /// </summary>
        /// <param name="lineRenderer">Рендер, который будет рисовать линию</param>
        /// <param name="startPoint">Начальная точка с которой начнём рисовать</param>
        public void RenderLine(LineRenderer lineRenderer, Vector3 startPoint)
        {
            List<Vector3> path = new List<Vector3>();

            foreach (Vector3 point in CalculatePath(startPoint))
                path.Add(point);

            lineRenderer.SetVertexCount(path.Count);

            for (int i = 0; i < path.Count; i++)
                lineRenderer.SetPosition(i, path[i]);
        }

        /// <summary>
        /// Проверяет находится ли робот в полёте или падении
        /// </summary>
        /// <returns>true если падаем</returns>
        public bool CheckForFly()
        {
            return BoardModel.CheckForEmpty(_transform.position + Vector3.up);
        }

        #endregion

        #region first coroutines

        private IEnumerator MoveBackward()
        {
            //проверка на взлёт
            if (BoardModel.CheckForEmpty(_transform.position + Vector3.up))
                yield return StartCoroutine(MakeStepUp());

            //проверка на шаг влево
            else if (BoardModel.CheckForEmptyOrPlatform(_transform.position + Vector3.left))
            {
                yield return StartCoroutine(MakeStepLeft());
            }

            //проверка на шаг по диагонали
            else if (BoardModel.CheckForEmpty(_transform.position + Vector3.down) &&
                BoardModel.CheckForEmptyOrPlatform(_transform.position + new Vector3(-1, -1, 0)))
            {
                yield return StartCoroutine(MakeStepLeftDown());
            }

            else if (BoardModel.CheckForPlatform(_transform.position))
                _robot.OnReturnAction();

            else
                _robot.OnStoppageAction();
        }

        private IEnumerator MoveForward()
        {
            //проверка на взлёт
            if (BoardModel.CheckForEmpty(_transform.position + Vector3.up))
                yield return StartCoroutine(MakeStepUp());

            //проверка на шаг вправо
            else if (BoardModel.CheckForEmptyOrPlatform(_transform.position + Vector3.right))
                yield return StartCoroutine(MakeStepRight());

            //проверка на шаг по диагонали
            else if (BoardModel.CheckForEmpty(_transform.position + Vector3.up) &&
                BoardModel.CheckForEmptyOrPlatform(_transform.position + new Vector3(1, -1, 0)))
            {
                yield return StartCoroutine(MakeStepRightDown());
            }

            else
                _robot.ForwardFlag = false;
        }

        private IEnumerable<Vector3> CalculatePath(Vector3 point)
        {
            yield return point;

            while (true)
            {
                //проверка на взлёт
                if (BoardModel.CheckForEmpty(point + Vector3.up))
                    point += Vector3.up;

                //проверка на шаг вправо
                else if (BoardModel.CheckForEmptyOrPlatform(point + Vector3.right))
                    point += Vector3.right;

                //проверка на шаг по диагонали
                else if (BoardModel.CheckForEmpty(point + Vector3.up) &&
                    BoardModel.CheckForEmptyOrPlatform(point + new Vector3(1, -1, 0)))
                {
                    point += new Vector3(1, -1, 0);
                }

                else
                    break;

                point = UtilityFunctions.Leveling(point);
                yield return point;
            }
        }

        #endregion

        #region second coroutines

        private IEnumerator MakeStepRight()
        {
            yield return StartCoroutine(MakeStep(Vector3.right, 1));
        }

        private IEnumerator MakeStepLeft()
        {
            yield return StartCoroutine(MakeStep(Vector3.left, 1));
        }

        private IEnumerator MakeStepUp()
        {
            yield return StartCoroutine(MakeStep(Vector3.up, 1));
        }

        private IEnumerator MakeStepLeftDown()
        {
            yield return StartCoroutine(MakeStep(new Vector3(-1, -1, 0), 1));
        }

        private IEnumerator MakeStepRightDown()
        {
            yield return StartCoroutine(MakeStep(new Vector3(1, -1, 0), 1));
        }

        #endregion

        #region others

        private IEnumerator MakeStep(Vector3 direction, float speed)
        {
            float t = 0;
            Vector3 pos = _transform.position;
            _sound.PlayRandomClip(ClipStorage.instance.flyShassisClips);

            while (t <= speed)
            {
                _transform.position = Vector3.Lerp(pos, pos + direction, t / speed);
                t += Time.deltaTime;
                yield return null;
            }
        }

        #endregion
    }
}

