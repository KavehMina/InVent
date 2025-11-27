
using InVent.Data;
using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InVent.Services.AttachmentServices
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
        Task<ResponseModel<Attachment>> GetEntitiesAttachments(Guid entityId,string entityType);
        Task<ResponseModel<Attachment>> DeleteLocalFiles(string FilePath);
    }

    public class AttachmentRepository(IDbContextFactory<EntityDBContext> contextFactory, IWebHostEnvironment env) : Repository<Attachment>(contextFactory), IAttachmentRepository
    {
        private readonly IDbContextFactory<EntityDBContext> contextFactory = contextFactory;
        private readonly IWebHostEnvironment env = env;

        public async Task<ResponseModel<Attachment>> DeleteLocalFiles(string FilePath)
        {
            await Task.CompletedTask;
            var fullPath = Path.Combine(env.WebRootPath + FilePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return new ResponseModel<Attachment>
                {
                    Message = Messages.Delete,
                    Success = true
                };
            }
            else
            {
                return new ResponseModel<Attachment> { Message = Messages.NotFound, Success = false };
            }
        }

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
