namespace beneficiarios_dif_api.Entities
{
    public class Evidencia
    {
        public int Id { get; set; }
        public int BeneficiarioId { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }

    }
}
