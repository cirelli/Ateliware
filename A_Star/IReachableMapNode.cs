namespace A_Star
{
    public interface IReachableMapNode
    {
        IMapNode MapNode { get; }

        int CostToMove { get; }

        int CostToMoveToDestination { get; }
    }
}
