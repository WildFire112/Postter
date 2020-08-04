using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;

namespace API.Data
{
  public class SqlPostterRepo : IPostterRepo
  {
    private readonly PostterContext _context;

    public SqlPostterRepo(PostterContext context)
    {
        _context = context;
    }

    public IEnumerable<Message> GetAllMessages()
    {
      return _context.Messages.ToList();
    }

    public Message GetMessageById(int id)
    {
      return _context.Messages.FirstOrDefault(m => m.Id == id);
    }

    public void CreateMessage(Message msg)
    {
      if (msg == null)
      {
          throw new ArgumentNullException(nameof(msg));
      }

      _context.Messages.Add(msg);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdateMessage(Message msg) {}

    public void DeleteMessage(Message msg)
    {
      if (msg == null)
      {
          throw new ArgumentNullException(nameof(msg));
      }

      _context.Remove(msg);
    }
  }
}