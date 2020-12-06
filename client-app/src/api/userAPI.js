export function testAPI(username, id) {

  fetch("https://localhost:5001/api/testapi/jwt?id=1&id=2")
    .then(res => {
      const tes = res.json();
    })



  fetch("https://localhost:5001/api/testapi/jwt" + "?username=" + username + "?id=" + id,{
    method: "GET",
    mod: "cors",
    // body: JSON.stringify({
    //   username,
    //   id
    // })
  })
  .then(response => {
    response.status
    console.log(response.json());
    
    // const url = new URL(response.url).searchParams;
    // const jwt = url.get(jwt);
    // console.log(jwt);
  })
  .then(console.log);
}

function userAuth() {
  const link = localStorage.getItem("domain") + "/api/user/auth";
  
}