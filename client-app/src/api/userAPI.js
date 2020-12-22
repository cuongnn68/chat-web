import rapi from "./index.js"

export function testAPI(username, id) {

  // fetch("https://localhost:5001/api/testapi/jwt?id=1&id=2")
  //   .then(res => {
  //     const tes = res.json();
  //   })

  rapi.get("/api/testapi/jwt", {
    username: "testhaha",
    id: 1111
  })

  // fetch("localhost:5001/api/testapi/jwt" + "?username=" + username + "?id=" + id,{
  //   method: "GET",
  //   mod: "cors",
  //   // body: JSON.stringify({
  //   //   username,
  //   //   id
  //   // })
  // })
  // .then(response => {
  //   response.status
  //   console.log(response.json());
    
  //   // const url = new URL(response.url).searchParams;
  //   // const jwt = url.get(jwt);
  //   // console.log(jwt);
  // })
  // .then(console.log);
}

export function getInfo(userId) {
  return rapi.get("/api/user/" + userId);
}

export function updateUser(userId, fullName, email, phone) {
  return rapi.put("/api/user/" + userId, {fullName, email, phone});
}

export function registerUser(data) {
  return rapi.post("/api/user",data);
}

export function userAuth(data) {
  return rapi.post("/api/user/auth", data);
  // console.log("test login");
}

export function logout() {
  const token = {
    token: JSON.parse(localStorage.getItem("userInfo"))["token"],
  };
  return rapi.post("/api/user/logout", token);
}

export function getRooms() {
  // const info = JSON.parse(localStorage.getItem("userInfo"));
  // const id = info["id"];
  const id = rapi.getUserId();
  return rapi.get("/api/user/" + id + "/room", {});
}

export function newRoom(roomName) {
  return rapi.post("/api/user/" + rapi.getUserId() + "/room", {roomName});
}

export function leaveRoom(roomId, userId) {
  return rapi.deleteMethod("/api/user/" + userId + "/room/" +roomId);
}