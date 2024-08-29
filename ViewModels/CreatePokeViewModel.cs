using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace PokemonApi.ViewModels
{
    public class CreatePokeViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public int Idade { get; set; }
        [Required]
        public string CPF { get; set; }
        [JsonIgnore]
        public bool Capturado { get; set; } = false;
    }
}
