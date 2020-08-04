using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Data;
using AutoMapper;
using API.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace API.Controllers
{
  [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IPostterRepo _repository;
        private readonly IMapper _mapper;

        public MessagesController(IPostterRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper= mapper;
        }

        // GET: api/messages
        [HttpGet]
        public ActionResult<IEnumerable<MessageReadDto>> GetMessages()
        {
            var messages = _repository.GetAllMessages();

            return Ok(_mapper.Map<IEnumerable<MessageReadDto>>(messages));
        }

        // GET: api/messages/{id}
        [HttpGet("{id}", Name="GetMessageById")]
        public ActionResult<MessageReadDto> GetMessageById(int id)
        {
            var message = _repository.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MessageReadDto>(message));
        }

        // POST: api/messages
        [HttpPost]
        public ActionResult<MessageReadDto> AddMessage(MessageCreateDto messageCreateDto)
        {
            var messageModel = _mapper.Map<Message>(messageCreateDto);
            _repository.CreateMessage(messageModel);
            _repository.SaveChanges();

            var messageReadDto = _mapper.Map<MessageReadDto>(messageModel);
            
            return CreatedAtRoute(nameof(GetMessageById), new {Id = messageReadDto.Id}, messageReadDto);
        }

        // PUT: api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, MessageUpdateDto messageUpdateDto)
        {
            var messageModelFromRepo = _repository.GetMessageById(id);
            if (messageModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(messageUpdateDto, messageModelFromRepo);

            _repository.UpdateMessage(messageModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/messages/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialMessageUpdate(int id, JsonPatchDocument<MessageUpdateDto> patchDocument)
        {
            var messageModelFromRepo = _repository.GetMessageById(id);
            if (messageModelFromRepo == null)
            {
                return NotFound();
            }

            var messageToPatch = _mapper.Map<MessageUpdateDto>(messageModelFromRepo);
            patchDocument.ApplyTo(messageToPatch, ModelState);

            if (!TryValidateModel(messageToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(messageToPatch, messageModelFromRepo);

            _repository.UpdateMessage(messageModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/messages/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMessage(int id)
        {
            var messageModelFromRepo = _repository.GetMessageById(id);
            if (messageModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteMessage(messageModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
