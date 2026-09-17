using UnityEngine;

namespace NiumaCharacter
{
    /// <summary>
    /// 管理展示用角色实例
    /// (这里传入的是展示预制体)
    /// </summary>
    [DisallowMultipleComponent]
    public class CharacterPreviewPresenter : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("模型挂载位置")]
        private Transform _previewRoot;
 
        //组件自己创建的展示实例
        private GameObject _previewInstance;
        //记录当前实例来自哪个预制体
        private GameObject _sourcePrefab;

        /// <summary>
        /// 尝试展示角色
        /// </summary>
        public bool TryShow(GameObject prefab, out string error)
        {
            if(!isActiveAndEnabled)
            {
                error = "角色展示组件未启用";
                return false;
            }

            if(_previewRoot == null)
            {
                error = "没有绑定角色挂载点";
                return false;
            }

            if(prefab == null)
            {
                error = "角色展示预制体为空";
                return false;
            }

            if(_previewInstance != null && _sourcePrefab == prefab)
            {
                error = string.Empty;
                return true;
            }

            //TODO:后期可以制作一个对象池进行复用

            //创建实例
            GameObject instance = Instantiate(prefab, _previewRoot, false);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;

            //保留预制体自身的缩放，不强制设置 localScale
            Clear();

            _previewInstance = instance;
            _sourcePrefab = prefab;

            error = string.Empty;
            return true;
        }

        /// <summary>
        /// 这里只清理展示预制体(实例化出来的)
        /// 不清除挂载点与资产
        /// </summary>
        public void Clear()
        {
            if(_previewInstance != null)
            {
                //TODO：创建出对象池后，只放回不销毁

                //先隐藏再销毁
                _previewInstance.SetActive(false);
                Destroy(_previewInstance);
            }

            _previewInstance = null;
            _sourcePrefab = null;

        }


        private void OnDisable()
        {
            Clear();
        }


    }
}
