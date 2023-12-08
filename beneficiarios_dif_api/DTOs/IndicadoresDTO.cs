namespace beneficiarios_dif_api.DTOs
{
    public class IndicadoresDTO
    {
        public int IndicadorId { get; set; }
        public int RangoInicial { get; set; }
        public int RangoFinal { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
    }
}
