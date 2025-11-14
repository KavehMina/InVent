using InVent.Data.Models;
using InVent.Extensions;

namespace InVent.Services.BankServices
{
    public class BankService(IBankRepository repository)
    {
        public async Task<ResponseModel<Bank>> GetAllBanks() => await repository.GetAll();
        public async Task<ResponseModel<Bank>> GetBankById(string id) => Guid.TryParse(id, out var bankId) ? await repository.GetById(bankId) : null;
        public async Task<ResponseModel<Bank>> GetBankById(Guid id) => await repository.GetById(id);
        public async Task<ResponseModel<Bank>> AddBank(Bank Bank)
        {
            var res = await repository.Add(Bank);
            return res.Success ? res : new ResponseModel<Bank>() { Message = ErrorExtension.HandleErrorMessage(res.Message), Success = false };
        }
        public async Task<ResponseModel<Bank>> EditBank(Bank Bank) => await repository.Update(Bank);
        public async Task<ResponseModel<Bank>> DeleteBank(Bank Bank)
        {
            var res = await repository.Delete(Bank);
            return res.Success ? res : new ResponseModel<Bank>() { Message = ErrorExtension.HandleErrorMessage(res.Message), Success = false };
        }
        public async Task<ResponseModel<Bank>> TestBanks() => await repository.TestBank();
    }
}
