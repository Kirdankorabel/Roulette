public class ChipPool : Pool<Chip>
{
    protected override Chip InstantiateItem()
    {
        var item = base.InstantiateItem();
        item.OnChipDisabled += ReleaseItem;
        return item;
    }
}
