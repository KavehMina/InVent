
using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.AttachmentServices
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
        Task<ResponseModel<Attachment>> GetEntitiesAttachments(Guid entityId,string entityType);
    }

    public class AttachmentRepository(IDbContextFactory<EntityDBContext> contextFactory) : Repository<Attachment>(contextFactory), IAttachmentRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;

        public async Task<ResponseModel<Attachment>> GetEntitiesAttachments(Guid entityId, string entityType)
        {
            using var context = contextFactory.CreateDbContext();
            try
            {
                var res = await context.Attachments
                    .Where(x=>x.ParentType == entityType && x.ParentId == entityId)
                    .ToListAsync();
                return new ResponseModel<Attachment>
                {
                    Message = Messages.Received,
                    Entities = res,
                    Success = true
                };
            }
            catch (Exception err)
            {
                return new ResponseModel<Attachment> { Message = err.Message, Success = false };
            }
        }
    }
}
