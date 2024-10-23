using UnityEngine;

namespace View
{
    public class ChipPool : Pool<Chip>
    {
        [SerializeField] private float _defaultScreenheigh = 1080;

        protected override Chip InstantiateItem()
        {
            var item = base.InstantiateItem();
            item.OnChipDisabled += ReleaseItem;
            item.transform.localScale = Vector3.one * Screen.height / _defaultScreenheigh;
            return item;
        }
    }
}