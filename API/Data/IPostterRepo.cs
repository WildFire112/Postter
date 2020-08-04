using System.Collections.Generic;
using API.Models;

namespace API.Data
{
    public interface IPostterRepo
    {
        bool SaveChanges();

        IEnumerable<Message> GetAllMessages();
        Message GetMessageById(int id);
        void CreateMessage(Message msg);
        void UpdateMessage(Message msg);
        void DeleteMessage(Message msg);
    }
}