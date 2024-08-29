using System.Text.RegularExpressions;

namespace PokemonApi.Models
{
    public class Poke
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        public bool Capturado  { get; set; } = false;
}
}
