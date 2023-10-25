namespace A_Star
{
    public interface IMap
    {
        IMapNode GetNode(string coordinate);
        IEnumerable<IReachableMapNode> GetReachableNodes(IMapNode start, IMapNode destination);
    }
}
