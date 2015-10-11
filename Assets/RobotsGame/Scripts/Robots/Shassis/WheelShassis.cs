using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RobotsGame
{
    /// <summary>
    /// Колёсное шасси.
    /// </summary>
    public class WheelShassis : MonoBehaviour, IShassis
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
            get { return ShassisEnum.Wheel; }
        }

        public static readonly string shassisName = "Колёсный робот";
        public string ShassisName
        {
            get { return shassisName; }
        }

        public Sprite ShassisSprite
        {
            get { return SpriteStorage.instance.wheelShassisSprite; }
        }

        #endregion

        #region Unity functions

        void Start()
        {
            _robot = GetComponent<Robot>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = AnimationStorage.instance.whellShassisAnimator;
            _sound = GetComponent<SoundPlayer>();
            GetComponent<SpriteRenderer>().sprite = ShassisSprite;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Проверяет находится ли робот в полёте или падении
        /// </summary>
        /// <returns>true если падаем</returns>
        public bool CheckForFly()
        {
            return BoardModel.CheckForEmpty(_transform.position + Vector3.down);
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

        #endregion

        #region first coroutines

        private IEnumerator MoveBackward()
        {
            //проверка на падение
            if (BoardModel.CheckForEmpty(_transform.position + Vector3.down))
                yield return StartCoroutine(MakeStepDown());

            //проверка на шаг влево
            else if (BoardModel.CheckForEmptyOrPlatform(_transform.position + Vector3.left))
                yield return StartCoroutine(MakeStepLeft());

            //проверка на шаг по диагонали
            else if (BoardModel.CheckForEmptyOrPlatform(_transform.position + Vector3.up) &&
                BoardModel.CheckForEmptyOrPlatform(_transform.position + new Vector3(-1, 1, 0)))
            {
                yield return StartCoroutine(MakeStepLeftUp());
            }

            else if (BoardModel.CheckForPlatform(_transform.position))
                _robot.OnReturnAction();

            else
                _robot.OnStoppageAction();
        }

        private IEnumerator MoveForward()
        {
            //проверка на падение
            if (BoardModel.CheckForEmpty(_transform.position + Vector3.down))
                yield return StartCoroutine(MakeStepDown());

            //проверка на шаг вправо
            else if (BoardModel.CheckForEmptyOrPlatform(_transform.position + Vector3.right))
                yield return StartCoroutine(MakeStepRight());

            //проверка на шаг по диагонали
            else if (BoardModel.CheckForEmptyOrPlatform(_transform.position + Vector3.up) &&
                BoardModel.CheckForEmptyOrPlatform(_transform.position + new Vector3(1, 1, 0)))
            {
                yield return StartCoroutine(MakeStepRightUp());
            }

            else
                _robot.ForwardFlag = false;
        }

        private IEnumerable<Vector3> CalculatePath(Vector3 point)
        {
            yield return point;

            while (true)
            {
                //проверка на падение
                if (BoardModel.CheckForEmpty(point + Vector3.down))
                    point += Vector3.down;

                //проверка на шаг вправо
                else if (BoardModel.CheckForEmptyOrPlatform(point + Vector3.right))
                    point += Vector3.right;

                //проверка на шаг по диагонали
                else if (BoardModel.CheckForEmptyOrPlatform(point + Vector3.up) &&
                    BoardModel.CheckForEmptyOrPlatform(point + new Vector3(1, 1, 0)))
                    yield return point += new Vector3(1, 1, 0);

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
            _animator.SetInteger("AnimState", 1);
            _sound.PlayRandomClip(ClipStorage.instance.whellShassisClips);
            yield return StartCoroutine(MakeStep(Vector3.right, 1));
        }

        private IEnumerator MakeStepLeft()
        {
            _animator.SetInteger("AnimState", -1);
            _sound.PlayRandomClip(ClipStorage.instance.whellShassisClips);
            yield return StartCoroutine(MakeStep(Vector3.left, 1));
        }

        private IEnumerator MakeStepDown()
        {
            _animator.SetInteger("AnimState", 0);
            yield return StartCoroutine(MakeStep(Vector3.down, 0.2f));
        }

        private IEnumerator MakeStepLeftUp()
        {
            _animator.SetInteger("AnimState", -1);
            _sound.PlayRandomClip(ClipStorage.instance.whellShassisClips);
            yield return StartCoroutine(MakeStep(new Vector3(-1, 1, 0), 1.5f));
        }

        private IEnumerator MakeStepRightUp()
        {
            _animator.SetInteger("AnimState", 1);
            _sound.PlayRandomClip(ClipStorage.instance.whellShassisClips);
            yield return StartCoroutine(MakeStep(new Vector3(1, 1, 0), 1.5f));
        }

        #endregion

        #region others

        private IEnumerator MakeStep(Vector3 direction, float speed)
        {
            float t = 0;
            Vector3 pos = _transform.position;

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

