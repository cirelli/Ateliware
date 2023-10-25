namespace A_Star
{
    public interface IMapNode
    {
        ICoordinate Coordinate { get; }

        IEnumerable<IReachableMapNode> GetReachableNodes(IMapNode destination);
    }
}
