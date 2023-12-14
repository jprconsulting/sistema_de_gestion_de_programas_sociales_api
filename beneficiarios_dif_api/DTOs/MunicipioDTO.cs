namespace beneficiarios_dif_api.DTOs
{
    public class MunicipioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TotalBeneficiarios { get; set; }
        public string Color { get; set; }
        public string DescripcionIndicador { get; set; }
    }
}