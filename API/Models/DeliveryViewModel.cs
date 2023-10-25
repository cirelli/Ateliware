namespace API.Models
{
    public struct DeliveryViewModel
    {
        public string Start { get; set; }

        public string Pickup { get; set; }

        public string Destination { get; set; }

        public float TotalTime { get; set; }
    }
}
