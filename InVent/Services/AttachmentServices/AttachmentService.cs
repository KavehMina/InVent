using InVent.Data.Models;

namespace InVent.Services.AttachmentServices
{
    public class AttachmentService(IAttachmentRepository repository)
    {
        public async Task<ResponseModel<Attachment>> GetAll(Guid entityId, string entityType) => await repository.GetEntitiesAttachments(entityId, entityType);
        public async Task<ResponseModel<Attachment>> Add(Attachment attachment) => await repository.Add(attachment);
        //public async Task<ResponseModel<Attachment>> Add(List<Attachment> attachments)
        //{
        //    var response = new ResponseModel<Attachment>() { Message = "", Success = false, Entities = [] };
        //    foreach (var attachment in attachments)
        //    {
        //       var res = await repository.Add(attachment);
        //        if (res.Success)
        //        {
        //            response.Entities.Add(res.Entities.FirstOrDefault());
        //        }
        //    }
        //}
        public async Task<ResponseModel<Attachment>> Delete(Guid id) => await repository.DeleteById(id);
    }
}
