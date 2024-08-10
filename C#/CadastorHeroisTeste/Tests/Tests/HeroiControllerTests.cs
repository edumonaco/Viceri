using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using CadastorHeroisTeste.Controllers;
using CadastorHeroisTeste.Models;
using NuGet.ContentModel;

namespace CadastorHeroisTeste.Tests
{
    public class HeroiControllerTests
    {
        private readonly HeroiController _controller;
        private readonly Mock<DbSet<Heroi>> _mockHeroiSet;
        private readonly Mock<Contexto> _mockContext;

        public HeroiControllerTests()
        {
            // Configura o mock do DbSet
            _mockHeroiSet = new Mock<DbSet<Heroi>>();
            _mockContext = new Mock<Contexto>();

            // Configura o mock do DbContext
            _mockContext.Setup(m => m.Herois).Returns(_mockHeroiSet.Object);

            // Inicializa o controlador com o contexto mockado
            _controller = new HeroiController(_mockContext.Object);
        }

        [Fact]
        public async Task Edit_ValidId_UpdatesHeroi()
        {
        
            var id = 1;
            var existingHeroi = new Heroi { Id = id, Nome = "Heroi Existente" };
            var updatedHeroi = new Heroi { Id = id, Nome = "Heroi Atualizado" };

            // Configura o mock para retornar o Heroi existente
            _mockContext.Setup(m => m.Herois.FindAsync(id)).ReturnsAsync(existingHeroi);

            var result = await _controller.Edit(id, updatedHeroi);

            var actionResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Edit_IdMismatch_ReturnsBadRequest()
        {

            var id = 1;
            var heroToUpdate = new Heroi { Id = 2, Nome = "Heroi Atualizado" };

            var result = await _controller.Edit(id, heroToUpdate);

    
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("O ID fornecido na URL não corresponde ao ID do objeto.", actionResult.Value);
        }

        [Fact]
        public async Task Edit_HeroiNotFound_ReturnsNotFound()
        {
   
            var id = 1;
            var heroToUpdate = new Heroi { Id = id, Nome = "Heroi Atualizado" };


            _mockContext.Setup(m => m.Herois.FindAsync(id)).ReturnsAsync((Heroi)null);

            var result = await _controller.Edit(id, heroToUpdate);

               var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Heroi com ID {id} não encontrado.", actionResult.Value);
        }

        [Fact]
        public async Task Edit_InvalidModel_ReturnsBadRequest()
        {

            var id = 1;
            var heroToUpdate = new Heroi { Id = id, Nome = null }; // Nome nulo para simular um erro de modelo


            _controller.ModelState.AddModelError("Nome", "O Nome é obrigatório.");

            var result = await _controller.Edit(id, heroToUpdate);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(_controller.ModelState, actionResult.Value);
        }
    }
}