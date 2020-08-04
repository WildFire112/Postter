const submitBtn = document.getElementById('submitBtn');
const textarea = document.getElementById('messageText');
const contentBlock = document.getElementById('contentBlock');

const targetUrl = 'https://localhost:5001/api/messages';

const createCard = (text, date, id) => {
  let card = document.createElement('div');
  card.id = `mcard-${id}`;
  card.innerHTML =
  '<div class="row mb-3">' +
    '<div class="col">' +
      '<div class="card bg-light">' +
        '<div class="row">' +
          '<div class="col">' +
            '<div class="card-body">' +
              `<p class="card-text">${text}</p>` +
              `<p class="card-text"><small class="text-muted">${date}</small></p>` +
            '</div>' +
          '</div>' +
          '<div class="col-2 text-right pt-1 pr-4">' +
            `<a href="#" class="badge badge-danger del-btn" btnId="${id}" onClick="deleteMessage(this)">X</a>` +
          '</div>' +
        '</div>' +
      '</div>' +
    '</div>' +
  '</div>';
  return card;
}

const addNewCard = card => {
  contentBlock.appendChild(card);
}

const getTimeStamp = date => {
  let now = date;
  return ((now.getMonth() + 1) + '/' + (now.getDate()) + '/' + now.getFullYear() + " " + now.getHours() + ':'
                + ((now.getMinutes() < 10) ? ("0" + now.getMinutes()) : (now.getMinutes())) + ':' + ((now.getSeconds() < 10) ? ("0" + now
                .getSeconds()) : (now.getSeconds())));
}

const getMessages = () => {
  fetch(targetUrl)
    .then(res => res.json())
    .then(data => {
      data.forEach(m => {
        let utcDateVal = new Date(m.date);
        let actualDate = new Date(utcDateVal.getTime() + (utcDateVal.getTimezoneOffset() * 60 * 1000));
        let card = createCard(m.text, getTimeStamp(actualDate), m.id);
        addNewCard(card);
      })
    })
    .catch(err => console.log(err))
}

const sendMessage = text => {
  let date = new Date().toISOString();;

  fetch(targetUrl, { 
      method: 'POST', 
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({text, date})
    })
    .then(res => res.json())
    .then(data => {
      let utcDateVal = new Date(data.date);
      let actualDate = new Date(utcDateVal.getTime() + (utcDateVal.getTimezoneOffset() * 60 * 1000));
      let card = createCard(data.text, getTimeStamp(actualDate), data.id);
      addNewCard(card)
    })
    .catch(err => console.log(err))
}

const deleteMessage = e => {
  let btnId = e.attributes.btnId.value;

  fetch(targetUrl + `/${btnId}`, { method: 'DELETE' })
  .then(res => {
    document.getElementById('mcard-' + btnId).remove();
  })
  .catch(err => console.log(err))
}

document.addEventListener("DOMContentLoaded", () => {
  getMessages();
});

const submitFunc = async () => {
  if (textarea.value == '') {
    alert('No content!');
  } 
  else {
    sendMessage(textarea.value);
    textarea.value = '';
  }
}
