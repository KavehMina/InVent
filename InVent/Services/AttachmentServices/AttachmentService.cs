using InVent.Data.Models;

namespace InVent.Services.AttachmentServices
{
    public class AttachmentService(IAttachmentRepository repository)
    {
        public async Task<ResponseModel<Attachment>> GetAll(Guid entityId, string entityType) => await repository.GetEntitiesAttachments(entityId, entityType);
        public async Task<ResponseModel<Attachment>> Add(Attachment attachment) => await repository.Add(attachment);
        public async Task<ResponseModel<Attachment>> Delete(Guid id) => await repository.DeleteById(id);
        public async Task<ResponseModel<Attachment>> DeleteFile(string FilePath)=> await repository.DeleteLocalFiles(FilePath);
    }
}
