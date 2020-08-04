using System.Collections.Generic;
using API.Models;

namespace API.Data
{
  public class MockPostterRepo : IPostterRepo
  {
    public void DeleteMessage(Message msg)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Message> GetAllMessages()
    {
      return new List<Message> {
          new Message{ Id=0, Date=new System.DateTime(), Text="Message #1" },
          new Message{ Id=1, Date=new System.DateTime(), Text="Message #2" },
          new Message{ Id=2, Date=new System.DateTime(), Text="Message #3" }
      };
    }

    public Message GetMessageById(int id)
    {
      return new Message{ Id=0, Date=new System.DateTime(), Text="Message #1" };
    }

    public void UpdateMessage(Message msg)
    {
      throw new System.NotImplementedException();
    }

    void IPostterRepo.CreateMessage(Message msg)
    {
      throw new System.NotImplementedException();
    }

    bool IPostterRepo.SaveChanges()
    {
      throw new System.NotImplementedException();
    }
  }
}