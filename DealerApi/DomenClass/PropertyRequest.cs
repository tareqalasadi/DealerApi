namespace DealerApi.DomenClass
{
    public class PropertyRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Area { get; set; }
        public string? Location { get; set; }
        public int? Space { get; set; }

        // Land-specific
        public string? LandType { get; set; }

        // House-specific
        public int? NumberOfRooms { get; set; }
        public int? NumberOfBathrooms { get; set; }
        public int? FloorNumber { get; set; }

        // Villa-specific
        public int? NumberOfFloors { get; set; }
        public decimal? Price { get; set; }

        public bool? TakePicture { get; set; }

        public string DescEn { get; set; }
        public string DescAr { get; set; }

        public List<PropertyRequestImage> Images { get; set; } = new();

    }

    public class PropertyRequestImage
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
    }
}
