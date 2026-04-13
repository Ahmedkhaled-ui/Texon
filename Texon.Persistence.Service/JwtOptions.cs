namespace E_Commerce.infrastructure.services
{
    public class JwtOptions
    {
#nullable disable
        public static string SectionName = "JwtOptions";
        public string Key { get; set; }
        public string issure { get; set; }
        public string audience { get; set; }
        public double Duration { get; set; }    
    }
}
