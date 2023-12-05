namespace beneficiarios_dif_api.Entities
{
    public class ProgramaSocial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public bool Estatus { get; set; }
        public string Acronimo { get; set; }
        public int AreaAdscripcionId { get; set; } 

        public AreaAdscripcion AreaAdscripcion { get; set; }
    }
}
