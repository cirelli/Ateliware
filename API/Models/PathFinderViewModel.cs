namespace API.Models
{
    public struct PathFinderViewModel
    {
        public string Path { get; set; }

        public float TotalTime { get; set; }

        public List<DeliveryViewModel> LatestDeliveries { get; set; }
    }
}
