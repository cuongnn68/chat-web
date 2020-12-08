export default {get, post};

//?: does it need async await here // async function return Promise so guess not
function get(/** @type {string} */url, data) {
  url = "https://" + localStorage.getItem("domain") + url;
  let keys = Object.keys(data);
  if(keys.length > 0) {
    url = url + "?"
    keys.forEach((key) => {
            url += key + "=" + data[key] + "&";
        });
    url = url.slice(0, url.length-1);
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

function post(url, data) {
  url = "https://" + localStorage.getItem("domain") + url;
  console.log("post: " + url);
  const token = getToken();
  return fetch(url, {
            method: "POST",
            headers: {
              "Accept": "application/json",
              "Content-type": "application/json; charset=utf-8",
              "Authorization": (token && "bearer " + token),
            },
            body: JSON.stringify(data),
          });
}

function getToken() {
  const info = JSON.parse(localStorage.getItem("userInfo"));
  if(!info) return null;
  return info["token"];
}