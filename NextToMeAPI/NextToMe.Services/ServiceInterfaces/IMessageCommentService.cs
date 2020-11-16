using NextToMe.Common.DTOs;
using System.Threading.Tasks;

namespace NextToMe.Services.ServiceInterfaces
{
    public interface IMessageCommentService
    {
        public Task<MessageCommentResponse> SendComment(AddMessageCommentRequest request);
    }
}
