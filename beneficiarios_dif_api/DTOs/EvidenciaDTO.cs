namespace beneficiarios_dif_api.DTOs
{
    public class EvidenciaDTO
    {
        public int Id { get; set; }
        public int BeneficiarioId { get; set; }
        public string Descripcion { get; set; }
        public string ImagenBase64 { get; set; }
        public string Foto { get; set; }
    }
}
