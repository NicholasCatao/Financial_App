using AutoFixture;
using Financial_App.Application.Interfaces;
using Financial_App.Domain.Enums;
using Financial_App.Domain.Interfaces;
using Financial_App.Domain.Model;
using Financial_App.Domain.Request;
using Financial_App.Domain.Response;
using Financial_App.InfraStructure.Repository.Interfaces;
using Financial_App.Services;
using Moq;

namespace Financial_App.Tests.Services
{
    public class AccountServiceTest
    {
        private readonly IAccountService _accountService;
        private readonly Mock<IAccountMapper> _accountMapperMock;
        private readonly Mock<IMovementMapper> _movementMapperMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        public AccountServiceTest()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _accountMapperMock = new Mock<IAccountMapper>();
            _movementMapperMock = new Mock<IMovementMapper>();

            _accountService = new AccountService(_accountRepositoryMock.Object, _accountMapperMock.Object, _movementMapperMock.Object);
        }

        [Theory]
        [InlineData("84e3521f9dfc42c3855feef0045cdfb1")]
        private async Task GetAccountbyIdAsync(string accountId)
        {
            //Arrange
            var accountModel = new Fixture().Build<AccountModel>().With(x => x.Id, accountId).Create();
            var accounResponse = new Fixture().Build<AccountResponse>().With(x => x.Id, accountId).Create();
            _accountMapperMock.Setup(x => x.MapModelToResponseAsync(It.IsAny<AccountModel>())).Returns(Task.FromResult(accounResponse));
            _accountRepositoryMock.Setup(x => x.GetAccountbyIdAsync(It.IsAny<string>())).Returns(Task.FromResult(accountModel));

            //Act
            var result = await _accountService.GetAccountbyIdAsync(accountId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(accountId, result.Dados.Id);
        }

        [Fact]
        private async Task CreateAccountAsync()
        {
            //Arrange
            var accountModel = new Fixture().Create<AccountModel>();
            var accounResponse = new Fixture().Build<AccountResponse>().With(x => x.Id, accountModel.Id).Create();
            var accounRequest = new Fixture().Create<AccountRequest>();
            _accountMapperMock.Setup(x => x.MapModelToResponseAsync(It.IsAny<AccountModel>())).Returns(Task.FromResult(accounResponse));
            _accountRepositoryMock.Setup(x => x.CreateAccountAsync(It.IsAny<AccountModel>())).Returns(Task.FromResult(accounResponse.Id));
            _accountRepositoryMock.Setup(x => x.GetAccountbyIdAsync(It.IsAny<string>())).Returns(Task.FromResult(accountModel));

            //Act
            var result = await _accountService.CreateAccountAsync(accounRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(accounResponse.Id, result.Dados.Id);
        }

        [Theory]
        [InlineData("84e3521f9dfc42c3855feef0045cdfb1", 400)]
        private async Task AccountDropCashAsync(string accountId, decimal amount)
        {
            //Arrange
            var balance = 345;
            var accountModel = new Fixture().Build<AccountModel>().With(x => x.Id, accountId).Create();
            var accounResponse = new Fixture().Build<AccountResponse>().With(x => x.Id, accountId).With(x => x.Balance, balance + amount).Create();
            _accountMapperMock.Setup(x => x.MapModelToResponseAsync(It.IsAny<AccountModel>())).Returns(Task.FromResult(accounResponse));
            _accountRepositoryMock.Setup(x => x.GetAccountbyIdAsync(It.IsAny<string>())).Returns(Task.FromResult(accountModel));
            _accountRepositoryMock.Setup(x => x.UpdateAccount(It.IsAny<AccountModel>())).Returns(Task.CompletedTask);

            //Act
            var result = await _accountService.AccountDropCashAsync(accountId, amount);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(accounResponse.Id, result.Dados.Id);
            Assert.True(balance + amount == result.Dados.Balance);
        }


        [Theory]
        [InlineData("84e3521f9dfc42c3855feef0045cdfb1", "ef319796ccbe4bc1a8ab973612782843", 300)]
        private async Task AccountMoveFundsAsync(string accountSourceId, string accountDestinationId, decimal amount)
        {
            //Arrange
            var movefundsRequest = new Fixture().Build<MoveFundsRequest>()
                .With(x => x.AccountIdSource, accountSourceId).With(x => x.AccountIdDestination, accountDestinationId).With(x => x.FundAmount, amount).Create();
            var movementModel = new Fixture().Build<MoveFundsRequest>().With(x => x.AccountIdSource, accountSourceId).With(x => x.AccountIdDestination, accountDestinationId).Create();
            var accountResponse = new Fixture().Build<AccountResponse>().With(x => x.Id, accountSourceId).Create();
            var accountModel = new Fixture().Build<AccountModel>().With(x => x.Id, accountSourceId).With(x => x.Balance, amount).Create();

            _accountMapperMock.Setup(x => x.MapModelToResponseAsync(It.IsAny<AccountModel>())).Returns(Task.FromResult(accountResponse));
            _accountRepositoryMock.Setup(x => x.GetAccountbyIdAsync(It.IsAny<string>())).Returns(Task.FromResult(accountModel));
            _accountRepositoryMock.Setup(x => x.RegisterMovimentAsync(It.IsAny<MovementModel>())).Returns(Task.CompletedTask);

            //Act
            var result = await _accountService.AccountMoveFundsAsync(movefundsRequest);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        private async Task AccountStatementbyRangeAsync()
        {
            //Arrange
            var initialDate = new DateTime(2023, 06, 22);
            var finalDate = new DateTime(2023, 06, 23);
           
            var accountfilter = new Fixture().Build<AccountFilter>().With(x => x.InitialDate, initialDate).With(x => x.FinallDate, finalDate).Create();
            _movementMapperMock.Setup(x => x.MapModelToResponseAsync(It.IsAny<IEnumerable<MovementModel>>())).Returns(Task.FromResult(MovimentosReponse));
            _accountRepositoryMock.Setup(x => x.AccountStatementbyRangeAsync(It.IsAny<AccountFilter>())).Returns(Task.FromResult(MovimentosModel));
            //Act
            var result = await _accountService.AccountStatementbyRangeAsync(accountfilter);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<IEnumerable<MovementResponse>>>(result);
           
        }

        [Fact]
        private async Task AccountStatementbyTypeAsync()
        {
            //Arrange
            var accountfilter = new Fixture().Build<AccountFilter>().With(x => x.MovimentType, MovimentType.Credit).Create();
            _movementMapperMock.Setup(x => x.MapModelToResponseAsync(It.IsAny<IEnumerable<MovementModel>>())).Returns(Task.FromResult(MovimentosReponse));
            _accountRepositoryMock.Setup(x => x.AccountStatementbyRangeAsync(It.IsAny<AccountFilter>())).Returns(Task.FromResult(MovimentosModel));
            //Act
            var result = await _accountService.AccountStatementbyTypeAsync(accountfilter);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<IEnumerable<MovementResponse>>>(result);
            Assert.Equal(result.Dados.Select(x => x.Type).First(), MovimentType.Credit);
        }

        private static IEnumerable<MovementResponse> MovimentosReponse => new List<MovementResponse>()
        {
            new MovementResponse()
            {
                Id = "84e3521f9dfc42c3855feef0045cdfb1",
                Type = MovimentType.Credit,
                
            }
        };

        private static IEnumerable<MovementModel> MovimentosModel => new List<MovementModel>()
        {
            new MovementModel()
            {
                Id = "84e3521f9dfc42c3855feef0045cdfb1"
            }
        };
    }
}
