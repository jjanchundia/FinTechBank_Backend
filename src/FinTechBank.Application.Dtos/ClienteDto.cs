
namespace FinTechBank.Application.Dtos
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NumeroCuenta { get; set; }
        public decimal? Saldo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string Correo { get; set; }
        public string TipoCliente { get; set; }
        public string EstadoCivil { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string ProfesionOcupacion { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
    }
}