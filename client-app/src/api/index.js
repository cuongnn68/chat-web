export default {get, post, deleteMethod, put, getToken, getUserId};

//?: does it need async await here // async function return Promise so guess not
function get(/** @type {string} */url, data) {
  url = "https://" + localStorage.getItem("domain") + url;
  if(data) {
    let keys = Object.keys(data);
    if(keys.length > 0) {
      url = url + "?"
      keys.forEach((key) => {
              url += key + "=" + data[key] + "&";
          });
      url = url.slice(0, url.length-1);
    }
  }
  console.log("get: " + url);
  const token = getToken();
  return fetch(url, {
            method: "GET",
            headers: {
              "Accept": "application/json",
              "Authorization": (token && "bearer " + token),
            },
          });
            // .then(response => response.json());
            // .then(val => console.log(val));
}

/**
 * 
 * @param {string} url 
 * @param {object} data 
 */
function post(url, data) {
  url = "https://" + localStorage.getItem("domain") + url;
  console.log("post: " + url);
  console.log(JSON.stringify(data));
  const token = getToken();
  return fetch(url, {
            method: "POST",
            headers: {
              "Accept": "application/json",
              "Content-type": "application/json; charset=utf-8",
              "Authorization": (token && "bearer " + token),
            },
            body: JSON.stringify(data),
          })
          .catch(err => console.log(err));
}

function put(url, data) {
  url = "https://" + localStorage.getItem("domain") + url;
  console.log("put: " + url);
  console.log(JSON.stringify(data));
  const token = getToken();
  return fetch(url, {
            method: "PUT",
            headers: {
              "Accept": "application/json",
              "Content-type": "application/json; charset=utf-8",
              "Authorization": (token && "bearer " + token),
            },
            body: JSON.stringify(data),
          })
          .catch(err => console.log(err));
}

function deleteMethod(url, data = null) {
  url = "https://" + localStorage.getItem("domain") + url;
  console.log("delete: " + url);
  console.log(JSON.stringify(data));
  const token = getToken();
  data = data || "";
  return fetch(url, {
            method: "DELETE",
            headers: {
              "Accept": "application/json",
              "Content-type": "application/json; charset=utf-8",
              "Authorization": (token && "bearer " + token),
            },
            body: JSON.stringify(data),
          })
          .catch(err => console.log(err));
}

function getToken() {
  const info = JSON.parse(localStorage.getItem("userInfo"));
  if(!info) return null;
  return info["token"];
}

function getUserId() {
  return JSON.parse(localStorage.getItem("userInfo"))["id"];
}