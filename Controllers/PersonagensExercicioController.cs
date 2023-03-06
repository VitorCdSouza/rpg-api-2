using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using System.Linq;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensExemploController : ControllerBase
    {

        private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

    
        [HttpGet("{nome}")]
        public IActionResult GetByNome (string nome)
        {
            Personagem selecionado = personagens.FirstOrDefault(pe => pe.Nome == nome);
            if(selecionado == null) {
                return NotFound("Personagem não encontrado");
            } else 
                return Ok(selecionado);
        }

        [HttpPost("PostValidacao")]  
        public IActionResult PostValidacao (Personagem novoPersonagm)
        {
            if(novoPersonagm.Defesa < 10 || novoPersonagm.Inteligencia > 30) {
                return BadRequest("Defesa tem que ser maior que 10 e inteligencia menor que 30");
            } else {
                personagens.Add(novoPersonagm);
                return Ok(personagens);
            }
        }

        [HttpPost("PostValidacaoMago")]  
        public IActionResult PostValidacaoMago (Personagem novoPersonagm)
        {
            if(novoPersonagm.Classe == ClasseEnum.Mago && novoPersonagm.Inteligencia < 35) {
                return BadRequest("Magos devem ter inteligencia maior que 35");
            } else {
                personagens.Add(novoPersonagm);
                return Ok(personagens);
            }
        }

        [HttpGet("GetClerigoMago")]  
        public IActionResult GetClerigoMago ()
        {
            personagens.RemoveAll(per => per.Classe == ClasseEnum.Cavaleiro);

            List<Personagem> listaFinal = personagens.OrderByDescending(personagens => personagens.PontosVida).ToList();
            return Ok(listaFinal);
        }

        [HttpGet("GetEstatistica")]  
        public IActionResult GetEstatistica ()
        {
            int personagensCount = personagens.Count;
            int inteligenciaTotal = 0;
            for(int i = 0; personagens.Count > i; i++)
            {
                inteligenciaTotal += personagens[i].Inteligencia;
            }
            return Ok("Quantidade de personagens: " + personagensCount + ", inteligencia total: " + inteligenciaTotal);
        }

        [HttpGet("GetByClasse/{classe}")]  
        public IActionResult GetByClasse (int classe)
        {
            ClasseEnum classePega;
            switch (classe)
            {
                case 1:
                    classePega = ClasseEnum.Cavaleiro;
                    break;
                case 2:
                    classePega = ClasseEnum.Mago;
                    break;
                case 3:
                    classePega = ClasseEnum.Clerigo;
                    break;
                default:
                    return NotFound("Classe não encontrada");
            }

            
            List<Personagem> listaFinal = personagens.FindAll(personagens => personagens.Classe == classePega).ToList();
            if(listaFinal == null) {
                return NotFound("Nenhum elemento");
            } else 
                return Ok(listaFinal);
        }
    }
}